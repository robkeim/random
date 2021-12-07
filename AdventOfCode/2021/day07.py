import statistics


def part1():
    crabs = [int(value) for value in open("day07.txt").read().split(",")]

    median = int(statistics.median(crabs))

    result = 0

    for crab in crabs:
        result += abs(median - crab)

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
