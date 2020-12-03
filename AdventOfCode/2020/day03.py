def part1():
    grid = [value.strip() for value in open("day03.txt").readlines()]

    row = 0
    col = 0
    num_trees = 0

    while row < len(grid):
        if grid[row][col % len(grid[0])] == "#":
            num_trees += 1

        row += 1
        col += 3

    print(num_trees)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()