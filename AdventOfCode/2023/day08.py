import re

from math import lcm


def part1():
    lines = [line.strip() for line in open("day08.txt").readlines()]
    instructions = lines[0]

    destinations = build_destinations(lines[2:])

    cur_position = "AAA"
    num_steps = 0
    instruction_index = 0

    while cur_position != "ZZZ":
        if instructions[instruction_index] == "L":
            cur_position = destinations[cur_position][0]
        else:
            cur_position = destinations[cur_position][1]

        num_steps += 1
        instruction_index = (instruction_index + 1) % len(instructions)

    print(num_steps)


def build_destinations(lines):
    destinations = dict()

    for line in lines:
        match = re.search(r"([A-Z]{3}) = \(([A-Z]{3}), ([A-Z]{3})\)", line)
        source, left, right = match.groups()

        destinations[source] = (left, right)

    return destinations


def part2():
    lines = [line.strip() for line in open("day08.txt").readlines()]
    instructions = lines[0]

    destinations = build_destinations(lines[2:])

    start_locations = [location for location in destinations.keys() if location.endswith("A")]

    cycle_lengths = []

    for start_location in start_locations:
        cycle_lengths.append(run_one_simulation(start_location, instructions, destinations))

    print(lcm(*cycle_lengths))


def run_one_simulation(start_location, instructions, destinations):
    cur_position = start_location
    num_steps = 0
    instruction_index = 0

    while not cur_position.endswith("Z"):
        if instructions[instruction_index] == "L":
            cur_position = destinations[cur_position][0]
        else:
            cur_position = destinations[cur_position][1]

        num_steps += 1
        instruction_index = (instruction_index + 1) % len(instructions)

    return num_steps


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
