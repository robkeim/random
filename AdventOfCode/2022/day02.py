def part1():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    points = {
        "X": 1,  # Rock
        "Y": 2,  # Paper
        "Z": 3   # Scissors
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
    lines = [line.strip() for line in open("day02.txt").readlines()]

    # X -> lose
    # Y -> tie
    # Z -> win
    points = {
        "A X": 0 + 3,
        "A Y": 3 + 1,
        "A Z": 6 + 2,
        "B X": 0 + 1,
        "B Y": 3 + 2,
        "B Z": 6 + 3,
        "C X": 0 + 2,
        "C Y": 3 + 3,
        "C Z": 6 + 1,
    }

    print(sum([points[game] for game in lines]))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
