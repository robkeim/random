import re


def part1():
    stacks_input, moves = open("day05.txt").read().split("\n\n")
    stacks = parse_stacks(stacks_input)

    for num_moves, source, destination in re.findall(r"move (\d+) from (\d+) to (\d+)", moves):
        for _ in range(int(num_moves)):
            stacks[int(destination) - 1].append(stacks[int(source) - 1].pop())

    result = ""

    for stack in stacks:
        result += stack.pop()

    print(result)


def parse_stacks(stacks_input):
    stacks_input = [line for line in stacks_input.split("\n")]

    stacks = []

    for i in range(len(stacks_input[-1])):
        if stacks_input[-1][i].isdigit():
            stack = []

            for j in range(len(stacks_input) - 2, -1, -1):
                if i >= len(stacks_input[j]):
                    break

                character = stacks_input[j][i]

                if "A" <= character <= "Z":
                    stack.append(character)

            stacks.append(stack)

    return stacks


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
