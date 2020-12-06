def part1():
    print(sum([len(set(line.replace("\n", ""))) for line in open("day06.txt").read().split("\n\n")]))


def part2():
    groups = [line.split("\n") for line in open("day06.txt").read().strip().split("\n\n")]
    result = 0

    for group in groups:
        intersection = set(group[0])

        for person in group[1:]:
            intersection = intersection.intersection(person)

        result += len(intersection)

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
