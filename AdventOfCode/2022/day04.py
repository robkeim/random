def part1():
    lines = [line.strip() for line in open("day04.txt").readlines()]

    result = 0

    for line in lines:
        first, second = line.split(",")
        first = [int(value) for value in first.split("-")]
        second = [int(value) for value in second.split("-")]

        if (first[0] <= second[0] and second[1] <= first[1]) or (second[0] <= first[0] and first[1] <= second[1]):
            result += 1

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
