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

        op_index += 4

    print(data[0])


def part2():
    orig_data = [int(num) for num in open("day2.txt").read().split(",")]

    found = False

    # Brute force through a range of possibilities
    for noun in range(0, 1000):
        if found:
            break

        for verb in range(0, 1000):
            data = orig_data[:]
            instruction_pointer = 0

            data[1] = noun
            data[2] = verb

            try:
                while True:
                    opcode = data[instruction_pointer]

                    if opcode != 1 and opcode != 2:
                        break

                    val1 = data[data[instruction_pointer + 1]]
                    val2 = data[data[instruction_pointer + 2]]

                    if opcode == 1:
                        result = val1 + val2
                    else:
                        result = val1 * val2

                    data[data[instruction_pointer + 3]] = result

                    instruction_pointer += 4

                if data[0] == 19690720:
                    print(str(100 * noun + verb))
                    found = True
            except Exception:
                pass # Some iterations don't terminate and run out of instructions

        noun += 1


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
