def part1():
    grid, instructions = open("day22.txt").read().split("\n\n")
    instructions = parse_instructions(instructions)

    min_r = dict()
    max_r = dict()
    min_c = dict()
    max_c = dict()
    walls = set()
    start_c = None

    for r, row in enumerate(grid.split("\n")):
        for c, value in enumerate(row):
            if r == 0 and not start_c and value == ".":
                start_c = c

            if value in ".#":
                if r not in min_c:
                    min_c[r] = c

                if c not in min_r:
                    min_r[c] = r

                max_c[r] = c
                max_r[c] = r

                if value == "#":
                    walls.add((r, c))

    # Right, down, left, up
    deltas = [(0, 1), (1, 0), (0, -1), (-1, 0)]
    facing = 0
    r = 0
    c = start_c

    for instruction in instructions:
        if instruction == "L":
            facing = (facing + 3) % 4
        elif instruction == "R":
            facing = (facing + 1) % 4
        else:
            for _ in range(instruction):
                next_r = r + deltas[facing][0]
                next_c = c + deltas[facing][1]

                if facing == 0 and next_c > max_c[r]:
                    next_c = min_c[r]
                elif facing == 1 and next_r > max_r[c]:
                    next_r = min_r[c]
                elif facing == 2 and next_c < min_c[r]:
                    next_c = max_c[r]
                elif facing == 3 and next_r < min_r[c]:
                    next_r = max_r[c]

                if (next_r, next_c) in walls:
                    break

                r = next_r
                c = next_c

    print(1000 * (r + 1) + 4 * (c + 1) + facing)

def parse_instructions(string):
    instructions = []
    prev = ""

    for char in string:
        if char in "LR":
            if len(prev) > 0:
                instructions.append(int(prev))
                prev = ""

            instructions.append(char)
        else:
            prev += char

    if len(prev) > 0:
        instructions.append(int(prev))

    return instructions

def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
