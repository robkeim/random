import re
from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    result = 0

    for line in lines:
        game_id = int(line.split(":")[0].split()[1]) # Ugly but it works :)
        max_dict = defaultdict(int)

        rounds = line.split(";")

        for cur_round in rounds:
            for match in re.findall(r"(\d+) (blue|green|red)", cur_round):
                max_dict[match[1]] = max(max_dict[match[1]], int(match[0]))

        if max_dict["red"] <= 12 and max_dict["green"] <= 13 and max_dict["blue"] <= 14:
            result += game_id

    print(result)


def part2():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    result = 0

    for line in lines:
        max_dict = defaultdict(int)

        rounds = line.split(";")

        for cur_round in rounds:
            for match in re.findall(r"(\d+) (blue|green|red)", cur_round):
                max_dict[match[1]] = max(max_dict[match[1]], int(match[0]))

        result += max_dict["red"] * max_dict["green"] * max_dict["blue"]

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
