from collections import defaultdict

def part1():
    lines = [line.strip() for line in open("quest11_p1.txt").readlines()]
    pairings = defaultdict(list)

    for line in lines:
        start, end = line.split(":")
        for item in end.split(","):
            pairings[start].append(item)

    print(expand("A", 4, pairings))


def expand(termine, num_days, pairings):
    if num_days == 0:
        return 1

    return sum([expand(value, num_days - 1, pairings) for value in pairings[termine]])


def part2():
    lines = [line.strip() for line in open("quest11_p2.txt").readlines()]
    pairings = defaultdict(list)

    for line in lines:
        start, end = line.split(":")
        for item in end.split(","):
            pairings[start].append(item)

    print(expand("Z", 10, pairings))


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
