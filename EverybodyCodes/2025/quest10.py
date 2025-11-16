from collections import defaultdict


def part1():
    grid = [line.strip() for line in open("quest10_p1.txt").readlines()]
    sheep = set()
    num_r = len(grid)
    num_c = len(grid[0])

    for r in range(len(grid)):
        for c in range(len(grid[0])):
            if grid[r][c] == "S":
                sheep.add((r, c))
            elif grid[r][c] == "D":
                start_r, start_c = r, c

    assert start_r is not None and start_c is not None, "Dragon not found"

    seen = defaultdict(int)
    to_process = [(start_r, start_c, 4)]

    while to_process:
        r, c, num_steps = to_process.pop()

        if seen[(r, c)] >= num_steps:
            continue

        for dr, dc in [(-1, -2), (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2)]:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c:
                to_process.append((next_r, next_c, num_steps - 1))

    print(len(seen.keys() & sheep))


def part2():
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
