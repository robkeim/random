import sys
from collections import deque


def part1():
    directions = open("quest14_p1.txt").read().strip()

    max_height = cur_height = 0

    for direction in directions.split(","):
        if direction[0] == "U":
            cur_height += int(direction[1:])
            max_height = max(max_height, cur_height)
        elif direction[0] == "D":
            cur_height -= int(direction[1:])

    print(max_height)


def part2():
    plants = [line.strip() for line in open("quest14_p2.txt").readlines()]
    seen = set()
    dirs = {
        "R": (0, 1, 0),
        "D": (1, 0, 0),
        "L": (0, -1, 0),
        "U": (-1, 0, 0),
        "F": (0, 0, 1),
        "B": (0, 0, -1)
    }

    for plant in plants:
        x = y = z = 0

        for direction in plant.split(","):
            dy, dx, dz = dirs[direction[0]]

            for _ in range(int(direction[1:])):
                x += dx
                y += dy
                z += dz

                seen.add((x, y, z))

    print(len(seen))


def part3():
    plants = [line.strip() for line in open("quest14_p3.txt").readlines()]
    leaves = set()
    trunk = set()
    segments = set()

    dirs = {
        "R": (0, 1, 0),
        "D": (-1, 0, 0),
        "L": (0, -1, 0),
        "U": (1, 0, 0),
        "F": (0, 0, 1),
        "B": (0, 0, -1)
    }

    for plant in plants:
        x = y = z = 0

        for direction in plant.split(","):
            dy, dx, dz = dirs[direction[0]]

            for _ in range(int(direction[1:])):
                x += dx
                y += dy
                z += dz

                if x == 0 and z == 0:
                    trunk.add(y)

                segments.add((x, y, z))

        leaves.add((x, y, z))

    answer = sys.maxsize

    for trunk_y in trunk:
        cost = 0

        for start_x, start_y, start_z in leaves:
            to_process = deque()
            to_process.append((0, start_x, start_y, start_z))
            seen = set()

            while to_process:
                steps, x, y, z = to_process.popleft()

                if x == 0 and y == trunk_y and z == 0:
                    cost += steps
                    break

                if (x, y, z) in seen:
                    continue

                seen.add((x, y, z))

                for dx, dy, dz in dirs.values():
                    next_x = x + dx
                    next_y = y + dy
                    next_z = z + dz

                    if (next_x, next_y, next_z) in segments:
                        to_process.append((steps + 1, next_x, next_y, next_z))

        answer = min(answer, cost)

    print(answer)


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
