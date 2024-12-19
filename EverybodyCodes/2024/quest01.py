def part1():
    line = open("quest01_p1.txt").read().strip()

    print(line.count("B") + 3 * line.count("C"))


def part2():
    line = open("quest01_p2.txt").read().strip()

    answer = line.count("B") + 3 * line.count("C") + 5 * line.count("D")

    for index in range(0, len(line), 2):
        if "x" not in line[index:index + 2]:
            answer += 2

    print(answer)


def part3():
    line = open("quest01_p3.txt").read().strip()

    answer = line.count("B") + 3 * line.count("C") + 5 * line.count("D")

    for index in range(0, len(line), 3):
        num_creatures = 3 - line[index:index + 3].count("x")

        if num_creatures == 2:
            answer += 2
        elif num_creatures == 3:
            answer += 6

    print(answer)


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
