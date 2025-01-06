from functools import lru_cache


def part1():
    lines = [line.strip() for line in open("day19.txt").readlines()]
    unsolved = dict()
    solved = dict()
    potential_matches = []

    for line in lines:
        if ":" not in line:
            potential_matches.append(line)
            continue

        split = line.split(":")

        if "\"" in line:
            solved[split[0]] = [split[1].strip().strip("\"")]
            continue

        unsolved[split[0]] = split[1].strip().split(" | ")

    while len(unsolved) > 0:
        for key in list(unsolved):
            solution_found = True
            results = []

            for solution in unsolved[key]:
                tmp_results = [""]

                for value in solution.split(" "):
                    tmp_results2 = []
                    for tmp_result in tmp_results:
                        if value in solved:
                            for replacement in solved[value]:
                                tmp_results2.append(tmp_result + " " + replacement)
                        elif value.isalpha():
                            tmp_results2.append(tmp_result + " " + value)
                        else:
                            solution_found = False
                            tmp_results2.append(tmp_result + " " + value)

                    tmp_results = [result.strip() for result in tmp_results2]

                results += tmp_results

            if solution_found:
                solved[key] = results
                del unsolved[key]
            else:
                unsolved[key] = results

    solutions = set([value.replace(" ", "") for value in solved["0"]])

    result = 0

    for potential_match in potential_matches:
        if potential_match in solutions:
            result += 1

    print(result)


def part2():
    constants, rules, messages = parse_input()

    @lru_cache(4_000_000)
    def match_list(message, cur_rules):
        if not message and not cur_rules:
            return True

        if not message or not cur_rules:
            return False

        for i in range(len(message) + 1):
            first = message[:i]
            remaining = message[i:]

            if match(first, cur_rules[0]) and match_list(remaining, cur_rules[1:]):
                return True

        return False

    @lru_cache(4_000_000)
    def match(message, rule):
        if rule in constants:
            return constants[rule] == message

        for option in rules[rule]:
            if match_list(message, tuple(option)):
                return True

        return False

    num_matches = 0

    for message in messages:
        match.cache_clear()
        match_list.cache_clear()

        if match(message, "0"):
            num_matches += 1

    print(num_matches)


def parse_input():
    lines = [line.strip() for line in open("day19.txt").readlines()]
    constants = dict()
    rules = dict()
    messages = []

    for line in lines:
        if ":" in line:
            if line.startswith("8:"):
                line = "8: 42 | 42 8"

            if line.startswith("11:"):
                line = "11: 42 31 | 42 11 31"

            key, rest = line.split(":")

            if "\"" in rest:
                constants[key] = rest.strip().strip("\"")
            else:
                rules[key] = [part.split() for part in rest.split("|")]
        elif line:
            messages.append(line)

    return constants, rules, messages


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
