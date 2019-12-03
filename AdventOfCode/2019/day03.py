import sys


def part1():
    first, second = [line.strip() for line in open("day03.txt").readlines()]
    deltas = {"U": (0, 1), "D": (0, -1), "L": (-1, 0), "R": (1, 0)}
    visited = set()
    x, y = 0, 0

    for instruction in first.split(","):
        direction, distance = instruction[0], int(instruction[1:])
        delta_x, delta_y = deltas[direction]

        for _ in range(0, distance):
            x += delta_x
            y += delta_y
            visited.add((x, y))

    min_distance = sys.maxsize

    x, y = 0, 0

    for instruction in second.split(","):
        direction, distance = instruction[0], int(instruction[1:])
        delta_x, delta_y = deltas[direction]

        for _ in range(0, distance):
            x += delta_x
            y += delta_y
            if (x, y) in visited:
                manhatten_distance = abs(x) + abs(y)
                if manhatten_distance < min_distance:
                    min_distance = manhatten_distance

    print(min_distance)


def part2():
    first, second = [line.strip() for line in open("day03.txt").readlines()]
    deltas = {"U": (0, 1), "D": (0, -1), "L": (-1, 0), "R": (1, 0)}
    visited = {}
    x, y, steps = 0, 0, 1

    for instruction in first.split(","):
        direction, distance = instruction[0], int(instruction[1:])
        delta_x, delta_y = deltas[direction]

        for _ in range(0, distance):
            x += delta_x
            y += delta_y
            visited[(x, y)] = steps
            steps += 1

    min_time = sys.maxsize

    x, y, steps = 0, 0, 1

    for instruction in second.split(","):
        direction, distance = instruction[0], int(instruction[1:])
        delta_x, delta_y = deltas[direction]

        for _ in range(0, distance):
            x += delta_x
            y += delta_y
            if (x, y) in visited:
                time = visited[(x, y)] + steps
                if time < min_time:
                    min_time = time
            steps += 1

    print(min_time)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
