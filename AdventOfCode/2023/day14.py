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
    grid = [list(line.strip()) for line in open("day14.txt").readlines()]
    rows = len(grid)
    cols = len(grid[0])

    num_steps = 0
    seen = {
        0: "".join(["".join(row) for row in grid])
    }

    while True:
        num_steps += 1
        grid = run_one_cycle(grid)
        grid_hash = "".join(["".join(row) for row in grid])
        if grid_hash in seen:
            break

        seen[grid_hash] = num_steps

    cycle_start = seen[grid_hash]
    cycle_length = num_steps - cycle_start

    remaining_cycles = (1_000_000_000 - cycle_start) % cycle_length

    for _ in range(remaining_cycles):
        grid = run_one_cycle(grid)

    result = 0

    for row in range(rows):
        for col in range(cols):
            if grid[row][col] == "O":
                result += rows - row

    print(result)


def run_one_cycle(grid):
    rows = len(grid)
    cols = len(grid[0])

    # North
    for row in range(1, rows):
        for col in range(cols):
            if grid[row][col] == "O":
                index = row - 1

                while index >= 0 and grid[index][col] == ".":
                    index -= 1

                grid[row][col] = "."
                grid[index + 1][col] = "O"

    # West
    for col in range(1, cols):
        for row in range(rows):
            if grid[row][col] == "O":
                index = col - 1

                while index >= 0 and grid[row][index] == ".":
                    index -= 1

                grid[row][col] = "."
                grid[row][index + 1] = "O"

    # South
    for row in range(rows - 1, -1, -1):
        for col in range(cols):
            if grid[row][col] == "O":
                index = row + 1

                while index < rows and grid[index][col] == ".":
                    index += 1

                grid[row][col] = "."
                grid[index - 1][col] = "O"

    # East
    for col in range(cols - 1, -1, -1):
        for row in range(rows):
            if grid[row][col] == "O":
                index = col + 1

                while index < cols and grid[row][index] == ".":
                    index += 1

                grid[row][col] = "."
                grid[row][index - 1] = "O"

    return grid


def print_grid(grid, message):
    print("\n" + message)

    for row in grid:
        print("".join(row))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
