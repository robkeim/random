def part1():
    values = [int(line.strip()) for line in open("day01.txt").readlines()]
    values_set = set(values)

    for value in values:
        if 2020 - value in values_set:
            print(value * (2020 - value))
            return

    raise Exception("No solution found")


def part2():
    values = [int(line.strip()) for line in open("day01.txt").readlines()]
    values_dict = dict()

    for value1 in values:
        for value2 in values:
            values_dict[value1 + value2] = value1 * value2

    for value in values:
        if 2020 - value in values_dict:
            print(values_dict[2020 - value] * value)
            return

    raise Exception("No solution found")


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
