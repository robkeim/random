def part1():
    grid = [line.strip() for line in open("quest03_p1.txt").readlines()]
    solve_parts_one_and_two(grid)


def solve_parts_one_and_two(grid):
    num_r = len(grid)
    num_c = len(grid[0])
    mining_area = set()

    for r in range(num_r):
        grid[r] = [1 if value == "#" else 0 for value in grid[r]]

        for c in range(num_c):
            if grid[r][c] == 1:
                mining_area.add((r, c))

    dirs = [(0, 1), (1, 0), (0, -1), (-1, 0)]
    found_diff = True

    while found_diff:
        found_diff = False

        for r, c in mining_area:
            min_neighbor = min([grid[r + dr][c + dc] for (dr, dc) in dirs])
            if min_neighbor >= grid[r][c]:
                grid[r][c] += 1
                found_diff = True

    answer = 0

    for row in grid:
        answer += sum(row)

    print(answer)

def print_grid(grid):
    for r in range(len(grid)):
        row = ""
        for c in range(len(grid[0])):
            if grid[r][c] != 0:
                row += str(grid[r][c])
            else:
                row += "."

        print(row)

    print()


def part2():
    grid = [line.strip() for line in open("quest03_p2.txt").readlines()]
    solve_parts_one_and_two(grid)


def part3():
    raw_grid = [line.strip() for line in open("quest03_p3.txt").readlines()]
    num_c = len(raw_grid[0]) + 2

    grid = ["." * num_c]

    for row in raw_grid:
        grid.append("." + row + ".")

    grid.append("." * num_c)

    num_r = len(grid)
    num_c = len(grid[0])
    mining_area = set()

    for r in range(num_r):
        grid[r] = [1 if value == "#" else 0 for value in grid[r]]

        for c in range(num_c):
            if grid[r][c] == 1:
                mining_area.add((r, c))

    dirs = [(0, 1), (1, 0), (0, -1), (-1, 0), (-1, -1), (1, 1), (-1, 1), (1, -1)]
    found_diff = True

    while found_diff:
        found_diff = False

        for r, c in mining_area:
            min_neighbor = min([grid[r + dr][c + dc] for (dr, dc) in dirs])
            if min_neighbor >= grid[r][c]:
                grid[r][c] += 1
                found_diff = True

    answer = 0

    for row in grid:
        answer += sum(row)

    print(answer)


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
