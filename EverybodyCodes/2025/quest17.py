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
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
