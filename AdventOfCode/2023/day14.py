def part1():
    grid = [list(line.strip()) for line in open("day14.txt").readlines()]
    rows = len(grid)
    cols = len(grid[0])

    for row in range(1, rows):
        for col in range(cols):
            if grid[row][col] == "O":
                index = row - 1

                while index >= 0 and grid[index][col] == ".":
                    index -= 1

                grid[row][col] = "."
                grid[index + 1][col] = "O"

    result = 0

    for row in range(rows):
        for col in range(cols):
            if grid[row][col] == "O":
                result += rows - row

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
