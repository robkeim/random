def part1():
    numbers = [int(line.strip()) for line in open("quest13_p1.txt").readlines()]
    right = []
    left = []

    for i, num in enumerate(numbers):
        if i % 2 == 0:
            right.append(num)
        else:
            left.append(num)

    clock = [1] + right + left[::-1]

    print(clock[2025 % len(clock)])


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
