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
        sorted_hands.append((score_hand(hand, False), hand, rank))

    sorted_hands.sort(key=lambda x: (-x[0], int(x[1], 16)))

    result = 0

    for i in range(len(sorted_hands)):
        result += sorted_hands[i][2] * (i + 1)

    print(result)


def convert_to_hex(hand, has_jokers):
    joker_replacement = "1" if has_jokers else "B"
    return hand.replace("A", "E").replace("K", "D").replace("Q", "C").replace("J", joker_replacement).replace("T", "A")


def part2():
    lines = [line.strip() for line in open("day07.txt").readlines()]
    lines = [(convert_to_hex(line.split()[0], True), int(line.split()[1])) for line in lines]

    sorted_hands = []

    for hand, rank in lines:
        sorted_hands.append((score_hand(hand, True), hand, rank))

    sorted_hands.sort(key=lambda x: (-x[0], int(x[1], 16)))

    result = 0

    for i in range(len(sorted_hands)):
        result += sorted_hands[i][2] * (i + 1)

    print(result)


def score_hand(hand, replace_jokers):
    values = sorted(Counter(hand).values(), reverse=True)

    if len(values) == 1:
        return five_of_a_kind

    if replace_jokers:
        counts = Counter(hand)
        num_jokers = counts["1"]
        del counts["1"]

        values = sorted(counts.values(), reverse=True)
        values[0] += num_jokers

    if values == [5]:
        return five_of_a_kind
    elif values == [4, 1]:
        return four_of_a_kind
    elif values == [3, 2]:
        return full_house
    elif values == [3, 1, 1]:
        return three_of_a_kind
    elif values == [2, 2, 1]:
        return two_pair
    elif values == [2, 1, 1, 1]:
        return one_pair
    else:
        return high_card


def score_hand_tests():
    tests = [
        ("JJJJJ", five_of_a_kind),
        ("JJJJ4", five_of_a_kind),
        ("JJJ44", five_of_a_kind),
        ("JJ444", five_of_a_kind),
        ("J4444", five_of_a_kind),
        ("JJJ45", four_of_a_kind),
        ("JJ445", four_of_a_kind),
        ("J4445", four_of_a_kind),
        ("JJ456", three_of_a_kind),
        ("JJ455", four_of_a_kind),
        ("T55J5", four_of_a_kind),
        ("KTJJT", four_of_a_kind),
        ("QQQJA", four_of_a_kind),
        ("J2345", one_pair),
        ("J4455", full_house),
        ("J4456", three_of_a_kind)
    ]

    for hand, expected_result in tests:
        hex_hand = convert_to_hex(hand, True)
        actual_result = score_hand(hex_hand, True)

        if actual_result != expected_result:
            raise Exception("Expected: {}, Actual: {} for hand {}".format(expected_result, actual_result, hand))


def main():
    part1()
    part2()
    # score_hand_tests()


if __name__ == "__main__":
    main()
