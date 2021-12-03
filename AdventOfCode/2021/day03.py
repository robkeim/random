from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day03.txt").readlines()]

    num_ones = defaultdict(int)

    for line in lines:
        for i, bit in enumerate(line):
            if bit == "1":
                num_ones[i] += 1

    half = len(lines) // 2

    gamma_rate = ""

    for i in range(len(lines[0])):
        if num_ones[i] > half:
            gamma_rate += "1"
        else:
            gamma_rate += "0"

    epsilon_rate = gamma_rate.replace("0", "2").replace("1", "0").replace("2", "1")

    result = int(gamma_rate, 2) * int(epsilon_rate, 2)

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
