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
    lines = [line.strip() for line in open("day14.txt").readlines()]

    template = lines[0]
    lines = lines[2:]
    rules = dict()

    for line in lines:
        source, replacement = line.split(" -> ")

        rules[source] = replacement

    counts = Counter()

    for i in range(0, len(template) - 1):
        counts[template[i:i + 2]] += 1

    for _ in range(40):
        counts_next = Counter()

        for key in counts:
            replacement = rules[key]
            # If the rule is AB -> C then AB is transformed into AC and CB
            counts_next[key[0] + replacement] += counts[key]
            counts_next[replacement + key[1]] += counts[key]

        counts = counts_next

    letter_counts = Counter()

    for key in counts:
        letter_counts[key[0]] += counts[key]

    # Add the last character of the string (after taking only the first character of each pair)
    letter_counts[template[-1]] += 1

    print(max(letter_counts.values()) - min(letter_counts.values()))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
