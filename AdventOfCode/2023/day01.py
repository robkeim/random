def part1():
    lines = [line.strip() for line in open("day01.txt").readlines()]
    digit_set = set("0123456789")

    result = 0

    for line in lines:
        digits = [letter for letter in line if letter in digit_set]
        result += int(digits[0] + digits[len(digits) - 1])

    print(result)


def part2():
    lines = [line.strip() for line in open("day01.txt").readlines()]
    digit_set = set("0123456789")

    replacements = [
        ("one", "1"),
        ("two", "2"),
        ("three", "3"),
        ("four", "4"),
        ("five", "5"),
        ("six", "6"),
        ("seven", "7"),
        ("eight", "8"),
        ("nine", "9"),
    ]

    result = 0

    for line in lines:
        first_digit = None
        last_digit = None

        for i in range(0, len(line)):
            if first_digit:
                break

            if line[i] in digit_set:
                first_digit = line[i]
                break

            for original, replacement in replacements:
                if line[i:].startswith(original):
                    first_digit = replacement
                    break

        for i in range(len(line) - 1, 0, -1):
            if last_digit:
                break

            if line[i] in digit_set:
                last_digit = line[i]
                break

            for original, replacement in replacements:
                if line[i:].startswith(original):
                    last_digit = replacement
                    break

        if not last_digit:
            last_digit = first_digit

        result += int(first_digit + last_digit)

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
