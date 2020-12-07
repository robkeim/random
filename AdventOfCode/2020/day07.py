import re
from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day07.txt").readlines()]

    bags = defaultdict(list)

    for line in lines:
        match = re.fullmatch("([a-z ]+) bags contain ([^.]+).", line)

        if not match:
            raise Exception("Wrongly formatted line: " + line)

        bag = match.group(1)
        contains = match.group(2).split(", ")

        for contain in contains:
            if contain == "no other bags":
                bags[bag] = []
                continue

            match = re.fullmatch("\d+ ([a-z ]+) bags?", contain)

            if not match:
                raise Exception("Wrongly formatted line: " + line)

            bags[bag].append(match.group(1))

    result = 0

    for bag in bags:
        to_process = bags[bag][:]

        while len(to_process) > 0:
            cur = to_process.pop(0)

            if cur == "shiny gold":
                result += 1
                break
            to_process += bags[cur][:]

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
