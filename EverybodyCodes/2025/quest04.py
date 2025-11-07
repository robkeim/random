import math


def part1():
    gears = [int(line.strip()) for line in open("quest04_p1.txt").readlines()]

    multiplier = 1

    for i in range(len(gears) - 1):
        multiplier *= gears[i] / gears[i + 1]

    print(math.floor(multiplier * 2025))


def part2():
    gears = [int(line.strip()) for line in open("quest04_p2.txt").readlines()]

    multiplier = 1

    for i in range(len(gears) - 1):
        multiplier *= gears[i] / gears[i + 1]

    print(math.ceil(10000000000000 / multiplier))


def part3():
    gears = [line.strip() for line in open("quest04_p3.txt").readlines()]

    multiplier = 1

    for i in range(len(gears) - 1):
        first = gears[i]
        if "|" in first:
            first = int(first.split("|")[1])
        else:
            first = int(first)

        second = gears[i + 1]
        if "|" in second:
            second = int(second.split("|")[0])
        else:
            second = int(second)

        multiplier *= first / second

    print(math.floor(100 * multiplier))


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
