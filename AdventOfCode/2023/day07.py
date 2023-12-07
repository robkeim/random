from collections import Counter

five_of_a_kind = 1
four_of_a_kind = 2
full_house = 3
three_of_a_kind = 4
two_pair = 5
one_pair = 6
high_card = 7


def part1():
    lines = [line.strip() for line in open("day07.txt").readlines()]
    lines = [(convert_to_hex(line.split()[0], False), int(line.split()[1])) for line in lines]

    sorted_hands = []

    for hand, rank in lines:
        cards = Counter(hand)

        if len(cards) == 1:
            sorted_hands.append((five_of_a_kind, hand, rank))
        elif len(cards) == 2:
            if 4 in cards.values():
                sorted_hands.append((four_of_a_kind, hand, rank))
            else:
                sorted_hands.append((full_house, hand, rank))
        elif len(cards) == 3:
            if 3 in cards.values():
                sorted_hands.append((three_of_a_kind, hand, rank))
            else:
                sorted_hands.append((two_pair, hand, rank))
        elif len(cards) == 4:
            sorted_hands.append((one_pair, hand, rank))
        else:
            sorted_hands.append((high_card, hand, rank))

    sorted_hands.sort(key=lambda x: (-x[0], int(x[1], 16)))

    result = 0

    for i in range(len(sorted_hands)):
        result += sorted_hands[i][2] * (i + 1)

    print(result)


def convert_to_hex(card, has_jokers):
    joker_replacement = "1" if has_jokers else "B"
    return card.replace("A", "E").replace("K", "D").replace("Q", "C").replace("J", joker_replacement).replace("T", "A")


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
