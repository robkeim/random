def part1():
    nums = [line.strip() for line in open("day03.txt").readlines()]

    result = 0

    for num_str in nums:
        max_num = 0

        for i in range(len(num_str)):
            for j in range(i + 1, len(num_str)):
                max_num = max(int(num_str[i] + num_str[j]), max_num)

        result += max_num

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
