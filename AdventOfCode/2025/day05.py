def part1():
    lines = [line.strip() for line in open("day05.txt").readlines() if line.strip()]

    ranges = []
    result = 0

    for line in lines:
        if "-" in line:
            start, end = line.split("-")
            ranges.append((int(start), int(end)))
            continue

        value = int(line)

        for start, end in ranges:
            if start <= value <= end:
                result += 1
                break

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
