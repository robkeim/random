def part1():
    values = [int(line.strip()) for line in open("quest04_p1.txt").readlines()]
    min_value = min(values)
    print(sum([value - min_value for value in values]))


def part2():
    values = [int(line.strip()) for line in open("quest04_p2.txt").readlines()]
    min_value = min(values)
    print(sum([value - min_value for value in values]))


def part3():
    values = sorted([int(line.strip()) for line in open("quest04_p3.txt").readlines()])
    median = values[len(values) // 2]
    print(sum([abs(value - median) for value in values]))


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
