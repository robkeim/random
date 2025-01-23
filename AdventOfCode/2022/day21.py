def part1():
    lines = [line.strip() for line in open("day21.txt").readlines()]
    values = dict()
    formulas = dict()

    for line in lines:
        key, value = line.split(": ")
        value = value.split()

        if len(value) == 1:
            values[key] = int(value[0])
        else:
            formulas[key] = value

    def yell(key):
        if key in values:
            return values[key]

        left, operator, right = formulas[key]
        left = yell(left)
        right = yell(right)

        if operator == "+":
            return left + right
        elif operator == "-":
            return left - right
        elif operator == "*":
            return left * right
        elif operator == "/":
            return left // right
        else:
            raise Exception(f"Invalid operator: {operator}")

    print(yell("root"))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
