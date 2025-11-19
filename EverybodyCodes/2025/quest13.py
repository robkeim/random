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
    ranges = [line.strip() for line in open("quest13_p2.txt").readlines()]
    right = []
    left = []

    for i, num in enumerate(ranges):
        low, high = [int(value) for value in num.split("-")]
        segment = [value for value in range(low, high + 1)]

        if i % 2 == 0:
            right += segment
        else:
            left += segment

    clock = [1] + right + left[::-1]

    print(clock[20252025 % len(clock)])


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
