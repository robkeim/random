import sys
from collections import deque


def part1():
    grid = [line.strip() for line in open("quest18_p1.txt").readlines()]
    solve_parts_1_and_2(grid)


def part2():
    grid = [line.strip() for line in open("quest18_p2.txt").readlines()]
    solve_parts_1_and_2(grid)


def solve_parts_1_and_2(grid):
    num_r = len(grid)
    num_c = len(grid[0])
    neighbors = [(0, 1), (0, -1), (1, 0), (-1, 0)]

    palm_trees = set()
    to_process = deque()

    for r in range(num_r):
        for c in range(num_c):
            if (c == 0 or c == num_c - 1) and grid[r][c] == ".":
                to_process.append((0, r, c))

            if grid[r][c] == "P":
                palm_trees.add((r, c))

    seen = set()

    while True:
        num_steps, r, c = to_process.popleft()

        if (r, c) in seen:
            continue

        seen.add((r, c))

        if grid[r][c] == "P":
            palm_trees.remove((r, c))

            if not palm_trees:
                print(num_steps)
                break

        for dr, dc in neighbors:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in seen and grid[next_r][next_c] in ".P":
                to_process.append((num_steps + 1, next_r, next_c))


def part3():
    grid = [line.strip() for line in open("quest18_p3.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]

    costs = dict()

    for start_r in range(num_r):
        for start_c in range(num_c):
            if grid[start_r][start_c] != "P":
                continue

            seen = set()
            to_process = deque()
            to_process.append((0, start_r, start_c))

            while to_process:
                num_steps, r, c = to_process.popleft()

                if (r, c) in seen:
                    continue

                seen.add((r, c))

                if grid[r][c] == ".":
                    if (r, c) not in costs:
                        costs[(r, c)] = num_steps
                    else:
                        costs[(r, c)] += num_steps

                for dr, dc in dirs:
                    next_r = r + dr
                    next_c = c + dc

                    if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in seen and grid[next_r][next_c] != "#":
                        to_process.append((num_steps + 1, next_r, next_c))

    print(min(costs.values()))


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
