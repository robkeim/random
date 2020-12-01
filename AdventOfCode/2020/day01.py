def part1():
    values = [int(value.strip()) for value in open("day01.txt").readlines()]
    values_set = set(values)

    for value in values:
        if 2020 - value in values_set:
            print(value * (2020 - value))
            return

    raise Exception("No solution found")


def part2():
    values = [int(value.strip()) for value in open("day01.txt").readlines()]

    for value1 in values:
        for value2 in values:
            for value3 in values:
                if value1 + value2 + value3 == 2020:
                    print(value1 * value2 * value3)
                    return

    raise Exception("No solution found")


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
