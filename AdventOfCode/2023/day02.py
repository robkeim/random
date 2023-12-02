import re


def part1():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    result = 0

    for line in lines:
        max_blue = 0
        max_green = 0
        max_red = 0
        id = int(line.split(":")[0].split()[1])

        rounds = line.split(";")

        for round in rounds:
            for match in re.findall("(\d+) (blue|green|red)", round):
                value = int(match[0])
                color = match[1]

                if color == "blue":
                    max_blue = max(max_blue, value)
                    pass
                elif color == "green":
                    max_green = max(max_green, value)
                    pass
                elif color == "red":
                    max_red = max(max_red, value)
                    pass
                else:
                    raise Exception("Unknown color " + color)

        if max_red <= 12 and max_green <= 13 and max_blue <= 14:
            result += id

    print(result)


def part2():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    result = 0

    for line in lines:
        max_blue = 0
        max_green = 0
        max_red = 0
        id = int(line.split(":")[0].split()[1])

        rounds = line.split(";")

        for round in rounds:
            for match in re.findall("(\d+) (blue|green|red)", round):
                value = int(match[0])
                color = match[1]

                if color == "blue":
                    max_blue = max(max_blue, value)
                    pass
                elif color == "green":
                    max_green = max(max_green, value)
                    pass
                elif color == "red":
                    max_red = max(max_red, value)
                    pass
                else:
                    raise Exception("Unknown color " + color)

        result += max_red * max_green * max_blue

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
