def part1():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    points = {
        "X": 1,
        "Y": 2,
        "Z": 3
    }

    wins = {"C X", "A Y", "B Z"}
    ties = {"A X", "B Y", "C Z"}

    result = 0

    for game in lines:
        if game in wins:
            result += 6
        elif game in ties:
            result += 3

        result += points[game[2]]

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
