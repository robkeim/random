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
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
