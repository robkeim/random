def part1():
    secret_numbers = [int(line.strip()) for line in open("day22.txt").readlines()]

    print(sum([get_next_secret_number(num, 2000) for num in secret_numbers]))


def get_next_secret_number(number, num_iterations):
    for _ in range(num_iterations):
        number = ((number * 64) ^ number) % 16_777_216
        number = ((number // 32) ^ number) % 16_777_216
        number = ((number * 2048) ^ number) % 16_777_216

    return number


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
