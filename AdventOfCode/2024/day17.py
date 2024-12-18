adv = 0
bxl = 1
bst = 2
jnz = 3
bxc = 4
out = 5
bdv = 6
cdv = 7

def part1():
    lines = [line.strip() for line in open("day17.txt").readlines()]
    A = int(lines[0].split(":")[1])
    B = int(lines[1].split(":")[1])
    C = int(lines[2].split(":")[1])
    program = [int(value) for value in lines[4].split(":")[1].split(",")]
    _, _, _, output = run_program(A, B, C, program)
    print(output)


def run_program(A, B, C, program):
    instruction_pointer = 0
    output = []

    while instruction_pointer < len(program):
        increment_instruction_pointer = True

        opcode, operand = program[instruction_pointer:instruction_pointer + 2]

        if opcode == adv:
            A = A // (2 ** combo(operand, A, B, C))
        elif opcode == bxl:
            B ^= operand
        elif opcode == bst:
            B = combo(operand, A, B, C) % 8
        elif opcode == jnz:
            if A != 0:
                increment_instruction_pointer = False
                instruction_pointer = operand
        elif opcode == bxc:
            B ^= C
        elif opcode == out:
            output.append(combo(operand, A, B, C) % 8)
        elif opcode == bdv:
            B = A // (2 ** combo(operand, A, B, C))
        elif opcode == cdv:
            C = A // (2 ** combo(operand, A, B, C))
        else:
            assert False, f"Invalid opcode: {opcode}"

        if increment_instruction_pointer:
            instruction_pointer += 2

    return A, B, C, ",".join([str(value) for value in output])


def combo(operand, A, B, C):
    if 0 <= operand <= 3:
        return operand
    elif operand == 4:
        return A
    elif operand == 5:
        return B
    elif operand == 6:
        return C
    else:
        assert False, f"Invalid operand: {operand}"

def part2():
    lines = [line.strip() for line in open("day17.txt").readlines()]
    B = int(lines[1].split(":")[1])
    C = int(lines[2].split(":")[1])
    program_str = lines[4].split(":")[1]
    program = [int(value) for value in program_str.split(",")]

    A = 1

    while True:
        if A % 10_000 == 0:
            print("A:", A)

        _, _, _, output = run_program(A, B, C, program)
        if output == program_str:
            print(A)
            break

        A += 1

    # 2,4 -> B = A % 8
    # 1,7 -> B ^= 7
    # 7,5 -> C = A // (2 ** B)
    # 0,3 -> A = A // (2 ** 3) => A // 8
    # 4,4 -> B ^= C
    # 1,7 -> B ^= 7
    # 5,5 -> Output: B % 8
    # 3,0 -> If A != 0, goto 0


# Running the simulation would be too long so reverse engineer the program and translate it into a high level language
# Since everything is mod 8, look at the numbers that start outputting the correct values and look for a pattern
# use that pattern as a suffix and continue adding new prefixes and try to extend the pattern
# After several iterations you eventually come up with the correct value
def run_part2():
    prefix = 0
    expected_output = [2, 4, 1, 7, 7, 5, 0, 3, 4, 4, 1, 7, 5, 5, 3, 0]

    while True:
        for base in ["522621633", "522621635"]:
            startA = int(f"{oct(prefix)}{base}", 8)

            A = startA
            B = 0
            C = 0
            output = []

            while A != 0:
                B = A % 8
                B ^= 7
                C = A // (2 ** B)
                A = A // 8
                B ^= C
                B ^= 7
                output.append(B % 8)

                if len(output) > len(expected_output) or output[-1] != expected_output[len(output) - 1]:
                    break

                if output == expected_output:
                    print(startA)
                    return

        prefix += 1


def test():
    for test_case in test_cases:
        inA, inB, inC, inProgram = test_case[0]
        expectedA, expectedB, expectedC, expectedOutput = test_case[1]
        inProgram = [int(value) for value in inProgram.split(",")]
        actualA, actualB, actualC, actualOutput = run_program(inA, inB, inC, inProgram)

        errors = []

        if expectedA and expectedA != actualA:
            errors.append(f"Invalid value for A. Expected={expectedA}, Actual={actualA}")

        if expectedB and expectedB != actualB:
            errors.append(f"Invalid value for B. Expected={expectedB}, Actual={actualB}")

        if expectedC and expectedC != actualC:
            errors.append(f"Invalid value for C. Expected={expectedC}, Actual={actualC}")

        if expectedOutput and expectedOutput != actualOutput:
            errors.append(f"Invalid value for output. Expected={expectedOutput}, Actual={actualOutput}")

        if len(errors) > 0:
            assert False, "\n".join(errors)


test_cases = [
    # [(A, B, C, program), (A, B, C, output)]
    [(None, None, 9, "2,6"), (None, 1, 9, None)],
    [(10, None, None, "5,0,5,1,5,4"), (None, None, None, "0,1,2")],
    [(2024, None, None, "0,1,5,4,3,0"), (0, None, None, "4,2,5,6,7,7,7,7,3,1,0")],
    [(None, 29, None, "1,7"), (None, 26, None, None)],
    [(None, 2024, 43690, "4,0"), (None, 44354, None, None)],
    [(729, 0, 0, "0,1,5,4,3,0"), (None, None, None, "4,6,3,5,6,3,5,2,1,0")]
]


def main():
    # test()
    part1()
    # part2()
    run_part2()


if __name__ == "__main__":
    main()
