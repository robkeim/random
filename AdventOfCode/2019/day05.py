OPCODE_ADD = 1
OPCODE_MULTIPLY = 2
OPCODE_INPUT = 3
OPCODE_OUTPUT = 4
OPCODE_QUIT = 99

MODE_POSITION = 0
MODE_VALUE = 1


def part1():
    memory = [int(num) for num in open("day05.txt").read().split(",")]
    run_iteration(memory, [1])


def part2():
    pass


def run_iteration(memory, inputs):
    instruction_pointer = 0

    while True:
        opcode = memory[instruction_pointer]
        modes = opcode // 100
        opcode %= 100

        modes = [int(val) for val in list(str(modes))][::-1]
        modes.append(0)  # Ensure there are enough modes to avoid out of range issues
        modes.append(0)

        if opcode == OPCODE_QUIT:
            return

        if opcode == OPCODE_ADD:
            if modes[0] == MODE_POSITION:
                val1 = memory[memory[instruction_pointer + 1]]
            elif modes[0] == MODE_VALUE:
                val1 = memory[instruction_pointer + 1]
            else:
                raise Exception("Unknown mode: " + str(modes[0]))

            if modes[1] == MODE_POSITION:
                val2 = memory[memory[instruction_pointer + 2]]
            elif modes[1] == MODE_VALUE:
                val2 = memory[instruction_pointer + 2]
            else:
                raise Exception("Unknown mode: " + str(modes[1]))

            result = val1 + val2
            memory[memory[instruction_pointer + 3]] = result
            instruction_pointer += 4

        elif opcode == OPCODE_MULTIPLY:
            if modes[0] == MODE_POSITION:
                val1 = memory[memory[instruction_pointer + 1]]
            elif modes[0] == MODE_VALUE:
                val1 = memory[instruction_pointer + 1]
            else:
                raise Exception("Unknown mode: " + str(modes[0]))

            if modes[1] == MODE_POSITION:
                val2 = memory[memory[instruction_pointer + 2]]
            elif modes[1] == MODE_VALUE:
                val2 = memory[instruction_pointer + 2]
            else:
                raise Exception("Unknown mode: " + str(modes[1]))

            result = val1 * val2
            memory[memory[instruction_pointer + 3]] = result
            instruction_pointer += 4

        elif opcode == OPCODE_INPUT:
            if modes[0] == MODE_POSITION:
                memory[memory[instruction_pointer + 1]] = inputs[0]
            elif modes[0] == MODE_VALUE:
                memory[instruction_pointer + 1] = inputs[0]
            else:
                raise Exception("Unknown mode: " + str(modes[0]))

            memory[memory[instruction_pointer + 1]] = inputs[0]
            inputs = inputs[1:]
            instruction_pointer += 2

        elif opcode == OPCODE_OUTPUT:
            if modes[0] == MODE_POSITION:
                print(memory[memory[instruction_pointer + 1]])
            elif modes[0] == MODE_VALUE:
                print(memory[instruction_pointer + 1])
            else:
                raise Exception("Unknown mode: " + str(modes[0]))

            instruction_pointer += 2

        else:
            raise Exception("Unknown opcode: " + str(opcode))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
