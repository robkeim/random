def part1():
    instructions = [line.split() for line in open("day10.txt").readlines()]

    num_cycles = 0
    x = 1
    result = 0
    key_cycles = {20, 60, 100, 140, 180, 220}

    for instruction in instructions:
        num_cycles += 1

        if num_cycles in key_cycles:
            result += num_cycles * x

        if instruction[0] == "addx":
            num_cycles += 1

            if num_cycles in key_cycles:
                result += num_cycles * x

            x += int(instruction[1])

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
