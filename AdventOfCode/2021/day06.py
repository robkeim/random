def part1():
    input = [int(line.strip()) for line in open("day06.txt").read().split(",")]
    cur = [0] * 9

    for laternfish in input:
        cur[laternfish] += 1

    for _ in range(80):
        next = [0] * 9
        next[8] = cur[0]
        next[6] = cur[0]

        for i in range(0, 8):
            next[i] += cur[i + 1]

        cur = next

    print(sum(cur))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
