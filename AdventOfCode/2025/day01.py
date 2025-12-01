def part1():
    lines = [line.strip() for line in open("day01.txt").readlines()]
    dial_pos = 50
    password = 0

    for line in lines:
        direction = line[0]
        amount = int(line[1:])
        amount %= 100

        if direction == "L":
            amount *= -1

        dial_pos = (dial_pos + amount + 100) % 100

        if dial_pos == 0:
            password += 1

    print(password)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
