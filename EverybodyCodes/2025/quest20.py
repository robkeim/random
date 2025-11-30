from collections import deque


def part1():
    grid = [line.strip() for line in open("quest20_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    t = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "T":
                t.add((r, c))

    num_trampolines = 0

    for r in range(num_r):
        for c in range(num_c):
            if (r, c) not in t:
                continue

            if (r, c + 1) in t:
                num_trampolines += 1

            if get_direction(r, c) == "U" and (r + 1, c) in t:
                num_trampolines += 1

    print(num_trampolines)


def get_direction(r, c):
    if r % 2 == 0:
        if c % 2 == 0:
            return "D"
        else:
            return "U"
    else:
        if c % 2 == 0:
            return "U"
        else:
            return "D"


def part2():
    grid = [line.strip() for line in open("quest20_p2.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    t = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "T":
                t.add((r, c))
            elif grid[r][c] == "S":
                start = (r, c)
                t.add((r, c))
            elif grid[r][c] == "E":
                end = (r, c)
                t.add((r, c))

    to_process = deque([(start[0], start[1], 0)])
    seen = set()

    while to_process:
        r, c, num_steps = to_process.popleft()

        if r == end[0] and c == end[1]:
            print(num_steps)
            return

        if (r, c) in seen:
            continue

        seen.add((r, c))

        # Right
        if (r, c + 1) in t:
            to_process.append((r, c + 1, num_steps + 1))

        # Left
        if (r, c - 1) in t:
            to_process.append((r, c - 1, num_steps + 1))

        # Down
        if get_direction(r, c) == "U" and (r + 1, c) in t:
            to_process.append((r + 1, c, num_steps + 1))

        # Up
        if get_direction(r, c) == "D" and (r - 1, c) in t:
            to_process.append((r - 1, c, num_steps + 1))

    assert False, "No solution found"


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
