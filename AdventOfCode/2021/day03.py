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
    lines = [line.strip() for line in open("day03.txt").readlines()]

    oxygen_generator_rating = calculate_life_support_criteria(
        lines,
        lambda num_zeros, num_ones: "1" if num_ones >= num_zeros else "0"
    )

    co2_scrubber_rating = calculate_life_support_criteria(
        lines,
        lambda num_zeros, num_ones: "0" if num_zeros <= num_ones else "1"
    )

    print(oxygen_generator_rating * co2_scrubber_rating)


def calculate_life_support_criteria(lines, to_keep_fn):
    cur_bit = 0

    while len(lines) > 1:
        num_zeros, num_ones = get_counts_in_column(lines, cur_bit)
        to_keep = to_keep_fn(num_zeros, num_ones)

        lines = [line for line in lines if line[cur_bit] == to_keep]

        cur_bit += 1

    return int(lines[0], 2)


def get_counts_in_column(lines, column):
    num_zeros = num_ones = 0

    for line in lines:
        if line[column] == "0":
            num_zeros += 1
        else:
            num_ones += 1

    return num_zeros, num_ones


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
