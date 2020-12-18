import re


def part1():
    equations = [line.strip() for line in open("day18.txt").readlines()]

    result = 0

    for equation in equations:
        match = re.search("\(([^()]+)\)", equation)
        while match:
            equation = equation.replace(match.group(0), calculate(match.group(1)))
            match = re.search("\(([^()]+)\)", equation)

        equation = calculate(equation)

        result += int(equation)

    print(result)


def calculate(value):
    values = value.split(" ")

    result = int(values[0])

    index = 1

    while index < len(values):
        if values[index] == "+":
            result += int(values[index + 1])
        elif values[index] == "*":
            result *= int(values[index + 1])
        else:
            raise Exception("Unknown operator: " + values[index])

        index += 2

    return str(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
