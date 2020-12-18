import re


def part1():
    expressions = [line.strip() for line in open("day18.txt").readlines()]

    result = 0

    for expression in expressions:
        match = re.search("\(([^()]+)\)", expression)
        while match:
            expression = expression.replace(match.group(0), evaluate_expression(match.group(1)))
            match = re.search("\(([^()]+)\)", expression)

        expression = evaluate_expression(expression)

        result += int(expression)

    print(result)


def evaluate_expression(expression):
    values = expression.split(" ")

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
    expressions = [line.strip() for line in open("day18.txt").readlines()]

    result = 0

    for expression in expressions:
        match = re.search("\(([^()]+)\)", expression)
        while match:
            expression = expression.replace(match.group(0), evaluate_expression_with_reverse_order_of_operations(match.group(1)), 1)
            match = re.search("\(([^()]+)\)", expression)

        expression = evaluate_expression_with_reverse_order_of_operations(expression)

        result += int(expression)

    print(result)


def evaluate_expression_with_reverse_order_of_operations(equation):
    while "+" in equation:
        match = re.search("(\d+) \+ (\d+)", equation)

        equation = equation.replace(match.group(0), str(int(match.group(1)) + int(match.group(2))), 1)

    while "*" in equation:
        match = re.search("(\d+) \* (\d+)", equation)

        equation = equation.replace(match.group(0), str(int(match.group(1)) * int(match.group(2))), 1)

    return equation


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
