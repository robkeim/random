import re
from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day16.txt").readlines()]

    valid_values = set()

    line_number = 0
    result = 0

    while line_number < len(lines):
        match = re.fullmatch(".+: (\d+)-(\d+) or (\d+)-(\d+)", lines[line_number])

        if match:
            for i in range(int(match.group(1)), int(match.group(2)) + 1):
                valid_values.add(i)

            for i in range(int(match.group(3)), int(match.group(4)) + 1):
                valid_values.add(i)
        elif lines[line_number] == "your ticket:":
            # Skip your ticket
            line_number += 2
            continue
        elif lines[line_number] == "nearby tickets:" or lines[line_number] == "":
            pass  # Nothing to do
        else:
            for value in lines[line_number].split(","):
                if int(value) not in valid_values:
                    result += int(value)

        line_number += 1

    print(result)


def part2():
    lines = [line.strip() for line in open("day16.txt").readlines()]

    valid_values = set()
    attributes = []
    your_ticket = []
    nearby_tickets = []

    line_number = 0

    # Parse and filter input
    while line_number < len(lines):
        match = re.fullmatch("(.+): (\d+)-(\d+) or (\d+)-(\d+)", lines[line_number])

        if match:
            for i in range(int(match.group(2)), int(match.group(3)) + 1):
                valid_values.add(i)

            for i in range(int(match.group(4)), int(match.group(5)) + 1):
                valid_values.add(i)

            attributes.append([match.group(1), int(match.group(2)), int(match.group(3)), int(match.group(4)), int(match.group(5))])
        elif lines[line_number] == "your ticket:":
            your_ticket = [int(value) for value in lines[line_number + 1].split(",")]
            line_number += 1
        elif lines[line_number] == "nearby tickets:" or lines[line_number] == "":
            pass  # Nothing to do
        else:
            ticket = [int(value) for value in lines[line_number].split(",")]

            valid = True
            for value in ticket:
                if int(value) not in valid_values:
                    valid = False
                    break

            if valid:
                nearby_tickets.append(ticket)

        line_number += 1

    column_to_attribute = defaultdict(set)

    for attribute in attributes:
        for i in range(len(your_ticket)):
            found = True
            for j in range(len(nearby_tickets)):
                if not (attribute[1] <= nearby_tickets[j][i] <= attribute[2] or attribute[3] <= nearby_tickets[j][i] <= attribute[4]):
                    found = False
                    break

            if found:
                column_to_attribute[i].add(attribute[0])

    result = 1
    total = 0

    while total < 6:
        for column in column_to_attribute:
            values = column_to_attribute[column]

            if len(values) == 1:
                value = list(values)[0]

                for item in column_to_attribute:
                    if value in column_to_attribute[item]:
                        column_to_attribute[item].remove(value)

                if value.find("departure") != -1:
                    result *= your_ticket[column]
                    total += 1

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
