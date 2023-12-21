from functools import lru_cache


def part1():
    grid = tuple([line.strip() for line in open("day21.txt").readlines()])
    rows = len(grid)
    cols = len(grid[0])

    start = None

    for row in range(rows):
        for col in range(cols):
            if grid[row][col] == "S":
                start = (row, col)

    result = get_positions(grid, start[0], start[1], 64)
    print(len(result))


@lru_cache(maxsize=100000)
def get_positions(grid, row, col, num_steps):
    if num_steps == 0:
        return {(row, col)}

    rows = len(grid)
    cols = len(grid[0])
    deltas = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    result = set()

    for dr, dc in deltas:
        new_row = row + dr
        new_col = col + dc

        if 0 <= new_row < rows and 0 <= new_col < cols and grid[new_row][new_col] in ".S":
            result = result.union(get_positions(grid, new_row, new_col, num_steps - 1))

    return result


def print_grid(grid, result):
    rows = len(grid)
    cols = len(grid[0])

    for row in range(rows):
        row_to_print = ""
        for col in range(cols):
            if (row, col) in result:
                row_to_print += "O"
            else:
                row_to_print += grid[row][col]

        print(row_to_print)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
