import re


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
    lines = [line.strip() for line in open("day19.txt").readlines()]
    rules = dict()
    potential_matches = []

    for line in lines:
        if ":" not in line:
            potential_matches.append(line)
            continue

        split = line.split(":")

        if "\"" in line:
            rules[split[0]] = split[1].strip().strip("\"")
            continue

        value = split[1].strip()

        if "|" in value:
            value = "( " + value + " )"

        rules[split[0]] = value

    # 8: 42 | 42 8
    # 11: 42 31 | 42 11 31
    rules["8"] = "( 42 )+"
    rules["11"] = "42 ( 42 31 )* 31"

    finished = False

    while not finished:
        finished = True

        for key in rules:
            values = rules[key].split(" ")

            for index, value in enumerate(values):
                if value.isnumeric():
                    values[index] = rules[value]
                    finished = False

            rules[key] = " ".join(values)

    print(rules)

    for key in rules:
        rules[key] = rules[key].replace(" ", "")

    count = 0

    for item in potential_matches:
        for rule in rules:
            if re.fullmatch(rules[rule], item):
                print(rules[rule], item)
                count += 1
                break

    print(count)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
