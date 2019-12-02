OPCODE_ADD = 1
OPCODE_MULTIPLY = 2
OPCODE_QUIT = 99


def part1():
    memory = [int(num) for num in open("day2.txt").read().split(",")]

    memory[1] = 12
    memory[2] = 2
    run_iteration(memory)

    print(memory[0])


def part2():
    orig_memory = [int(num) for num in open("day2.txt").read().split(",")]

    for noun in range(0, 100):
        for verb in range(0, 100):
            memory = orig_memory[:]

            memory[1] = noun
            memory[2] = verb

            try:
                run_iteration(memory)

                if memory[0] == 19690720:
                    print(str(100 * noun + verb))
                    return
            except Exception:
                pass # Some iterations don't terminate and run out of instructions

        noun += 1


def run_iteration(memory):
    instruction_pointer = 0

    while True:
        opcode = memory[instruction_pointer]

        if opcode == OPCODE_QUIT:
            return

        val1 = memory[memory[instruction_pointer + 1]]
        val2 = memory[memory[instruction_pointer + 2]]

        if opcode == OPCODE_ADD:
            result = val1 + val2
        elif opcode == OPCODE_MULTIPLY:
            result = val1 * val2
        else:
            raise Exception("Unknown opcode: " + str(opcode))

        memory[memory[instruction_pointer + 3]] = result

        instruction_pointer += 4


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
