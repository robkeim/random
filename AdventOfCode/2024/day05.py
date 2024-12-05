def part1():
    input = [line.strip() for line in open("day05.txt").readlines()]
    rules = []
    lines = []
    result = 0

    for line in input:
        if "|" in line:
            rules.append(line.split("|"))

        if "," in line:
            lines.append(line.split(","))

    for line in lines:
        found_error = False

        for first, second in rules:
            first_index = line.index(first) if first in line else None
            second_index = line.index(second) if second in line else None

            if first_index is not None and second_index is not None and first_index > second_index:
                found_error = True
                break

        if not found_error:
            result += int(line[len(line) // 2])

    print(result)


def part2():
    input = [line.strip() for line in open("day05.txt").readlines()]
    rules = []
    lines = []
    result = 0

    for line in input:
        if "|" in line:
            rules.append(line.split("|"))

        if "," in line:
            # Add padding on either side to simplify replacements for the first and last items of the list
            lines.append(["X"] + line.split(",") + ["X"])

    for line in lines:
        found_error = True
        made_swap = False

        while found_error:
            found_error = False

            for first, second in rules:
                first_index = line.index(first) if first in line else None
                second_index = line.index(second) if second in line else None

                if first_index is not None and second_index is not None and first_index > second_index:
                    found_error = True
                    made_swap = True
                    line = line[:second_index] + [line[first_index]] + line[second_index:first_index] + line[first_index + 1:]

        if made_swap:
            result += int(line[len(line) // 2])

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
