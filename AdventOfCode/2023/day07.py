from collections import Counter


def part1():
    lines = [line.strip() for line in open("day07.txt").readlines()]
    lines = [(convert_to_hex(line.split()[0]), int(line.split()[1])) for line in lines]
    print(lines)

    five_of_a_kind = []
    four_of_a_kind = []
    full_house = []
    three_of_a_kind = []
    two_pair = []
    one_pair = []
    high_card = []

    for hand, rank in lines:
        values = Counter(hand)

        if len(values) == 1:
            five_of_a_kind.append((hand, rank))
        elif len(values) == 2:
            found = False
            for key in values:
                if values[key] == 4:
                    four_of_a_kind.append((hand, rank))
                    found = True
                    break

            if not found:
                full_house.append((hand, rank))
        elif len(values) == 3:
            found = False
            for key in values:
                if values[key] == 3:
                    three_of_a_kind.append((hand, rank))
                    found = True
                    break

            if not found:
                two_pair.append((hand, rank))
        elif len(values) == 4:
            one_pair.append((hand, rank))
        else:
            high_card.append((hand, rank))

    sorted_hands = []
    sorted_hands += sorted(high_card, key=lambda x: int(x[0], 16))
    sorted_hands += sorted(one_pair, key=lambda x: int(x[0], 16))
    sorted_hands += sorted(two_pair, key=lambda x: int(x[0], 16))
    sorted_hands += sorted(three_of_a_kind, key=lambda x: int(x[0], 16))
    sorted_hands += sorted(full_house, key=lambda x: int(x[0], 16))
    sorted_hands += sorted(four_of_a_kind, key=lambda x: int(x[0], 16))
    sorted_hands += sorted(five_of_a_kind, key=lambda x: int(x[0], 16))

    result = 0

    for i in range(len(sorted_hands)):
        result += sorted_hands[i][1] * (i + 1)

    print(result)


def convert_to_hex(card):
    return card.replace("A", "E").replace("K", "D").replace("Q", "C").replace("J", "B").replace("T", "A")


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
