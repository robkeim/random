def part1():
    grid = [line.strip() for line in open("day04.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    rolls = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "@":
                rolls.add((r, c))

    accessible = 0

    for (r, c) in rolls:
        num_neighbors = 0

        for dr in [-1, 0, 1]:
            for dc in [-1, 0, 1]:
                if (dr != 0 or dc != 0) and (r + dr, c + dc) in rolls:
                    num_neighbors += 1

        if num_neighbors < 4:
            accessible += 1

    print(accessible)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
