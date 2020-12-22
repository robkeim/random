def part1():
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

    score = 0

    for i, value in enumerate(winner[::-1]):
        score += (i + 1) * value

    print(score)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
