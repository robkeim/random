def part1():
    lines = [line.strip() for line in open("day09.txt").readlines()]
    lines = [[int(value) for value in line.split()] for line in lines]
    print(sum([get_next_value(line) for line in lines]))


def get_next_value(line):
    lines = [line]

    # Generate all rows
    while len(set(lines[-1])) != 1:
        cur_line = lines[-1]
        next_line = []

        for i in range(len(cur_line) - 1):
            next_line.append(cur_line[i + 1] - cur_line[i])

        lines.append(next_line)

    # Calculate next value
    next_value = lines[-1][-1]

    for i in range(len(lines) - 2, -1, -1):
        next_value += lines[i][-1]

    return next_value


def part2():
    lines = [line.strip() for line in open("day09.txt").readlines()]
    lines = [[int(value) for value in line.split()] for line in lines]
    print(sum([get_prev_value(line) for line in lines]))


def get_prev_value(line):
    lines = [line]

    # Generate all rows
    while len(set(lines[-1])) != 1:
        cur_line = lines[-1]
        next_line = []

        for i in range(len(cur_line) - 1):
            next_line.append(cur_line[i + 1] - cur_line[i])

        lines.append(next_line)

    # Calculate next value
    next_value = lines[-1][0]

    for i in range(len(lines) - 2, -1, -1):
        next_value = lines[i][0] - next_value

    return next_value


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
