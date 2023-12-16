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
    instructions = [line.split() for line in open("day10.txt").readlines()]

    num_cycles = 0
    sprite_position = 1
    result = ""

    for instruction in instructions:
        if abs((num_cycles % 40) - sprite_position) <= 1:
            result += "#"
        else:
            result += "."

        num_cycles += 1

        if instruction[0] == "addx":
            if abs((num_cycles % 40) - sprite_position) <= 1:
                result += "#"
            else:
                result += "."

            num_cycles += 1

            sprite_position += int(instruction[1])

    for i in range(0, len(result), 40):
        print(result[i:i+40])


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
