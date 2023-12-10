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
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
