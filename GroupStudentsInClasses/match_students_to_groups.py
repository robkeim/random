from itertools import combinations
from collections import defaultdict
from ortools.sat.python import cp_model
import csv


input_csv = "input.csv"



def read_input():
    students = []

    with open(input_csv, 'r', encoding='utf-8') as file:
        reader = csv.reader(file, delimiter=',')
        header_row = next(reader)
        
        time_slots_start_index = 3  # Skip Name, School, Age
        time_slots_headers = header_row[time_slots_start_index:]
        
        for row in reader:
            available_time_slots = []
            for i, value in enumerate(row[time_slots_start_index:]):
                if len(value.strip()) > 0:
                    available_time_slots.append(time_slots_headers[i])
            
            student = {
                "name": row[0],
                "school": row[1],
                "age": int(row[2]),
                "time_slots": available_time_slots
            }

            students.append(student)
    
    return students


def group_students(students):
    candidate_groups = enumerate_candidate_groups(students)
    num_groups = len(candidate_groups)

    model = cp_model.CpModel()
    x = [model.NewBoolVar(f"g_{i}") for i in range(num_groups)]

    # Constraints: each student in ≤1 chosen group
    for st in students:
        model.Add(sum(x[i] for i, g in enumerate(candidate_groups) if st["name"] in g["members"]) <= 1)

    # Constraint: ≤1 group per time slot
    time_slots = set(g["time_slot"] for g in candidate_groups)

    for time_slot in time_slots:
        model.Add(sum(x[i] for i, g in enumerate(candidate_groups) if g["time_slot"] == time_slot) <= 1)

    # Objective: maximize the number of placed students
    model.Maximize(sum(g["size"] * x[i] for i, g in enumerate(candidate_groups)))

    solver = cp_model.CpSolver()
    solver.parameters.max_time_in_seconds = 30
    status = solver.Solve(model)

    groups = []

    if status in (cp_model.OPTIMAL, cp_model.FEASIBLE):
        for i, group in enumerate(candidate_groups):
            if solver.BooleanValue(x[i]):
                groups.append(group)
    
    assigned_students = set()

    for group in groups:
        assigned_students.update(group["members"])

    unassigned_students = [student for student in students if student["name"] not in assigned_students]

    return groups, unassigned_students


def enumerate_candidate_groups(students):
    """Return list of feasible groups (2 to 6 students, same school and time slot, age span ≤1)."""
    by_bucket = defaultdict(list)
    for student in students:
        for time_slot in student["time_slots"]:
            by_bucket[(student["school"], time_slot)].append(student)

    groups = []
    for (school, time_slot), bucket in by_bucket.items():
        names = [student["name"] for student in bucket]
        # Brute-force all 2–6-size combinations (fine for small numbers of students)
        for r in range(2, min(6, len(names)) + 1):
            for combination in combinations(names, r):
                ages = [next(student["age"] for student in bucket if student["name"] == n) for n in combination]
                if max(ages) - min(ages) <= 1:
                    groups.append({"members": combination, "size": r, "time_slot": time_slot, "school": school})
    return groups


def print_output(groups, unassigned_students):
    print("\n=== Assigned Groups ===")
    if not groups:
        print("No groups were formed.")
    else:
        for i, group in enumerate(groups, 1):
            print(f"\nGroup {i}:")
            print(f"  Day: {group['time_slot']}")
            print(f"  School: {group['school']}")
            print(f"  Members ({group['size']}): {', '.join(group['members'])}")
    
    print("\n=== Unassigned Students ===")
    if not unassigned_students:
        print("All students were assigned to groups.")
    else:
        for student in unassigned_students:
            print(f"\n{student['name']}:")
            print(f"  School: {student['school']}")
            print(f"  Age: {student['age']}")
            print(f"  Available days: {', '.join(student['time_slots'])}")


if __name__ == "__main__":
    students = read_input()
    groups, unassigned_students = group_students(students)
    print_output(groups, unassigned_students)
