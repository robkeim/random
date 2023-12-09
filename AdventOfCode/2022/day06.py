def part1():
    stream = open("day06.txt").read().strip()
    print(first_unique_n(stream, 4))


def first_unique_n(stream, n):
    i = 0

    while len(set(stream[i:i + n])) != n:
        i += 1

    return i + n


def part2():
    stream = open("day06.txt").read().strip()
    print(first_unique_n(stream, 14))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
