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
    grid = [list(line.strip()) for line in open("quest10_p2.txt").readlines()]
    power = 0
    r_offset = 0

    while r_offset < len(grid):
        c_offset = 0

        while c_offset < len(grid[0]):
            word = ""
            for r in range(2, 6):
                for c in range(2, 6):
                    row = grid[r_offset + r][c_offset] + grid[r_offset + r][c_offset + 1] + grid[r_offset + r][c_offset + 6] + grid[r_offset + r][c_offset + 7]
                    col = grid[r_offset][c_offset + c] + grid[r_offset + 1][c_offset + c] + grid[r_offset + 6][c_offset + c] + grid[r_offset + 7][c_offset + c]
                    word += list(set(row).intersection(set(col)))[0]

            power += sum([(i + 1) * (ord(letter) - ord("A") + 1) for i, letter in enumerate(word)])

            c_offset += 9

        r_offset += 9

    print(power)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
