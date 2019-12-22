import re


def part1():
    lines = [line.strip() for line in open("day22.txt").readlines()]
    cards = list(range(10007))

    for line in lines:
        if line == "deal into new stack":
            cards = cards[::-1]
            continue

        num = int(re.search("-?\d+", line).group(0))

        if line.startswith("cut "):
            cards = cards[num:] + cards[:num]
        elif line.startswith("deal with increment "):
            result = [0] * len(cards)
            index = 0
            for card in cards:
                result[index] = card
                index = (index + num) % len(cards)
            cards = result
        else:
            raise Exception("Unknown line format: " + line)

    print(cards.index(2019))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
