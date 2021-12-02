def part1():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    horizontal = depth = 0

    for line in lines:
        direction, x = line.split(" ")
        x = int(x)

        if direction == "forward":
            horizontal += x
        elif direction == "up":
            depth -= x
        elif direction == "down":
            depth += x
        else:
            raise Exception("Invalid direction: " + direction)

    print(horizontal * depth)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
