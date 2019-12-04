def part1():
    minimum, maximum = [int(value) for value in open("day04.txt").read().split("-")]

    print(len([i for i in range(minimum, maximum + 1) if valid_password(i)]))


def part2():
    minimum, maximum = [int(value) for value in open("day04.txt").read().split("-")]

    print(len([i for i in range(minimum, maximum + 1) if valid_password_only_double(i)]))


def valid_password(password):
    password = str(password)
    double = decrease = False

    for i in range(len(password) - 1):
        if password[i] == password[i + 1]:
            double = True

        if password[i] > password[i + 1]:
            decrease = True

    return double and not decrease


def valid_password_only_double(password):
    password = str(password)
    double = decrease = False

    for i in range(len(password) - 1):
        if password[i] == password[i + 1]:  # Current two characters are the same
            if i == 0 or password[i - 1] != password[i]:  # Character before is different
                if i + 2 >= len(password) or password[i + 2] != password[i]:  # Character after is different
                    double = True

        if password[i] > password[i + 1]:
            decrease = True

    return double and not decrease


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
