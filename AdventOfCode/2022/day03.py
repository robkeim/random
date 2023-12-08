def part1():
    lines = [line.strip() for line in open("day03.txt").readlines()]

    result = 0

    for line in lines:
        half = len(line) // 2
        overlap = list(set(line[:half]) & set(line[half:]))[0]

        if "a" <= overlap <= "z":
            result += ord(overlap) - ord("a") + 1
        else:
            result += ord(overlap) - ord("A") + 27

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
