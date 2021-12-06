def part1():
    print(run_simulation(80))


def part2():
    print(run_simulation(256))


def run_simulation(num_iterations):
    input = [int(line.strip()) for line in open("day06.txt").read().split(",")]
    cur = [0] * 9

    for laternfish in input:
        cur[laternfish] += 1

    for _ in range(num_iterations):
        next = [0] * 9
        next[8] = cur[0]
        next[6] = cur[0]

        for i in range(0, 8):
            next[i] += cur[i + 1]

        cur = next

    return sum(cur)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
