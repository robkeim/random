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
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
