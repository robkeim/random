def part1():
    lines = [line.strip() for line in open("quest01_p1.txt").readlines()]
    names = lines[0].split(",")

    index = 0

    for instruction in lines[2].split(","):
        value = int(instruction[1:])
        if instruction[0] == "L":
            index = max(0, index - value)
        else:
            index = min(len(names) - 1, index + value)

    print(names[index])


def part2():
    lines = [line.strip() for line in open("quest01_p2.txt").readlines()]
    names = lines[0].split(",")

    index = 0

    for instruction in lines[2].split(","):
        value = int(instruction[1:])
        if instruction[0] == "L":
            index -= value
        else:
            index += value

    index %= len(names)
    index = (index + len(names)) % len(names)

    print(names[index])


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
