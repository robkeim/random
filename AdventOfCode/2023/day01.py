import re


def part1():
    lines = [line.strip() for line in open("day01.txt").readlines()]

    result = 0

    for line in lines:
        matches = re.findall("([0-9])", line)
        result += int(matches[0] + matches[len(matches) - 1])

    print(result)


def part2():
    lines = [line.strip() for line in open("day01.txt").readlines()]

    replacements = {
        "one": "1",
        "two": "2",
        "three": "3",
        "four": "4",
        "five": "5",
        "six": "6",
        "seven": "7",
        "eight": "8",
        "nine": "9",
        "1": "1",
        "2": "2",
        "3": "3",
        "4": "4",
        "5": "5",
        "6": "6",
        "7": "7",
        "8": "8",
        "9": "9"
    }
    regex = "([0-9]|one|two|three|four|five|six|seven|eight|nine)"

    result = 0

    for line in lines:
        first_digit = replacements[re.search(regex, line)[0]]

        for i in range(len(line) - 1, -1, -1):
            match = re.search(regex, line[i:])

            if match:
                last_digit = replacements[match[0]]
                break

        result += int(first_digit + last_digit)

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
