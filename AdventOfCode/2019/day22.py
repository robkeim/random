import re


def part1():
    deck_size = 10007

    lines = [line.strip() for line in open("day22.txt").readlines()]

    result = 2019

    for line in lines:
        if line == "deal into new stack":
            result = new_stack(result, deck_size)
            continue

        num = int(re.search("-?\d+", line).group(0))

        if line.startswith("cut "):
            result = cut_n(result, num, deck_size)
        elif line.startswith("deal with increment "):
            result = increment_n(result, num, deck_size)
        else:
            raise Exception("Unknown line format: " + line)

    print(result)

def new_stack(index, deck_size):
    return deck_size - index - 1


def cut_n(index, n, deck_size):
    return (index - n + deck_size) % deck_size


def increment_n(index, n, deck_size):
    return (index * n) % deck_size


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
