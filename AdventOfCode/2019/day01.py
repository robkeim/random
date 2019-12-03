def part1():
    lines = open("day01.txt").read().split()
    print(sum([get_fuel(int(mass)) for mass in lines]))


def part2():
    lines = open("day01.txt").read().split()
    print(sum([get_total_fuel(int(mass)) for mass in lines]))


def get_total_fuel(amount):
    total = 0

    while amount > 0:
        amount = get_fuel(amount)
        total += amount

    return total


def get_fuel(amount):
    return max(0, amount // 3 - 2)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
