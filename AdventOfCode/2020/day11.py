def part1():
    cur_iteration = [line.strip() for line in open("day11.txt").readlines()]
    num_rows = len(cur_iteration)
    num_cols = len(cur_iteration[0])

    seen = set()
    seen.add(hash_grid(cur_iteration))

    num_iterations = 0

    while True:
        num_iterations += 1

        next_iteration = [['_' for _ in range(num_cols)] for _ in range(num_rows)]

        for row in range(num_rows):
            for col in range(num_cols):
                occupied_neighbors = 0

                for delta_row in range(-1, 2):
                    for delta_col in range(-1, 2):
                        if delta_row == 0 and delta_col == 0:
                            continue

                        test_row = row + delta_row
                        test_col = col + delta_col

                        if 0 <= test_row < num_rows and 0 <= test_col < num_cols and cur_iteration[test_row][test_col] == "#":
                            occupied_neighbors += 1

                if cur_iteration[row][col] == "L":
                    next_iteration[row][col] = "#" if occupied_neighbors == 0 else "L"
                elif cur_iteration[row][col] == "#":
                    next_iteration[row][col] = "L" if occupied_neighbors >= 4 else "#"
                else:
                    next_iteration[row][col] = "."

        next_hash = hash_grid(next_iteration)
        cur_iteration = next_iteration

        if next_hash in seen:
            break

        seen.add(next_hash)

    result = 0

    for row in range(num_rows):
        for col in range(num_cols):
            if cur_iteration[row][col] == "#":
                result += 1

    print(result)


def part2():
    cur_iteration = [line.strip() for line in open("day11.txt").readlines()]
    num_rows = len(cur_iteration)
    num_cols = len(cur_iteration[0])

    seen = set()
    seen.add(hash_grid(cur_iteration))

    num_iterations = 0

    while True:
        num_iterations += 1

        next_iteration = [['_' for _ in range(num_cols)] for _ in range(num_rows)]

        for row in range(num_rows):
            for col in range(num_cols):
                occupied_neighbors = 0

                for direction_row in range(-1, 2):
                    for direction_col in range(-1, 2):
                        if direction_row == 0 and direction_col == 0:
                            continue

                        multiplier = 1

                        while True:
                            test_row = row + direction_row * multiplier
                            test_col = col + direction_col * multiplier

                            if 0 <= test_row < num_rows and 0 <= test_col < num_cols:
                                if cur_iteration[test_row][test_col] == "#":
                                    occupied_neighbors += 1
                                    break
                                elif cur_iteration[test_row][test_col] == "L":
                                    break
                            else:
                                break

                            multiplier += 1

                if cur_iteration[row][col] == "L":
                    next_iteration[row][col] = "#" if occupied_neighbors == 0 else "L"
                elif cur_iteration[row][col] == "#":
                    next_iteration[row][col] = "L" if occupied_neighbors >= 5 else "#"
                else:
                    next_iteration[row][col] = "."

        next_hash = hash_grid(next_iteration)
        cur_iteration = next_iteration

        if next_hash in seen:
            break

        seen.add(next_hash)

    result = 0

    for row in range(num_rows):
        for col in range(num_cols):
            if cur_iteration[row][col] == "#":
                result += 1

    print(result)


def hash_grid(grid):
    result = ""

    for row in range(len(grid)):
        for col in range(len(grid[0])):
            if grid[row][col] == "#":
                result += str(row) + "," + str(col) + "_"

    return result


def print_grid(grid):
    for row in range(len(grid)):
        print("".join(grid[row]))

    print()


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
