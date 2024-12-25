from collections import deque

def part1():
    grid = [line.strip() for line in open("quest15_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    seen = set()
    to_process = deque()

    for c in range(num_c):
        if grid[0][c] == ".":
            to_process.append((0, 0, c))

    while to_process:
        cost, r, c = to_process.popleft()

        if (r, c) in seen:
            continue

        seen.add((r, c))

        if grid[r][c] == "H":
            print(cost * 2)
            break

        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] != "#":
                to_process.append((cost + 1, next_r, next_c))


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
