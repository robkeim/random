def part1():
    lines = [line.strip() for line in open("day09.txt").readlines()]

    width = len(lines[0])
    height = len(lines)

    risk = 0

    for y in range(height):
        for x in range(width):
            if ((y == 0 or lines[y - 1][x] > lines[y][x])
                    and (y == height - 1 or lines[y][x] < lines[y + 1][x])
                    and (x == 0 or lines[y][x - 1] > lines[y][x])
                    and (x == width - 1 or lines[y][x] < lines[y][x + 1])):
                risk += int(lines[y][x]) + 1

    print(risk)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
