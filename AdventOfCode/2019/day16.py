def part1():
    digits = [int(d) for d in open("day16.txt").read()]

    for i in range(100):
        digits = run_phase(digits)

    print("".join(str(d) for d in digits)[:8])


def run_phase(sequence):
    result = []

    for level in range(1, len(sequence) + 1):
        pattern = generate_pattern(len(sequence), level)
        result.append(extract_ones_digit(sum([value[0] * value[1] for value in zip(sequence, pattern)])))

    return result


def generate_pattern(length, level):
    result = []
    while len(result) <= length:
        for value in [0, 1, 0, -1]:
            result += [value] * level

    return result[1:-1 * (len(result) - length - 1)]


def extract_ones_digit(value):
    return int(str(value)[-1])


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
