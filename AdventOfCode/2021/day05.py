import re


def part1():
    count_overlap(False)


def part2():
    count_overlap(True)


def count_overlap(include_diagonal):
    lines = [line.strip() for line in open("day05.txt").readlines()]

    points = set()
    overlap = set()

    for line in lines:
        match = re.match("([0-9]+),([0-9]+) -> ([0-9]+),([0-9]+)", line)

        if not match:
            raise Exception("Invalid line format: " + line)

        x1, y1, x2, y2 = int(match.group(1)), int(match.group(2)), int(match.group(3)), int(match.group(4))

        if x1 == x2:
            max_y = max(y1, y2)
            min_y = min(y1, y2)

            while max_y >= min_y:
                coordinate = (x1, max_y)

                if coordinate in points:
                    overlap.add(coordinate)
                else:
                    points.add(coordinate)

                max_y -= 1
        elif y1 == y2:
            max_x = max(x1, x2)
            min_x = min(x1, x2)

            while max_x >= min_x:
                coordinate = (max_x, y1)

                if coordinate in points:
                    overlap.add(coordinate)
                else:
                    points.add(coordinate)

                max_x -= 1
        elif include_diagonal:
            steps = abs(x1 - x2) + 1
            delta_x = 1 if x2 > x1 else -1
            delta_y = 1 if y2 > y1 else -1

            for i in range(steps):
                coordinate = (x1 + i * delta_x, y1 + i * delta_y)

                if coordinate in points:
                    overlap.add(coordinate)
                else:
                    points.add(coordinate)

    print(len(overlap))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
