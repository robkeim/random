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
    grid = [line.strip() for line in open("day04.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    rolls = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "@":
                rolls.add((r, c))

    total_removed = 0

    while True:
        removed, rolls = remove_rolls(rolls)

        if removed == 0:
            break

        total_removed += removed

    print(total_removed)


def remove_rolls(rolls):
    num_removed = 0
    new_rolls = set()

    for (r, c) in rolls:
        num_neighbors = 0

        for dr in [-1, 0, 1]:
            for dc in [-1, 0, 1]:
                if (dr != 0 or dc != 0) and (r + dr, c + dc) in rolls:
                    num_neighbors += 1

        if num_neighbors < 4:
            num_removed += 1
        else:
            new_rolls.add((r, c))

    return num_removed, new_rolls

def print_grid(rolls, num_r, num_c):
    for r in range(num_r):
        row = ""
        for c in range(num_c):
            if (r,c) in rolls:
                row += "@"
            else:
                row += "."

        print(row)

def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
