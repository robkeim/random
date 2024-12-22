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
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
