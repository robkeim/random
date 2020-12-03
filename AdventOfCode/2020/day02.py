from collections import Counter
import re


def part1():
    lines = [line.strip() for line in open("day02.txt").readlines()]
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
    lines = [line.strip() for line in open("day02.txt").readlines()]
    valid_passwords = 0

    for line in lines:
        match = re.match("(\d+)-(\d+) ([a-z]): ([a-z]+)", line)

        if not match:
            raise Exception("Invalid input line: " + line)

        first_index = int(match.group(1)) - 1
        second_index = int(match.group(2)) - 1
        letter = match.group(3)
        password = match.group(4)

        num_matches = 0

        if first_index < len(password) and password[first_index] == letter:
            num_matches += 1

        if second_index < len(password) and password[second_index] == letter:
            num_matches += 1

        if num_matches == 1:
            valid_passwords += 1

    print(valid_passwords)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
