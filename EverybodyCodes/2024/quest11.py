import sys
from collections import defaultdict
from functools import lru_cache


def part1():
    lines = [line.strip() for line in open("quest11_p1.txt").readlines()]
    pairings = defaultdict(list)

    for line in lines:
        start, end = line.split(":")
        for item in end.split(","):
            pairings[start].append(item)

    print(expand("A", 4, pairings))


def expand(termite, num_days, pairings):
    if num_days == 0:
        return 1

    return sum([expand(value, num_days - 1, pairings) for value in pairings[termite]])


def part2():
    lines = [line.strip() for line in open("quest11_p2.txt").readlines()]
    pairings = defaultdict(list)

    for line in lines:
        start, end = line.split(":")
        for item in end.split(","):
            pairings[start].append(item)

    print(expand("Z", 10, pairings))


pairings_part3 = defaultdict(list)


def part3():
    lines = [line.strip() for line in open("quest11_p3.txt").readlines()]

    for line in lines:
        start, end = line.split(":")
        for item in end.split(","):
            pairings_part3[start].append(item)

    min_val = sys.maxsize
    max_val = 0

    for termite in pairings_part3:
        result = cached_expand(termite, 20)
        min_val = min(min_val, result)
        max_val = max(max_val, result)

    print(max_val - min_val)


@lru_cache(1_000_000)
def cached_expand(termite, num_days):
    if num_days == 0:
        return 1

    return sum([cached_expand(value, num_days - 1) for value in pairings_part3[termite]])


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
