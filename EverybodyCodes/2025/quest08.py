def part1():
    nails = [int(nail) for nail in open("quest08_p1.txt").read().strip().split(",")]
    num_nails = 32

    result = 0
    for i in range(len(nails) - 1):
        if abs(nails[i] - nails[i + 1]) == num_nails // 2:
            result += 1

    print(result)


def part2():
    nails = [int(nail) for nail in open("quest08_p2.txt").read().strip().split(",")]

    segments = []
    result = 0

    for i in range(len(nails) - 1):
        small = nails[i]
        big = nails[i + 1]

        for small_test, big_test in segments:
            sorted_values = "".join([values[1] for values in sorted([(small, "A"), (big, "A"), (small_test, "B"), (big_test, "B")])])

            if (sorted_values == "ABAB" or sorted_values == "BABA") and len({small, big, small_test, big_test}) == 4:
                result += 1

        segments.append((small, big))

    print(result)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
