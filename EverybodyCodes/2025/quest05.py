def part1():
    print(calculate_quality(open("quest05_p1.txt").read().strip()))


def calculate_quality(line):
    values = [int(value) for value in line.split(":")[1].split(",")]
    fishbone = [[None, values[0], None]]

    for value in values[1:]:
        added = False
        for row in fishbone:
            if value < row[1] and row[0] is None:
                row[0] = value
                added = True
            elif value > row[1] and row[2] is None:
                row[2] = value
                added = True

            if added:
                break

        if not added:
            fishbone.append([None, value, None])

    return int("".join([str(row[1]) for row in fishbone]))


def part2():
    qualities = [calculate_quality(line) for line in open("quest05_p2.txt").readlines()]
    print(max(qualities) - min(qualities))


def part3():
    swords = sorted([calculate_sword(line) for line in open("quest05_p3.txt").readlines()], reverse=True)
    print(sum([(idx + 1) * value[2] for idx, value in enumerate(swords)]))


def calculate_sword(line):
    id_, rest = line.split(":")
    values = [int(value) for value in rest.split(",")]
    fishbone = [[None, values[0], None]]

    for value in values[1:]:
        added = False
        for row in fishbone:
            if value < row[1] and row[0] is None:
                row[0] = value
                added = True
            elif value > row[1] and row[2] is None:
                row[2] = value
                added = True

            if added:
                break

        if not added:
            fishbone.append([None, value, None])

    quality = int("".join([str(row[1]) for row in fishbone]))

    levels = []

    for row in fishbone:
        value = ""

        for item in row:
            if item is not None:
                value += str(item)

        levels.append(int(value))

    return quality, levels, int(id_)


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
