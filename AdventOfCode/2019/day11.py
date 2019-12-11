from collections import defaultdict


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
    BLACK_PANEL = 0
    WHITE_PANEL = 1
    TURN_LEFT = 0
    TURN_RIGHT = 1

    intcode = Intcode("day11.txt")

    white_panels = set()
    painted_panels = set()
    cur_pos = (0, 0)
    cur_dir = "U"

    while True:
        new_color = intcode.run_to_next_output([WHITE_PANEL if cur_pos in white_panels else BLACK_PANEL])

        if new_color is None:
            break

        turn_direction = intcode.run_to_next_output([])

        if turn_direction is None:
            break

        if new_color == BLACK_PANEL:
            white_panels.remove(cur_pos)
        elif new_color == WHITE_PANEL:
            white_panels.add(cur_pos)
        else:
            raise Exception("Unknown panel color: " + str(new_color))

        painted_panels.add(cur_pos)

        if turn_direction == TURN_LEFT:
            if cur_dir == "U":
                cur_dir = "L"
            elif cur_dir == "L":
                cur_dir = "D"
            elif cur_dir == "D":
                cur_dir = "R"
            elif cur_dir == "R":
                cur_dir = "U"
            else:
                raise Exception("Unknown current direction: " + cur_dir)
        elif turn_direction == TURN_RIGHT:
            if cur_dir == "U":
                cur_dir = "R"
            elif cur_dir == "R":
                cur_dir = "D"
            elif cur_dir == "D":
                cur_dir = "L"
            elif cur_dir == "L":
                cur_dir = "U"
            else:
                raise Exception("Unknown current direction: " + cur_dir)
        else:
            raise Exception("Unknown turn direction: " + str(turn_direction))

        if cur_dir == "U":
            cur_pos = (cur_pos[0], cur_pos[1] + 1)
        elif cur_dir == "L":
            cur_pos = (cur_pos[0] - 1, cur_pos[1])
        elif cur_dir == "D":
            cur_pos = (cur_pos[0], cur_pos[1] - 1)
        elif cur_dir == "R":
            cur_pos = (cur_pos[0] + 1, cur_pos[1])
        else:
            raise Exception("Unknown current direction: " + cur_dir)

    print(len(painted_panels))


def part2():
    BLACK_PANEL = 0
    WHITE_PANEL = 1
    TURN_LEFT = 0
    TURN_RIGHT = 1

    intcode = Intcode("day11.txt")

    white_panels = set()
    white_panels.add((0, 0))
    cur_pos = (0, 0)
    cur_dir = "U"

    while True:
        new_color = intcode.run_to_next_output([WHITE_PANEL if cur_pos in white_panels else BLACK_PANEL])

        if new_color is None:
            break

        turn_direction = intcode.run_to_next_output([])

        if turn_direction is None:
            break

        if new_color == BLACK_PANEL:
            if cur_pos in white_panels:
                white_panels.remove(cur_pos)
        elif new_color == WHITE_PANEL:
            white_panels.add(cur_pos)
        else:
            raise Exception("Unknown panel color: " + str(new_color))

        if turn_direction == TURN_LEFT:
            if cur_dir == "U":
                cur_dir = "L"
            elif cur_dir == "L":
                cur_dir = "D"
            elif cur_dir == "D":
                cur_dir = "R"
            elif cur_dir == "R":
                cur_dir = "U"
            else:
                raise Exception("Unknown current direction: " + cur_dir)
        elif turn_direction == TURN_RIGHT:
            if cur_dir == "U":
                cur_dir = "R"
            elif cur_dir == "R":
                cur_dir = "D"
            elif cur_dir == "D":
                cur_dir = "L"
            elif cur_dir == "L":
                cur_dir = "U"
            else:
                raise Exception("Unknown current direction: " + cur_dir)
        else:
            raise Exception("Unknown turn direction: " + str(turn_direction))

        if cur_dir == "U":
            cur_pos = (cur_pos[0], cur_pos[1] + 1)
        elif cur_dir == "L":
            cur_pos = (cur_pos[0] - 1, cur_pos[1])
        elif cur_dir == "D":
            cur_pos = (cur_pos[0], cur_pos[1] - 1)
        elif cur_dir == "R":
            cur_pos = (cur_pos[0] + 1, cur_pos[1])
        else:
            raise Exception("Unknown current direction: " + cur_dir)

    min_x = min([pos[0] for pos in white_panels])
    max_x = max([pos[0] for pos in white_panels])
    min_y = min([pos[1] for pos in white_panels])
    max_y = max([pos[1] for pos in white_panels])

    for y in range(max_y, min_y - 1, -1):
        row = ""
        for x in range(min_x, max_x + 1):
            if (x, y) in white_panels:
                row += "X"
            else:
                row += " "

        print(row)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
