def part1():
    data = [int(num) for num in open("day2.txt").read().split(",")]
    op_index = 0

    data[1] = 12
    data[2] = 2

    while True:
        op = data[op_index]

        if op != 1 and op != 2:
            break

        val1 = data[data[op_index + 1]]
        val2 = data[data[op_index + 2]]

        if op == 1:
            result = val1 + val2
        else:
            result = val1 * val2

        data[data[op_index + 3]] = result

        op_index = op_index + 4 % len(data)

    print(data[0])


def main():
    part1()


if __name__ == "__main__":
    main()
