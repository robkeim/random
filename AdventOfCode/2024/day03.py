import re


def part1():
    lines = [line.strip() for line in open("day03.txt").readlines()]

    result = 0

    for line in lines:
        for match in re.findall(r"mul\((\d{1,3}),(\d{1,3})\)", line):
            result += int(match[0]) * int(match[1])

    print(result)


def part2():
    memory = " ".join(open("day03.txt").readlines())
    enabled = True
    result = 0
    i = 0

    while i < len(memory):
        if enabled:
            match = re.match(r"^mul\((\d{1,3}),(\d{1,3})\)", memory[i:])

            if match:
                result += int(match.group(1)) * int(match.group(2))

        if memory[i:].startswith("do()"):
            enabled = True

        if memory[i:].startswith("don't()"):
            enabled = False

        i += 1

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
