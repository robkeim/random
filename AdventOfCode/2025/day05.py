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
    lines = [line.strip() for line in open("day05.txt").readlines() if line.strip()]

    ranges = []

    for line in lines:
        if "-" in line:
            start, end = line.split("-")
            ranges.append((int(start), int(end)))

    found_overlap = True

    while found_overlap:
        found_overlap = False
        ranges = sorted(ranges)

        for i in range(len(ranges) - 1):
            left_start, left_end = ranges[i]
            right_start, right_end = ranges[i + 1]

            if right_start <= left_end:
                found_overlap = True
                del ranges[i]
                del ranges[i]
                ranges.append((left_start, max(left_end, right_end)))
                break

    result = 0

    for start, end in ranges:
        result += end - start + 1

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
