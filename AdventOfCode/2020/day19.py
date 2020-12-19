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
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
