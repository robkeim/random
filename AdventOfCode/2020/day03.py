def part1():
    grid = [line.strip() for line in open("day03.txt").readlines()]

    print(count_trees(grid, 3, 1))


def part2():
    grid = [line.strip() for line in open("day03.txt").readlines()]

    combinations = [(1, 1), (3, 1), (5, 1), (7, 1), (1, 2)]

    result = 1

    for right, down in combinations:
        result *= count_trees(grid, right, down)

    print(result)


def count_trees(grid, right, down):
    row = 0
    col = 0
    num_trees = 0

    while row < len(grid):
        if grid[row][col % len(grid[0])] == "#":
            num_trees += 1

        row += down
        col += right

    return num_trees


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
