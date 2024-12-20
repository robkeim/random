def part1():
    grid = [list(line.strip()) for line in open("quest10_p1.txt").readlines()]
    result = ""

    for r in range(2, 6):
        for c in range(2, 6):
            row = grid[r][0] + grid[r][1] + grid[r][-1] + grid[r][-2]
            col = grid[0][c] + grid[1][c] + grid[-1][c] + grid[-2][c]
            result += list(set(row).intersection(set(col)))[0]

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
