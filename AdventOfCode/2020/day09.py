def part1():
    values = [int(line.strip()) for line in open("day09.txt").readlines()]
    preamble = 25

    for i in range(preamble, len(values)):
        prev = set(values[i - preamble:i])

        valid = False

        for value in values[i - preamble:i]:
            if values[i] - value in prev and values[i] - value != value:
                valid = True
                break

        if not valid:
            print(values[i])
            return

    raise Exception("All numbers are valid")


def part2():
    values = [int(line.strip()) for line in open("day09.txt").readlines()]
    target = 15353384  # Result from part 1

    for i in range(len(values)):
        total = 0

        for j in range(i, len(values)):
            total += values[j]

            if total == target:
                print(min(values[i:j]) + max(values[i:j]))
                return
            elif total > target:
                break

    raise Exception("No solution found")


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
