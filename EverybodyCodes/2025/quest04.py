import math


def part1():
    gears = [int(line.strip()) for line in open("quest04_p1.txt").readlines()]

    multiplier = 1

    for i in range(len(gears) - 1):
        multiplier *= gears[i] / gears[i + 1]

    print(math.floor(multiplier * 2025))


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
