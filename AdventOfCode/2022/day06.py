def part1():
    stream = open("day06.txt").read().strip()
    i = 0

    while len(set(stream[i:i + 4])) != 4:
        i += 1

    print(i + 4)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
