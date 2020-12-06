def part1():
    print(sum([len(set(line.replace("\n", ""))) for line in open("day06.txt").read().split("\n\n")]))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
