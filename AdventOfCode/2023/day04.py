from functools import lru_cache


def part1():
    lines = [line.strip() for line in open("day04.txt").readlines()]

    result = 0

    for line in lines:
        winning_numbers, my_numbers = line.split(":")[1].split(" | ")
        winning_numbers = set([number for number in winning_numbers.split()])
        my_numbers = [number for number in my_numbers.split() if number in winning_numbers]

        result += int(2 ** (len(my_numbers) - 1))

    print(result)


cards_won = dict()


def part2():
    lines = [line.strip() for line in open("day04.txt").readlines()]

    for index, line in enumerate(lines):
        winning_numbers, my_numbers = line.split(":")[1].split(" | ")
        winning_numbers = set([number for number in winning_numbers.split()])
        my_numbers = [number for number in my_numbers.split() if number in winning_numbers]

        cards_won[index] = len(my_numbers)

    result = 0

    for i in range(len(lines)):
        result += 1 + part2helper(i)

    print(result)


@lru_cache(maxsize=1000)
def part2helper(index):
    result = 0

    for i in range(cards_won[index]):
        result += 1 + part2helper(index + i + 1)

    return result


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
