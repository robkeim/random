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
            return left / right
        else:
            raise Exception(f"Invalid operator: {operator}")

    key1, _, key2 = formulas["root"]
    needed = yell(key2) # This part isn't generic because it assumes "humn" is on the left side of the equation

    low = 0
    high = 1
    values["humn"] = high

    while yell(key1) > needed:
        high *= 2
        values["humn"] = high

    while low <= high:
        mid = (low + high) // 2

        values["humn"] = mid
        result = yell(key1)

        if result < needed:
            high = mid - 1
        elif result > needed:
            low = mid + 1
        else:
            print(mid)
            break


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
