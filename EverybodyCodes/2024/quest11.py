from collections import defaultdict

pairings = defaultdict(list)

def part1():
    lines = [line.strip() for line in open("quest11_p1.txt").readlines()]

    for line in lines:
        start, end = line.split(":")
        for item in end.split(","):
            pairings[start].append(item)

    print(expand("A", 4))


def expand(termine, num_days):
    if num_days == 0:
        return 1

    return sum([expand(value, num_days - 1) for value in pairings[termine]])


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
