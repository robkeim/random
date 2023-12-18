def part1():
    lines = [line.split() for line in open("day18.txt").readlines()]
    border = {(0, 0)}
    row = 0
    col = 0
    dirs = {
        "R": (0, 1),
        "D": (1, 0),
        "L": (0, -1),
        "U": (-1, 0)
    }

    min_row = 0
    max_row = 0
    min_col = 0
    max_col = 0
    for direction, num, _ in lines:
        dr, dc = dirs[direction]

        for _ in range(int(num)):
            row += dr
            col += dc
            border.add((row, col))
            min_row = min(min_row, row)
            min_col = min(min_col, col)
            max_row = max(max_row, row)
            max_col = max(max_col, col)

    outside = set()

    for row in range(min_row, max_row + 1):
        flood_fill(border, outside, min_row, max_row, min_col, max_col, row, min_col)
        flood_fill(border, outside, min_row, max_row, min_col, max_col, row, max_col)

    for col in range(min_col, max_col + 1):
        flood_fill(border, outside, min_row, max_row, min_col, max_col, min_row, col)
        flood_fill(border, outside, min_row, max_row, min_col, max_col, max_row, col)

    ans = 0

    for row in range(min_row, max_row + 1):
        for col in range(min_col, max_col + 1):
            if (row, col) not in outside:
                ans += 1

    print(ans)


def print_grid(border, outside, min_row, max_row, min_col, max_col):
    for row in range(min_row, max_row + 1):
        row_to_print = ""
        for col in range(min_col, max_col + 1):
            if (row, col) in border:
                row_to_print += "#"
            elif (row, col) in outside:
                row_to_print += "."
            else:
                row_to_print += " "

        print(row_to_print)


def flood_fill(border, outside, min_row, max_row, min_col, max_col, start_row, start_col):
    if (start_row, start_col) in border:
        return

    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    to_process = [(start_row, start_col)]

    while len(to_process) > 0:
        row, col = to_process.pop()
        outside.add((row, col))

        for dr, dc in dirs:
            next_row = row + dr
            next_col = col + dc

            if min_row <= next_row <= max_row and min_col <= next_col <= max_col and (next_row, next_col) not in border and (next_row, next_col) not in outside:
                to_process.append((next_row, next_col))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
