def main():
    instruction_pointer_register = 5
    instructions = [line.strip().split(" ") for line in open("Day19.txt")]

    registers = [0] * 6
    instruction_pointer = 0

    while instruction_pointer < len(instructions):
        registers[instruction_pointer_register] = instruction_pointer
        code, A, B, C = instructions[registers[instruction_pointer_register]]
        A = int(A)
        B = int(B)
        C = int(C)

        if code == "addr":
            registers[C] = registers[A] + registers[B]
        elif code == "addi":
            registers[C] = registers[A] + B
        elif code == "mulr":
            registers[C] = registers[A] * registers[B]
        elif code == "muli":
            registers[C] = registers[A] * B
        elif code == "banr":
            registers[C] = registers[A] & registers[B]
        elif code == "bani":
            registers[C] = registers[A] & B
        elif code == "borr":
            registers[C] = registers[A] | registers[B]
        elif code == "bori":
            registers[C] = registers[A] | B
        elif code == "setr":
            registers[C] = registers[A]
        elif code == "seti":
            registers[C] = A
        elif code == "gtir":
            registers[C] = 1 if A > registers[B] else 0
        elif code == "gtri":
            registers[C] = 1 if registers[A] > B else 0
        elif code == "gtrr":
            registers[C] = 1 if registers[A] > registers[B] else 0
        elif code == "eqir":
            registers[C] = 1 if A == registers[B] else 0
        elif code == "eqri":
            registers[C] = 1 if registers[A] == B else 0
        elif code == "eqrr":
            registers[C] = 1 if registers[A] == registers[B] else 0
        else:
            raise Exception("Unknown instruction: " + code)

        instruction_pointer = registers[instruction_pointer_register] + 1

    print(registers[0])


if __name__ == "__main__":
    main()
