def part1():
    grid = [line.strip() for line in open("day09.txt").readlines()]

    width = len(grid[0])
    height = len(grid)

    risk = 0

    for y in range(height):
        for x in range(width):
            if ((y == 0 or grid[y - 1][x] > grid[y][x])
                    and (y == height - 1 or grid[y][x] < grid[y + 1][x])
                    and (x == 0 or grid[y][x - 1] > grid[y][x])
                    and (x == width - 1 or grid[y][x] < grid[y][x + 1])):
                risk += int(grid[y][x]) + 1

    print(risk)


def part2():
    grid = [list(line.strip()) for line in open("day09.txt").readlines()]

    width = len(grid[0])
    height = len(grid)

    basins = []

    for y in range(height):
        for x in range(width):
            if grid[y][x] != "9":
                basins.append(flood_fill(grid, x, y))

    basins = sorted(basins)[-3:]

    print(basins[0] * basins[1] * basins[2])


def flood_fill(grid, start_x, start_y):
    width = len(grid[0])
    height = len(grid)

    to_process = [(start_x, start_y)]
    size = 0

    while len(to_process) > 0:
        x, y = to_process.pop()

        if 0 <= x < width and 0 <= y < height and grid[y][x] != "9":
            size += 1
            grid[y][x] = "9"
            to_process.append((x - 1, y))
            to_process.append((x + 1, y))
            to_process.append((x, y - 1))
            to_process.append((x, y + 1))

    return size


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
