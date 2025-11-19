from copy import deepcopy


def part1():
    grid = [line.strip() for line in open("quest12_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    seen = set()
    to_process = [(0, 0)]

    while to_process:
        r, c = to_process.pop()

        if r < 0 or c < 0 or r >= num_r or c >= num_c or (r, c) in seen:
            continue

        seen.add((r, c))

        for dr, dc in ((-1, 0), (1, 0), (0, -1), (0, 1)):
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in seen and grid[next_r][next_c] <= grid[r][c]:
                to_process.append((next_r, next_c))

    print(len(seen))


def part2():
    grid = [line.strip() for line in open("quest12_p2.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    seen = set()
    to_process = [(0, 0), (num_r - 1, num_c - 1)]

    while to_process:
        r, c = to_process.pop()

        if r < 0 or c < 0 or r >= num_r or c >= num_c or (r, c) in seen:
            continue

        seen.add((r, c))

        for dr, dc in ((-1, 0), (1, 0), (0, -1), (0, 1)):
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in seen and grid[next_r][next_c] <= \
                    grid[r][c]:
                to_process.append((next_r, next_c))

    print(len(seen))


def part3():
    grid = [line.strip() for line in open("quest12_p3.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    result = set()

    for fire_start in range(3):
        best = set()

        for r in range(num_r):
            for c in range(num_c):
                potential_best = find_best(r, c, result, num_r, num_c, grid)

                if len(potential_best) > len(best):
                    best = potential_best

        result |= best

    print(len(result))

def find_best(r, c, cur_barrels, num_r, num_c, grid):
    total_barrels = deepcopy(cur_barrels)

    to_process = [(r, c)]

    while to_process:
        r, c = to_process.pop()

        if r < 0 or c < 0 or r >= num_r or c >= num_c or (r, c) in total_barrels:
            continue

        total_barrels.add((r, c))

        for dr, dc in ((-1, 0), (1, 0), (0, -1), (0, 1)):
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in total_barrels and grid[next_r][next_c] <= grid[r][c]:
                to_process.append((next_r, next_c))

    return total_barrels


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
