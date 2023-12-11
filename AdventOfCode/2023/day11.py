def part1():
    grid = [[character for character in line.strip()] for line in open("day11.txt").readlines()]
    grid = expand_grid(grid)

    rows = len(grid)
    cols = len(grid[0])
    points = set()

    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == "#":
                points.add((r, c))

    points = list(points)
    result = 0

    for i in range(len(points)):
        for j in range(i + 1, len(points)):
            x1, y1 = points[i]
            x2, y2 = points[j]
            result += abs(x1 - x2) + abs(y1 - y2)

    print(result)


def expand_grid(grid):
    rows = len(grid)
    cols = len(grid[0])

    for r in range(rows - 1, -1, -1):
        if grid[r][0] == "." and len(set(grid[r])) == 1:
            grid = grid[:r] + [["."] * cols] + grid[r:]

    rows = len(grid)
    for c in range(cols - 1, -1, -1):
        empty = True

        for r in range(rows):
            if grid[r][c] == "#":
                empty = False
                break

        if empty:
            for r in range(rows):
                grid[r] = grid[r][:c] + ["."] + grid[r][c:]

    return grid


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
