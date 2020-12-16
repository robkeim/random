import re


def part1():
    lines = [line.strip() for line in open("day16.txt").readlines()]

    valid_values = set()

    line_number = 0
    result = 0

    while line_number < len(lines):
        match = re.fullmatch(".+: (\d+)-(\d+) or (\d+)-(\d+)", lines[line_number])

        if match:
            for i in range(int(match.group(1)), int(match.group(2)) + 1):
                valid_values.add(i)

            for i in range(int(match.group(3)), int(match.group(4)) + 1):
                valid_values.add(i)
        elif lines[line_number] == "your ticket:":
            # Skip your ticket
            line_number += 2
            continue
        elif lines[line_number] == "nearby tickets:" or lines[line_number] == "":
            pass  # Nothing to do
        else:
            for value in lines[line_number].split(","):
                if int(value) not in valid_values:
                    result += int(value)

        line_number += 1

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
