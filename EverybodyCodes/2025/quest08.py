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
    nails = [int(nail) for nail in open("quest08_p3.txt").read().strip().split(",")]

    threads = []

    for i in range(len(nails) - 1):
        min_ = min(nails[i], nails[i + 1])
        max_ = max(nails[i], nails[i + 1])
        threads.append((min_, max_))

    max_cuts = 0

    for small in range(1, 257):
        for big in range(small + 1, 257):
            num_cuts = 0

            for thread_small, thread_big in threads:
                if small == thread_small and big == thread_big:
                    num_cuts += 1
                    continue

                sorted_values = "".join([values[1] for values in sorted([(small, "A"), (big, "A"), (thread_small, "B"), (thread_big, "B")])])

                if (sorted_values == "ABAB" or sorted_values == "BABA") and len({small, big, thread_small, thread_big}) == 4:
                    num_cuts += 1

            max_cuts = max(max_cuts, num_cuts)

    print(max_cuts)


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
