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
    mappings = open("day19.txt").read().split("\n\n")[0].split()
    rules = defaultdict(list)

    # Parse mappings
    for mapping in mappings:
        match = re.match(r"([a-z]+)\{(.+)\}", mapping)
        key = match.group(1)

        for rule in match.group(2).split(","):
            match = re.match(r"([xmas])([<>])(\d+):(([a-z]+)|A|R)", rule)

            if not match:
                rules[key].append(rule)
                continue

            rules[key].append([match.group(1), match.group(2), int(match.group(3)), match.group(4)])

    to_process = [("in", [])]
    successes = []

    while len(to_process) > 0:
        cur_rule, constraints = to_process.pop()

        if cur_rule == "R":
            continue

        if cur_rule == "A":
            successes.append(constraints)
            continue

        new_constraints = []

        for rule in rules[cur_rule]:
            if not isinstance(rule, list):
                to_process.append((rule, list(constraints) + list(new_constraints)))
            else:
                item, operator, value, dest = rule

                to_process.append((dest, list(constraints) + list(new_constraints) + [(item, operator, value)]))
                new_constraints.append(invert_logic(item, operator, value))

    result = 0

    for constraints in successes:
        mins = {
            "x": 1,
            "m": 1,
            "a": 1,
            "s": 1
        }

        maxes = {
            "x": 4000,
            "m": 4000,
            "a": 4000,
            "s": 4000
        }

        for item, operator, value in constraints:
            if operator == ">":
                mins[item] = max(mins[item], value + 1)
            else:
                maxes[item] = min(maxes[item], value - 1)

        product = 1

        for char in "xmas":
            product *= maxes[char] - mins[char] + 1

        result += product

    print(result)


def invert_logic(item, operator, value):
    if operator == "<":
        return item, ">", value - 1
    else:
        return item, "<", value + 1


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
