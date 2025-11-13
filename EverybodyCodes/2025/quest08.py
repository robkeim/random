def part1():
    nails = [int(nail) for nail in open("quest08_p1.txt").read().strip().split(",")]
    num_nails = 32

    result = 0
    for i in range(len(nails) - 1):
        if abs(nails[i] - nails[i + 1]) == num_nails // 2:
            result += 1

    print(result)


def part2():
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
