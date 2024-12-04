from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day04.txt").readlines()]
    num_r = len(lines)
    num_c = len(lines[0])
    grid = defaultdict(str)
    result = 0

    for r in range(num_r):
        for c in range(num_c):
            grid[(r, c)] = lines[r][c]

    target_word = "XMAS"

    for r in range(num_r):
        for c in range(num_c):
            for dr in range(-1, 2):
                for dc in range(-1, 2):
                    match = True

                    for i in range(len(target_word)):
                        if grid[(r + dr * i, c + dc * i)] != target_word[i]:
                            match = False
                            break

                    if match:
                        result += 1

    print(result)


def part2():
    lines = [line.strip() for line in open("day04.txt").readlines()]
    num_r = len(lines)
    num_c = len(lines[0])
    grid = defaultdict(str)
    result = 0

    for r in range(num_r):
        for c in range(num_c):
            grid[(r, c)] = lines[r][c]

    for r in range(num_r):
        for c in range(num_c):
            if grid[(r, c)] != "A":
                continue

            top_left = (grid[(r - 1, c - 1)], grid[(r + 1, c + 1)])
            bottom_left = (grid[(r - 1, c + 1)], grid[(r + 1, c - 1)])

            if (top_left == ("M", "S") or top_left == ("S", "M")) and (bottom_left == ("M", "S") or bottom_left == ("S", "M")):
                result += 1

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
