from collections import deque


def part1():
    instructions = open("quest15_p1.txt").read().strip().split(",")
    solve(instructions)


def solve(instructions):
    walls = set()
    r = 0
    c = 0
    direction_index = 3
    direction_deltas = [(0, 1), (1, 0), (0, -1), (-1, 0)]

    for instruction in instructions:
        if instruction[0] == "L":
            direction_index = (direction_index - 1 + 4) % 4
        elif instruction[0] == "R":
            direction_index = (direction_index + 1) % 4
        else:
            assert False, f"Invalid direction: {instruction[0]}"

        for _ in range(int(instruction[1:])):
            r += direction_deltas[direction_index][0]
            c += direction_deltas[direction_index][1]
            walls.add((r, c))

    target = (r, c)
    walls.remove(target)

    to_process = deque([(0, 0, 0)])
    seen = set()

    while len(to_process):
        r, c, num_steps = to_process.popleft()

        if (r, c) == target:
            print(num_steps)
            break

        if (r, c) in seen or (r, c) in walls:
            continue

        seen.add((r, c))

        #print(r, c, num_steps)

        for dr, dc in direction_deltas:
            to_process.append((r + dr, c + dc, num_steps + 1))


def print_grid(walls):
    min_r = min([wall[0] for wall in walls])
    max_r = max([wall[0] for wall in walls])
    min_c = min([wall[1] for wall in walls])
    max_c = max([wall[1] for wall in walls])

    for r in range(min_r, max_r + 1):
        row = ""
        for c in range(min_c, max_c + 1):
            row += "#" if (r, c) in walls else " "

        print(row)


def part2():
    instructions = open("quest15_p2.txt").read().strip().split(",")
    solve(instructions)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
