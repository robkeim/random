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
    grid = [[character for character in line.strip()] for line in open("day11.txt").readlines()]

    rows = len(grid)
    cols = len(grid[0])
    points = set()
    non_empty_rows = set()
    non_empty_cols = set()

    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == "#":
                points.add((r, c))
                non_empty_rows.add(r)
                non_empty_cols.add(c)

    empty_rows = sorted(list(set(range(rows)) - non_empty_rows))
    empty_cols = sorted(set(range(cols)) - non_empty_cols)
    points = list(points)
    result = 0

    for i in range(len(points)):
        for j in range(i + 1, len(points)):
            r1, c1 = points[i]
            r2, c2 = points[j]

            min_row = min(r1, r2)
            max_row = max(r1, r2)
            min_col = min(c1, c2)
            max_col = max(c1, c2)

            result += (max_row - min_row) + (max_col - min_col)

            for empty_row in empty_rows:
                if empty_row > max_row:
                    break

                if min_row < empty_row:
                    result += 1000000 - 1  # Remove one to avoid double counting the original row

            for empty_col in empty_cols:
                if empty_col > max_col:
                    break

                if min_col < empty_col:
                    result += 1000000 - 1  # Remove one to avoid double counting the original column

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
