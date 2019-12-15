from collections import defaultdict
from collections import deque


class Intcode:
    __OPCODE_ADD = 1
    __OPCODE_MULTIPLY = 2
    __OPCODE_INPUT = 3
    __OPCODE_OUTPUT = 4
    __OPCODE_JUMP_IF_TRUE = 5
    __OPCODE_JUMP_IF_FALSE = 6
    __OPCODE_LESS_THAN = 7
    __OPCODE_EQUALS = 8
    __OPCODE_ADJUST_RELATIVE_BASE = 9
    __OPCODE_QUIT = 99

    __MODE_POSITION = 0
    __MODE_IMMEDIATE = 1
    __MODE_RELATIVE_BASE = 2

    def __init__(self, filename):
        input_memory = [int(num) for num in open(filename).read().split(",")]

        self.__memory = defaultdict(int)

        for i in range(len(input_memory)):
            self.__memory[i] = input_memory[i]

        self.__instruction_pointer = 0
        self.__relative_base = 0
        self.__inputs = []

    def run(self, inputs):
        result = self.run_to_next_output(inputs)

        while result is not None:
            print(result)
            result = self.run_to_next_output([])

    def run_to_next_output(self, inputs):
        self.__inputs += inputs

        while True:
            opcode = self.__memory[self.__instruction_pointer]
            modes = opcode // 100
            opcode %= 100

            modes = [int(val) for val in list(str(modes))][::-1]
            while len(modes) < 3:
                modes.append(0)  # Ensure there are enough modes to avoid out of range issues

            if opcode == self.__OPCODE_QUIT:
                return None

            if opcode == self.__OPCODE_ADD:
                val1 = self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0])
                val2 = self.__get_value(self.__instruction_pointer + 2, self.__relative_base, modes[1])
                result = val1 + val2

                self.__set_value(self.__instruction_pointer + 3, self.__relative_base, modes[2], result)
                self.__instruction_pointer += 4

            elif opcode == self.__OPCODE_MULTIPLY:
                val1 = self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0])
                val2 = self.__get_value(self.__instruction_pointer + 2, self.__relative_base, modes[1])
                result = val1 * val2

                self.__set_value(self.__instruction_pointer + 3, self.__relative_base, modes[2], result)
                self.__instruction_pointer += 4

            elif opcode == self.__OPCODE_INPUT:
                if len(self.__inputs) == 0:
                    raise Exception("Expected input but none available")

                self.__set_value(self.__instruction_pointer + 1, self.__relative_base, modes[0], self.__inputs[0])
                self.__inputs = self.__inputs[1:]
                self.__instruction_pointer += 2

            elif opcode == self.__OPCODE_OUTPUT:
                result = self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0])
                self.__instruction_pointer += 2
                return result

            elif opcode == self.__OPCODE_JUMP_IF_TRUE:
                if self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0]) != 0:
                    self.__instruction_pointer = self.__get_value(self.__instruction_pointer + 2, self.__relative_base, modes[1])
                else:
                    self.__instruction_pointer += 3

            elif opcode == self.__OPCODE_JUMP_IF_FALSE:
                if self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0]) == 0:
                    self.__instruction_pointer = self.__get_value(self.__instruction_pointer + 2, self.__relative_base, modes[1])
                else:
                    self.__instruction_pointer += 3

            elif opcode == self.__OPCODE_LESS_THAN:
                val1 = self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0])
                val2 = self.__get_value(self.__instruction_pointer + 2, self.__relative_base, modes[1])

                if val1 < val2:
                    value_to_store = 1
                else:
                    value_to_store = 0

                self.__set_value(self.__instruction_pointer + 3, self.__relative_base, modes[2], value_to_store)
                self.__instruction_pointer += 4

            elif opcode == self.__OPCODE_EQUALS:
                val1 = self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0])
                val2 = self.__get_value(self.__instruction_pointer + 2, self.__relative_base, modes[1])

                if val1 == val2:
                    value_to_store = 1
                else:
                    value_to_store = 0

                self.__set_value(self.__instruction_pointer + 3, self.__relative_base, modes[2], value_to_store)
                self.__instruction_pointer += 4

            elif opcode == self.__OPCODE_ADJUST_RELATIVE_BASE:
                self.__relative_base += self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0])
                self.__instruction_pointer += 2

            else:
                raise Exception("Unknown opcode: " + str(opcode))

    def __get_value(self, instruction_pointer, relative_base, mode):
        if mode == self.__MODE_POSITION:
            return self.__memory[self.__memory[instruction_pointer]]
        elif mode == self.__MODE_IMMEDIATE:
            return self.__memory[instruction_pointer]
        elif mode == self.__MODE_RELATIVE_BASE:
            return self.__memory[relative_base + self.__memory[instruction_pointer]]
        else:
            raise Exception("Unknown mode: " + str(mode))

    def __set_value(self, instruction_pointer, relative_base, mode, value):
        if mode == self.__MODE_POSITION:
            self.__memory[self.__memory[instruction_pointer]] = value
        elif mode == self.__MODE_IMMEDIATE:
            self.__memory[instruction_pointer] = value
        elif mode == self.__MODE_RELATIVE_BASE:
            self.__memory[relative_base + self.__memory[instruction_pointer]] = value
        else:
            raise Exception("Unknown mode: " + str(mode))


def part1():
    N = 1
    S = 2
    W = 3
    E = 4
    WALL = 0
    MOVED = 1
    OXYGEN = 2

    intcode = Intcode("day15.txt")

    direction_deltas = { N: (0, 1), S: (0, -1), W: (-1, 0), E: (1, 0) }
    opposite_directions = { N: S, S: N, W: E, E: W }

    explored = set()
    explored.add((0, 0))
    paths = deque()
    paths.append(((0, 0), []))

    while True:
        pos, path = paths.popleft()
        for direction in path:
            intcode.run_to_next_output([direction])

        for direction in [N, S, W, E]:
            delta = direction_deltas[direction]
            next_pos = (pos[0] + delta[0], pos[1] + delta[1])

            if next_pos in explored:
                continue

            explored.add(next_pos)

            output = intcode.run_to_next_output([direction])
            if output == OXYGEN:
                print(len(path) + 1)
                return
            elif output == MOVED:
                paths.append((next_pos, path[:] + [direction]))
                intcode.run_to_next_output([opposite_directions[direction]])

        for direction in path[::-1]:
            intcode.run_to_next_output([opposite_directions[direction]])


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
