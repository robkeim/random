def part1():
    cups = [int(cup) for cup in open("day23.txt").read().strip()]

    for _ in range(100):
        cur_cup_val = cups[0]
        pickup = cups[1:4]
        remaining = set(cups[4:])

        next_value = max(remaining)

        for i in range(cur_cup_val - 1, 0, -1):
            if i in remaining:
                next_value = i
                break

        index = cups.index(next_value)
        next_cups = [cur_cup_val] + cups[4:index + 1] + pickup + cups[index + 1:]

        cups = next_cups[1:] + [cups[0]]

    index = cups.index(1)
    result = [str(value) for value in cups[index + 1:] + cups[:index]]

    print("".join(result))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
