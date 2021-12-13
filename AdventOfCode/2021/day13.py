import re


def part1():
    lines = [line.strip() for line in open("day13.txt").readlines()]

    dots = set()
    fold_dir = fold_line = None

    for line in lines:
        coordinate_match = re.match(r"(\d+),(\d+)", line)
        fold_match = re.match(r"fold along ([xy])=(\d+)", line)

        if coordinate_match:
            dots.add((int(coordinate_match.group(1)), int(coordinate_match.group(2))))
        elif line == "":
            pass  # Skip the empty line
        elif fold_match:
            fold_dir = fold_match.group(1)
            fold_line = int(fold_match.group(2))
            break  # Keep the first fold only for part 1
        else:
            raise Exception("Invalid format for a line: " + line)

    next_dots = set()

    for x, y in dots:
        if fold_dir == "x":
            if x <= fold_line:
                next_dots.add((x, y))
            else:
                next_dots.add((fold_line - (x - fold_line), y))
        elif fold_dir == "y":
            if y <= fold_line:
                next_dots.add((x, y))
            else:
                next_dots.add((x, fold_line - (y - fold_line)))
        else:
            raise Exception("Invalid fold direction: " + fold_dir)

    print(len(next_dots))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
