def part1():
    lines = [line.strip() for line in open("day04.txt").readlines()]

    result = 0

    for line in lines:
        winning_numbers, my_numbers = line.split(":")[1].split(" | ")
        winning_numbers = set([number for number in winning_numbers.split()])
        my_numbers = [number for number in my_numbers.split() if number in winning_numbers]

        result += int(2 ** (len(my_numbers) - 1))

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
