def part1():
    blocks = [int(value) for value in open("quest16_p1.txt").read().strip().split(",")]

    result = 0

    for i in range(1, 91):
        for block in blocks:
            if i % block == 0:
                result += 1

    print(result)


def part2():
    blocks = [int(value) for value in open("quest16_p2.txt").read().strip().split(",")]

    result = 1

    while True:
        found = False

        for i in range(len(blocks)):
            if blocks[i] == 0:
                continue

            found = True

            multiplier = i + 1
            result *= multiplier

            value = multiplier

            while value <= len(blocks):
                blocks[value - 1] -= 1
                value += multiplier

        if not found:
            break

    print(result)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
