north = "north"
east = "east"
south = "south"
west = "west"


def part1():
    grid = [line.strip() for line in open("day10.txt").readlines()]
    x, y, direction = find_start_position(grid)

    distance = 0

    while True:
        distance += 1

        if direction == north:
            y -= 1
        elif direction == south:
            y += 1
        elif direction == west:
            x -=1
        elif direction == east:
            x += 1
        else:
            assert False, "Invalid direction: {}".format(direction)

        next_pos = grid[y][x]

        if next_pos == "L":
            direction = north if direction == west else east
        elif next_pos == "J":
            direction = west if direction == south else north
        elif next_pos == "7":
            direction = west if direction == north else south
        elif next_pos == "F":
            direction = east if direction == north else south
        elif next_pos == "S":
            result = distance // 2 if distance % 2 == 0 else distance // 2 + 1
            print(result)
            break


def find_start_position(grid):
    height = len(grid)
    width = len(grid[0])

    for y in range(height):
        for x in range(width):
            element = grid[y][x]

            if element != "S":
                continue

            if y > 0 and grid[y - 1][x] in set("|7F"):
                return x, y, north
            elif y < height - 1 and grid[y + 1][x] in set("|JL"):
                return x, y, south
            elif x > 0 and grid[y][x - 1] in set("-FL"):
                return x, y, west
            elif x < width - 1 and grid[y][x + 1] in set("-7J"):
                return x, y, east
            else:
                assert False, "No direction found from S"

    assert False, "S not found in the grid"


def part2():
    grid = [list(line.strip()) for line in open("day10.txt").readlines()]
    x, y, direction = find_start_position(grid)
    path = set()

    while True:
        path.add((x, y))

        if direction == north:
            y -= 1
        elif direction == south:
            y += 1
        elif direction == west:
            x -= 1
        elif direction == east:
            x += 1
        else:
            assert False, "Invalid direction: {}".format(direction)

        next_pos = grid[y][x]

        if next_pos == "L":
            direction = north if direction == west else east
        elif next_pos == "J":
            direction = west if direction == south else north
        elif next_pos == "7":
            direction = west if direction == north else south
        elif next_pos == "F":
            direction = east if direction == north else south
        elif next_pos == "S":
            break

    height = len(grid)
    width = len(grid[0])

    for y in range(height):
        for x in range(width):
            if (x, y) not in path:
                grid[y][x] = "."

    grid = expand_grid(grid)

    grid = flood_fill(grid)

    height = len(grid)
    width = len(grid[0])
    result = 0

    for y in range(height):
        for x in range(width):
            if grid[y][x] == ".":
                result += 1

    print(result)


def expand_grid(grid):
    grid = replace_S(grid)
    rows = len(grid)
    cols = len(grid[0])
    result = []

    # Expand horizontally
    for r in range(rows):
        row = []

        for c in range(cols - 1):
            row.append(grid[r][c])
            if grid[r][c] in set("FL-") and grid[r][c + 1] in set("J7-"):
                row.append("-")
            else:
                row.append("e")

        row.append(grid[r][cols - 1])
        result.append(row)

    # Expand vertically
    grid = result
    result = []
    cols = len(grid[0])

    for r in range(rows - 1):
        row = []

        for c in range(cols):
            if grid[r][c] in set("F7|") and grid[r + 1][c] in set("JL|"):
                row.append("|")
            else:
                row.append("e")

        result.append(grid[r])
        result.append(row)

    result.append(grid[rows - 1])

    return result


def replace_S(grid):
    rows = len(grid)
    cols = len(grid[0])

    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == "S":  # Assume S isn't along a border (handling those cases isn't done for simplicity)
                # This logic doesn't work when S has multiple borders, so I looked at the input and manually analyzed
                # what S should be replaced with
                grid[r][c] = "|"
                return grid

                has_left = grid[r][c - 1] != "."
                has_right = grid[r][c + 1] != "."
                has_up = grid[r - 1][c] != "."
                has_down = grid[r + 1][c] != "."

                if has_left and has_right:
                    replacement = "-"
                elif has_left and has_up:
                    replacement = "J"
                elif has_left and has_down:
                    replacement = "7"
                elif has_right and has_up:
                    replacement = "L"
                elif has_right and has_down:
                    replacement = "F"
                elif has_up and has_down:
                    replacement = "|"
                else:
                    assert False, "Invalid combination"

                grid[r][c] = replacement
                break

    return grid


def flood_fill(grid):
    height = len(grid)
    width = len(grid[0])

    to_process = []

    for i in range(width):
        to_process.append((i, 0))
        to_process.append((i, height - 1))

    for i in range(height):
        to_process.append((0, i))
        to_process.append((width - 1, i))

    processed = set()

    while len(to_process) > 0:
        x, y = to_process.pop()
        processed.add((x, y))

        if grid[y][x] != "." and grid[y][x] != "e":
            continue

        grid[y][x] = "x"

        for delta_x in [-1, 0, 1]:
            for delta_y in [-1, 0, 1]:
                if abs(delta_x + delta_y) == 2:
                    continue

                to_add = (x + delta_x, y + delta_y)

                if to_add not in processed and 0 <= to_add[0] < width and 0 <= to_add[1] < height:
                    to_process.append(to_add)

    return grid


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
