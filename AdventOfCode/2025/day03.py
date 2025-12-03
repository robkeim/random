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
    nums = [line.strip() for line in open("day03.txt").readlines()]

    result = 0

    for num_str in nums:
        num = ""
        start_index = 0

        for digit in range(12):
            max_val = num_str[start_index]
            max_index = start_index

            for i in range(start_index, len(num_str) - (12 - digit - 1)):
                if num_str[i] > max_val:
                    max_val = num_str[i]
                    max_index = i

            num += max_val
            start_index = max_index + 1

        result += int(num)

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
