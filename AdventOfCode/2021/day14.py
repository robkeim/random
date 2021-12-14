import sys
from collections import Counter


def part1():
    lines = [line.strip() for line in open("day14.txt").readlines()]

    template = lines[0]
    lines = lines[2:]

    rules = dict()

    for line in lines:
        source, replacement = line.split(" -> ")

        rules[source] = replacement

    for iteration in range(10):
        next_template = ""

        for i in range(len(template) - 1):
            next_template += template[i] + rules[template[i:i + 2]]

        next_template += template[len(template) - 1]

        template = next_template

    counter = Counter(template)

    min_key = ""
    min_value = sys.maxsize
    max_key = ""
    max_value = 0

    for key in counter:
        if counter[key] < min_value:
            min_key = key
            min_value = counter[key]

        if counter[key] > max_value:
            max_key = key
            max_value = counter[key]

    print(counter[max_key] - counter[min_key])


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
