from collections import defaultdict


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
    secret_numbers = [int(line.strip()) for line in open("day22.txt").readlines()]
    num_bananas = defaultdict(int)

    for secret_number in secret_numbers:
        seen = set()
        deltas, values = get_secret_sequence(secret_number, 2000)

        for i in range(4, len(deltas) + 1):
            key = tuple(deltas[i - 4:i])

            if key not in seen:
                seen.add(key)
                num_bananas[key] += values[i - 1]

    print(max(num_bananas.values()))


def get_secret_sequence(number, num_iterations):
    deltas = []
    values = []
    prev = number % 10

    for _ in range(num_iterations):
        number = ((number * 64) ^ number) % 16_777_216
        number = ((number // 32) ^ number) % 16_777_216
        number = ((number * 2048) ^ number) % 16_777_216

        cur = number % 10
        deltas.append(cur - prev)
        values.append(cur)
        prev = cur

    return deltas, values


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
