def part1():
    values = [int(value) for value in open("quest05_p1.txt").read().strip().split(":")[1].split(",")]

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

    print("".join([str(row[1]) for row in fishbone]))


def part2():
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
