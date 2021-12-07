import statistics
import sys


def part1():
    crabs = [int(value) for value in open("day07.txt").read().split(",")]

    median = int(statistics.median(crabs))

    result = 0

    for crab in crabs:
        result += abs(median - crab)

    print(result)


def part2():
    crabs = [int(value) for value in open("day07.txt").read().split(",")]

    min_crab = min(crabs)
    max_crab = max(crabs)
    result = sys.maxsize

    for i in range(min_crab, max_crab + 1):
        result = min(result, calc_solution(crabs, i))

    print(result)


def calc_solution(crabs, index):
    result = 0

    for crab in crabs:
        n = abs(index - crab)
        result += n * (n + 1) // 2

    return result


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
