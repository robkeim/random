import math


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
            A = math.trunc(A / (2 ** combo(operand, A, B, C)))
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
            B = math.trunc(A / (2 ** combo(operand, A, B, C)))
        elif opcode == cdv:
            C = math.trunc(A / (2 ** combo(operand, A, B, C)))
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
    pass


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
    part2()


if __name__ == "__main__":
    main()
