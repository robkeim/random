def part1():
    lines = [line.strip() for line in open("day11.txt").readlines()]

    grid = []

    for line in lines:
        grid.append([int(num) for num in line])

    num_flashes = 0

    for step in range(100):
        # Increase energy level by 1
        for row in range(10):
            for col in range(10):
                grid[row][col] += 1

        # Octopuses flash
        flashed = set()
        has_flash = True

        while has_flash:
            has_flash = False

            for row in range(10):
                for col in range(10):
                    if grid[row][col] > 9 and (row, col) not in flashed:
                        flashed.add((row, col))
                        num_flashes += 1
                        has_flash = True

                        for delta_row in range(-1, 2):
                            for delta_col in range(-1, 2):
                                update_row = row + delta_row
                                update_col = col + delta_col

                                if 0 <= update_row < 10 and 0 <= update_col < 10:
                                    grid[update_row][update_col] += 1

        # Reset energy for octopuses
        for (row, col) in flashed:
            grid[row][col] = 0

    print(num_flashes)


def print_grid(grid):
    for row in range(10):
        row_to_print = ""

        for col in range(10):
            row_to_print += str(grid[row][col])

        print(row_to_print)

    print()


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
