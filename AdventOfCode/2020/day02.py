from collections import Counter
import re


def part1():
    lines = [value.strip() for value in open("day02.txt").readlines()]
    valid_passwords = 0

    for line in lines:
        match = re.match("(\d+)-(\d+) ([a-z]): ([a-z]+)", line)

        if not match:
            raise Exception("Invalid input line: " + line)

        min_occurrences = int(match.group(1))
        max_occurrences = int(match.group(2))
        letter = match.group(3)
        password = Counter(match.group(4))

        if min_occurrences <= password[letter] <= max_occurrences:
            valid_passwords += 1

    print(valid_passwords)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
