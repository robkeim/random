import re
from collections import defaultdict


def part1():
    mappings, parts = open("day19.txt").read().split("\n\n")
    rules = defaultdict(list)

    # Parse mappings
    for mapping in mappings.split():
        match = re.match(r"([a-z]+)\{(.+)\}", mapping)
        key = match.group(1)

        for rule in match.group(2).split(","):
            match = re.match(r"([xmas])([<>])(\d+):(([a-z]+)|A|R)", rule)

            if not match:
                rules[key].append(rule)
                continue

            rules[key].append([match.group(1), match.group(2), int(match.group(3)), match.group(4)])

    # Process parts
    items = {
        "x": 0,
        "m": 1,
        "a": 2,
        "s": 3
    }

    result = 0

    for part in parts.split():
        xmas = [int(item.split("=")[1]) for item in part[1:-1].split(",")]

        cur_rule = "in"

        while True:
            if cur_rule == "A" or cur_rule == "R":
                break

            for rule in rules[cur_rule]:
                if not isinstance(rule, list):
                    cur_rule = rule
                    break

                item, operator, value, dest = rule

                if operator == "<" and xmas[items[item]] < value:
                    cur_rule = dest
                    break
                elif operator == ">" and xmas[items[item]] > value:
                    cur_rule = dest
                    break
                else:
                    pass  # Move onto the next rule

        if cur_rule == "A":
            result += sum(xmas)

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
