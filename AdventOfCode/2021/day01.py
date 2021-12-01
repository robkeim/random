import sys


def part1():
    lines = [int(line.strip()) for line in open("day01.txt").readlines()]
    lines = [lines[0] + 1] + lines

    result = 0

    for i in range(len(lines) - 1):
        if lines[i] < lines[i + 1]:
            result += 1

    print(result)


def part2():
    lines = [int(line.strip()) for line in open("day01.txt").readlines()]

    result = 0
    prev_window = sys.maxsize

    for i in range(len(lines) - 2):
        window = sum(lines[i:i + 3])

        if window > prev_window:
            result += 1

        prev_window = window

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
