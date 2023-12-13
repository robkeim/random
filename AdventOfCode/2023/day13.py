def part1():
    grids = [[line.strip() for line in grid.split()] for grid in open("day13.txt").read().split("\n\n")]

    horizontal = 0
    vertical = 0

    for grid in grids:
        rows = len(grid)
        cols = len(grid[0])

        # Find horizontal reflections
        for row in range(rows - 1):
            if grid[row] == grid[row + 1]:
                match = True
                i = 0
                while row - i >= 0 and row + i + 1 < rows:
                    if grid[row - i] != grid[row + i + 1]:
                        match = False
                        break

                    i += 1

                if match:
                    horizontal += row + 1

        # Find vertical reflections
        for col in range(cols - 1):
            if get_col(grid, col) == get_col(grid, col + 1):
                match = True
                i = 0
                while col - i >= 0 and col + i + 1 < cols:
                    if get_col(grid, col - i) != get_col(grid, col + i + 1):
                        match = False
                        break

                    i += 1

                if match:
                    vertical += col + 1

    print(horizontal * 100 + vertical)


def get_col(grid, col):
    result = ""

    for row in grid:
        result += row[col]

    return result


def part2():
    grids = [[line.strip() for line in grid.split()] for grid in open("day13.txt").read().split("\n\n")]

    horizontal = 0
    vertical = 0

    for grid in grids:
        rows = len(grid)
        cols = len(grid[0])

        # Find horizontal reflections
        for row in range(rows - 1):
            num_diff = 0
            i = 0

            while row - i >= 0 and row + i + 1 < rows:
                if num_diff > 1:
                    break

                num_diff += calc_num_diff(grid[row - i], grid[row + i + 1])
                i += 1

            if num_diff == 1:
                horizontal += row + 1

        # Find vertical reflections
        for col in range(cols - 1):
            num_diff = 0
            i = 0

            while col - i >= 0 and col + i + 1 < cols:
                if num_diff > 1:
                    break

                num_diff += calc_num_diff(get_col(grid, col - i), get_col(grid, col + i + 1))
                i += 1

            if num_diff == 1:
                vertical += col + 1

    print(horizontal * 100 + vertical)


def calc_num_diff(line1, line2):
    result = 0

    for i in range(len(line1)):
        if line1[i] != line2[i]:
            result += 1

    return result


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
