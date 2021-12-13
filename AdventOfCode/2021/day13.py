import re


def part1():
    print(len(run_folds(True)))


def part2():
    dots = run_folds(False)

    max_x = max_y = 0

    for x, y in dots:
        max_x = max(max_x, x)
        max_y = max(max_y, y)

    for i in range(max_y + 1):
        line = ""

        for j in range(max_x + 1):
            line += "X" if (j, i) in dots else " "

        print(line)


def run_folds(only_first_fold):
    lines = [line.strip() for line in open("day13.txt").readlines()]

    dots = set()
    folds = []

    for line in lines:
        coordinate_match = re.match(r"(\d+),(\d+)", line)
        fold_match = re.match(r"fold along ([xy])=(\d+)", line)

        if coordinate_match:
            dots.add((int(coordinate_match.group(1)), int(coordinate_match.group(2))))
        elif line == "":
            pass  # Skip the empty line
        elif fold_match:
            folds.append((fold_match.group(1), int(fold_match.group(2))))
        else:
            raise Exception("Invalid format for a line: " + line)

    for fold_dir, fold_line in folds:
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

        dots = next_dots

        if only_first_fold:
            break

    return dots


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
