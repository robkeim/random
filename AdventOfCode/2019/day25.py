from collections import defaultdict
from itertools import chain, combinations


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

    def run_until_next_input(self, inputs=None):
        if inputs is None:
            inputs = []
        else:
            inputs = [ord(char) for char in inputs] + [10]

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
                    return

                self.__set_value(self.__instruction_pointer + 1, self.__relative_base, modes[0], self.__inputs[0])
                self.__inputs = self.__inputs[1:]
                self.__instruction_pointer += 2

            elif opcode == self.__OPCODE_OUTPUT:
                print(chr(self.__get_value(self.__instruction_pointer + 1, self.__relative_base, modes[0])), end="")
                self.__instruction_pointer += 2

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


def main():
    intcode = Intcode("day25.txt")
    intcode.run_until_next_input()
    commands = ['north', 'east', 'take ornament', 'north', 'north', 'take dark matter', 'south', 'east', 'west', 'south', 'west', 'west', 'north', 'take astrolabe', 'east', 'take hologram', 'east', 'take klein bottle', 'west', 'south', 'west', 'east', 'north', 'west', 'west', 'east', 'west', 'east', 'south', 'west', 'take candy cane', 'west', 'south', 'north', 'west', 'take tambourine', 'east', 'east', 'east', 'east', 'south', 'south', 'east', 'take whirled peas', 'west', 'north', 'north', 'west', 'north', 'east', 'south', 'west']

    for command in commands:
        intcode.run_until_next_input(command)

    # This code is used to "play" the game in order to manually find all of the inventory
    # while True:
    #     next_instruction = input("")
    #     if next_instruction == "q":
    #         break
    #
    #     commands.append(next_instruction)
    #     intcode.run_until_next_input(next_instruction)
    #
    # print(commands)

    inventory_items = [
        "ornament",
        "klein bottle",
        "dark matter",
        "candy cane",
        "hologram",
        "astrolabe",
        "whirled peas",
        "tambourine"
    ]

    for combination in chain.from_iterable(combinations(inventory_items, r) for r in range(len(inventory_items) + 1)):
        for inventory_item in inventory_items:
            intcode.run_until_next_input("drop " + inventory_item)

        for item in combination:
            intcode.run_until_next_input("take " + item)

        intcode.run_until_next_input("north")


if __name__ == "__main__":
    main()

# Layout of the world:

# Hull Breach
# 	N (Holodeck)
# 		E (Warp drive maintenance)
# 			N (Kitchen)
# 				N (Sick Bay)
# 					S (Kitchen)
# 				E (Hallway)
# 					W (Kitchen)
# 				S (Warp drive maintenance)
# 			W (Holodeck)
# 		W (Science lab)
# 			N (Crew quarters)
# 				E (Engineering)
# 					E (Corridor)
# 						W (Engineering)
# 					S (Arcade)
# 						N (Engineering)
# 						W (Security checkpoint)
# 							N (XXX)
# 							E (Arcade)
# 					W (Crew quarters)
# 				S (Science lab)
# 				W (Stables)
# 					E (Crew quarters)
# 			E (Holodeck)
# 			W (Gift wrapping center)
# 				E (Science lab)
# 				W (Passages)
# 					E (Gift wrapping center)
# 					S (Hot chocolate fountain)
# 						N (Passages)
# 					W (Storage)
# 						E (Passages)
# 		S (Hull Breach)
# 	S (Observatory)
# 		N (Hull Breach)
# 		E (Navigation)
# 			W (Observatory)
