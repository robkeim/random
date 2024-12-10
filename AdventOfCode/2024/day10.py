from functools import lru_cache


def part1():
    grid = tuple([tuple([int(value) for value in line.strip()]) for line in open("day10.txt").readlines()])
    num_r = len(grid)
    num_c = len(grid[0])

    result = 0

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == 0:
                result += len(find_valid_trails(grid, r, c, 1))

    print(result)


deltas = [(1, 0), (-1, 0), (0, 1), (0, -1)]


@lru_cache
def find_valid_trails(grid, r, c, target):
    if target == 10:
        return {(r, c)}

    num_r = len(grid)
    num_c = len(grid[0])

    result = set()

    for dr, dc in deltas:
        new_r = r + dr
        new_c = c + dc

        if 0 <= new_r < num_r and 0 <= new_c < num_c and grid[new_r][new_c] == target:
            result = result.union(find_valid_trails(grid, new_r, new_c, target + 1))

    return result


def part2():
    grid = tuple([tuple([int(value) for value in line.strip()]) for line in open("day10.txt").readlines()])
    num_r = len(grid)
    num_c = len(grid[0])

    result = 0

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == 0:
                result += count_valid_trails(grid, r, c, 1)

    print(result)


@lru_cache
def count_valid_trails(grid, r, c, target):
    if target == 10:
        return 1

    num_r = len(grid)
    num_c = len(grid[0])

    result = 0

    for dr, dc in deltas:
        new_r = r + dr
        new_c = c + dc

        if 0 <= new_r < num_r and 0 <= new_c < num_c and grid[new_r][new_c] == target:
            result += count_valid_trails(grid, new_r, new_c, target + 1)

    return result


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
