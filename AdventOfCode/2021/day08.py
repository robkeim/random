def part1():
    lines = [line.split(" | ")[1].strip().split(" ") for line in open("day08.txt").readlines()]
    print(sum([len([word for word in line if len(word) in {2, 3, 4, 7}]) for line in lines]))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
