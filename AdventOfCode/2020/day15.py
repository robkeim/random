from collections import defaultdict


def part1():
    print(run(2020))


def part2():
    print(run(30000000))


def run(num_turns):
    starting_values = [int(value) for value in open("day15.txt").read().split(",")]
    prev = None
    turn = 0
    value_to_turns = defaultdict(list)

    while turn < num_turns:
        if len(starting_values) > 0:
            next_value = starting_values.pop(0)
        else:
            if len(value_to_turns[prev]) < 2:
                next_value = 0
            else:
                next_value = value_to_turns[prev][-1] - value_to_turns[prev][-2]

        value_to_turns[next_value].append(turn)
        prev = next_value
        turn += 1

    print(prev)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
