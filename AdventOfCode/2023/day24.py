def part1():
    lines = [parse_line(line) for line in open("day24.txt").readlines()]

    min_point = 200000000000000
    max_point = 400000000000000
    matches = 0

    for i in range(len(lines)):
        for j in range(i + 1, len(lines)):
            px1, py1, _, vx1, vy1, _ = lines[i]
            px2, py2, _, vx2, vy2, _ = lines[j]

            m1 = vy1 / vx1
            m2 = vy2 / vx2

            if m1 == m2:
                continue

            x = ((-m2 * px2) + py2 + (m1 * px1) - py1) / (m1 - m2)
            past = ((x - px1) / vx1 < 0) or ((x - px2) / vx2 < 0)

            y = m1 * (x - px1) + py1
            past = past or ((y - py1) / vy1 < 0) or ((y - py2) / vy2 < 0)

            if not past and min_point <= x <= max_point and min_point <= y <= max_point:
                matches += 1

    print(matches)


def parse_line(line):
    line = line.replace(",", "").replace("@", "")
    return [int(value) for value in line.split()]


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
