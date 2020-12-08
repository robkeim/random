def part1():
    instructions = [line.strip().split(" ") for line in open("day08.txt").readlines()]

    instruction_pointer = 0
    accumulator = 0
    seen = set()

    while True:
        if instruction_pointer in seen:
            print(accumulator)
            return

        seen.add(instruction_pointer)

        operation, value = instructions[instruction_pointer]
        value = int(value)

        if operation == "jmp":
            instruction_pointer += value
        else:
            instruction_pointer += 1

            if operation == "nop":
                pass
            elif operation == "acc":
                accumulator += value
            else:
                raise Exception("Invalid operation: " + operation)


def part2():
    instructions = [line.strip().split(" ") for line in open("day08.txt").readlines()]

    for instruction in instructions:
        instruction[1] = int(instruction[1])

    for i in range(len(instructions)):
        if instructions[i][0] != "nop" and instructions[i][0] != "jmp":
            continue

        instructions_tmp = [instruction[:] for instruction in instructions[:]]
        instructions_tmp[i][0] = "jmp" if instructions_tmp[i][0] == "nop" else "nop"

        result = execute_program(instructions_tmp)

        if result:
            print(result)
            return


def execute_program(instructions):
    instruction_pointer = 0
    accumulator = 0
    seen = set()

    while instruction_pointer < len(instructions):
        if instruction_pointer in seen:
            return None

        seen.add(instruction_pointer)

        operation, value = instructions[instruction_pointer]

        if operation == "jmp":
            instruction_pointer += value
        else:
            instruction_pointer += 1

            if operation == "nop":
                pass
            elif operation == "acc":
                accumulator += value
            else:
                raise Exception("Invalid operation: " + operation)

    if instruction_pointer == len(instructions):
        return accumulator

    return None


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
