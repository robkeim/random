def part1():
    grid = [line.strip() for line in open("quest17_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "@":
                Xv, Yv = r, c

    result = 0
    R = 10

    for r in range(num_r):
        for c in range(num_c):
            Xc, Yc = r, c

            if (Xv - Xc) * (Xv - Xc) + (Yv - Yc) * (Yv - Yc) <= R * R and grid[r][c] != "@":
                result += int(grid[r][c])

    print(result)


def part2():
    grid = [line.strip() for line in open("quest17_p2.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    blocks = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "@":
                Xv, Yv = r, c
            else:
                blocks.add((r, c, int(grid[r][c])))

    R = 1
    max_blocks = 0
    max_r = 0

    while len(blocks) > 0:
        num_removed = 0
        next_blocks = set()

        for Xc, Yc, value in blocks:
            if (Xv - Xc) * (Xv - Xc) + (Yv - Yc) * (Yv - Yc) <= R * R:
                num_removed += value
            else:
                next_blocks.add((Xc, Yc, value))

        if num_removed > max_blocks:
            max_blocks = num_removed
            max_r = R

        blocks = next_blocks
        R += 1

    print(max_blocks * max_r)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
