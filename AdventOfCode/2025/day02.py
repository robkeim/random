def part1():
    ranges = [line.strip() for line in open("day02.txt").read().split(",")]

    result = 0

    for start_and_end in ranges:
        low, high = start_and_end.split("-")

        for num in range(int(low), int(high) + 1):
            num_str = str(num)

            if len(num_str) % 2 != 0:
                continue

            left = num_str[:len(num_str) // 2]
            right = num_str[len(num_str) // 2:]

            if left == right:
                result += num

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
