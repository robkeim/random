def part1():
    grid = [line.strip() for line in open("day25.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    east = set()
    south = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == ">":
                east.add((r, c))
            elif grid[r][c] == "v":
                south.add((r, c))

    num_steps = 0

    while True:
        num_steps += 1

        next_east = set()

        for (r, c) in east:
            next_c = (c + 1) % num_c

            if (r, next_c) in east or (r, next_c) in south:
                next_east.add((r, c))
            else:
                next_east.add((r, next_c))

        next_south = set()

        for (r, c) in south:
            next_r = (r + 1) % num_r

            if (next_r, c) in next_east or (next_r, c) in south:
                next_south.add((r, c))
            else:
                next_south.add((next_r, c))

        if east == next_east and south == next_south:
            print(num_steps)
            break

        east = next_east
        south = next_south


def print_grid(num_r, num_c, east, south):
    for r in range(num_r):
        row = ""

        for c in range(num_c):
            if (r, c) in east:
                row += ">"
            elif (r, c) in south:
                row += "v"
            else:
                row += "."

        print(row)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
