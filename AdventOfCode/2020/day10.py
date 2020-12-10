def part1():
    volts = sorted([int(line.strip()) for line in open("day10.txt").readlines()])
    volts = [0] + volts + [volts[-1] + 3]

    one = three = 0

    for i in range(1, len(volts)):
        diff = volts[i] - volts[i - 1]

        if diff == 1:
            one += 1
        elif diff == 3:
            three += 1

    print(one * three)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
