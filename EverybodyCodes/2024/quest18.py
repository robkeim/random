from collections import deque


def part1():
    grid = [line.strip() for line in open("quest18_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    neighbors = [(0, 1), (0, -1), (1, 0), (-1, 0)]

    palm_trees = set()
    to_process = deque()

    for r in range(num_r):
        for c in range(num_c):
            if c == 0 and grid[r][c] == ".":
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
