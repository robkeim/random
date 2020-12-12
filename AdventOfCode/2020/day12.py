def part1():
    lines = [(line[0], int(line.strip()[1:])) for line in open("day12.txt").readlines()]
    dirs = [(0, 1), (1, 0), (0, -1), (-1, 0)]
    cur_dir = 1
    x = y = 0

    for instruction, value in lines:
        if instruction == "N":
            y += value
        elif instruction == "S":
            y -= value
        elif instruction == "E":
            x += value
        elif instruction == "W":
            x -= value
        elif instruction == "L":
            cur_dir = (cur_dir - value // 90 + 4) % 4
        elif instruction == "R":
            cur_dir = (cur_dir + value // 90) % 4
        elif instruction == "F":
            x += dirs[cur_dir][0] * value
            y += dirs[cur_dir][1] * value
        else:
            raise Exception("Invalid instruction: " + instruction)

    print(abs(x) + abs(y))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
