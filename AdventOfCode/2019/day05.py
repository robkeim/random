OPCODE_ADD = 1
OPCODE_MULTIPLY = 2
OPCODE_INPUT = 3
OPCODE_OUTPUT = 4
OPCODE_JUMP_IF_TRUE = 5
OPCODE_JUMP_IF_FALSE = 6
OPCODE_LESS_THAN = 7
OPCODE_EQUALS = 8
OPCODE_QUIT = 99

MODE_POSITION = 0
MODE_IMMEDIATE = 1


def part1():
    memory = [int(num) for num in open("day05.txt").read().split(",")]
    run_iteration(memory, [1])


def part2():
    memory = [int(num) for num in open("day05.txt").read().split(",")]
    run_iteration(memory, [5])


def run_iteration(memory, inputs):
    instruction_pointer = 0

    while True:
        opcode = memory[instruction_pointer]
        modes = opcode // 100
        opcode %= 100

        modes = [int(val) for val in list(str(modes))][::-1]
        while len(modes) < 3:
            modes.append(0)  # Ensure there are enough modes to avoid out of range issues

        if opcode == OPCODE_QUIT:
            return

        if opcode == OPCODE_ADD:
            val1 = get_value(memory, instruction_pointer + 1, modes[0])
            val2 = get_value(memory, instruction_pointer + 2, modes[1])
            result = val1 + val2

            set_value(memory, instruction_pointer + 3, modes[2], result)
            instruction_pointer += 4

        elif opcode == OPCODE_MULTIPLY:
            val1 = get_value(memory, instruction_pointer + 1, modes[0])
            val2 = get_value(memory, instruction_pointer + 2, modes[1])
            result = val1 * val2

            set_value(memory, instruction_pointer + 3, modes[2], result)
            instruction_pointer += 4

        elif opcode == OPCODE_INPUT:
            set_value(memory, instruction_pointer + 1, modes[0], inputs[0])
            inputs = inputs[1:]
            instruction_pointer += 2

        elif opcode == OPCODE_OUTPUT:
            print(get_value(memory, instruction_pointer + 1, modes[0]))
            instruction_pointer += 2

        elif opcode == OPCODE_JUMP_IF_TRUE:
            if get_value(memory, instruction_pointer + 1, modes[0]) != 0:
                instruction_pointer = get_value(memory, instruction_pointer + 2, modes[1])
            else:
                instruction_pointer += 3

        elif opcode == OPCODE_JUMP_IF_FALSE:
            if get_value(memory, instruction_pointer + 1, modes[0]) == 0:
                instruction_pointer = get_value(memory, instruction_pointer + 2, modes[1])
            else:
                instruction_pointer += 3

        elif opcode == OPCODE_LESS_THAN:
            val1 = get_value(memory, instruction_pointer + 1, modes[0])
            val2 = get_value(memory, instruction_pointer + 2, modes[1])

            if val1 < val2:
                value_to_store = 1
            else:
                value_to_store = 0

            set_value(memory, instruction_pointer + 3, modes[2], value_to_store)
            instruction_pointer += 4

        elif opcode == OPCODE_EQUALS:
            val1 = get_value(memory, instruction_pointer + 1, modes[0])
            val2 = get_value(memory, instruction_pointer + 2, modes[1])

            if val1 == val2:
                value_to_store = 1
            else:
                value_to_store = 0

            set_value(memory, instruction_pointer + 3, modes[2], value_to_store)
            instruction_pointer += 4


def get_value(memory, instruction_pointer, mode):
    if mode == MODE_POSITION:
        return memory[memory[instruction_pointer]]
    elif mode == MODE_IMMEDIATE:
        return memory[instruction_pointer]
    else:
        raise Exception("Unknown mode: " + str(mode))


def set_value(memory, instruction_pointer, mode, value):
    if mode == MODE_POSITION:
        memory[memory[instruction_pointer]] = value
    elif mode == MODE_IMMEDIATE:
        memory[instruction_pointer] = value
    else:
        raise Exception("Unknown mode: " + str(mode))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()