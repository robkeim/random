def part1():
    p1, p2 = parse_input()

    while len(p1) > 0 and len(p2) > 0:
        p1_turn = p1.pop(0)
        p2_turn = p2.pop(0)

        if p1_turn > p2_turn:
            p1.append(p1_turn)
            p1.append(p2_turn)
        elif p2_turn > p1_turn:
            p2.append(p2_turn)
            p2.append(p1_turn)
        else:
            assert False, "Deck shouldn't contain ties"

    winner = p1 if len(p1) > 0 else p2

    print(score_game(winner))


def part2():
    p1, p2 = parse_input()
    _, cards = play_game(p1, p2)

    print(score_game(cards))


def parse_input():
    lines = [line.strip() for line in open("day22.txt").readlines()]

    p1 = []
    p2 = []
    is_p2 = False

    for line in lines:
        if "Player" in line:
            if "2" in line:
                is_p2 = True

            continue

        if line == "":
            continue

        if is_p2:
            p2.append(int(line))
        else:
            p1.append(int(line))

    return p1, p2


def play_game(p1, p2):
    seen = set()
    while len(p1) > 0 and len(p2) > 0:
        hash_value = get_hash(p1, p2)

        if hash_value in seen:
            return "p1", p1

        seen.add(hash_value)

        p1_turn = p1.pop(0)
        p2_turn = p2.pop(0)

        if p1_turn <= len(p1) and p2_turn <= len(p2):
            winner, _ = play_game(p1[:p1_turn], p2[:p2_turn])

            if winner == "p1":
                p1.append(p1_turn)
                p1.append(p2_turn)
            elif winner == "p2":
                p2.append(p2_turn)
                p2.append(p1_turn)
            else:
                assert False, "Invalid winner: " + winner
        else:
            if p1_turn > p2_turn:
                p1.append(p1_turn)
                p1.append(p2_turn)
            elif p2_turn > p1_turn:
                p2.append(p2_turn)
                p2.append(p1_turn)
            else:
                assert False, "Deck shouldn't contain ties"

    if len(p1) > 0:
        return "p1", p1
    else:
        return "p2", p2


def get_hash(p1, p2):
    return tuple(p1), tuple(p2)


def score_game(cards):
    score = 0

    for i, value in enumerate(cards[::-1]):
        score += (i + 1) * value

    return score


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
