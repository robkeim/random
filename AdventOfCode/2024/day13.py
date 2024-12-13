import math
import re
import sys
from functools import lru_cache


def part1():
    lines = [line.strip() for line in open("day13.txt").readlines()]
    line_index = 0
    needed_tokens = 0

    while line_index < len(lines):
        match = re.match(r"Button A: X\+(\d+), Y\+(\d+)", lines[line_index])
        a_x, a_y = int(match.group(1)), int(match.group(2))

        match = re.match(r"Button B: X\+(\d+), Y\+(\d+)", lines[line_index + 1])
        b_x, b_y = int(match.group(1)), int(match.group(2))

        match = re.match(r"Prize: X=(\d+), Y=(\d+)", lines[line_index + 2])
        x, y = int(match.group(1)), int(match.group(2))

        num_tokens = calculate_num_tokens(a_x, a_y, b_x, b_y, x, y, 0)

        if num_tokens != sys.maxsize:
            needed_tokens += num_tokens

        line_index += 4

    print(needed_tokens)


@lru_cache(1_000_000)
def calculate_num_tokens(a_x, a_y, b_x, b_y, x, y, num_pushes):
    if x < 0 or y < 0:
        return sys.maxsize

    if x == 0 and y == 0:
        return num_pushes

    push_a = calculate_num_tokens(a_x, a_y, b_x, b_y, x - a_x, y - a_y, num_pushes + 3)
    push_b = calculate_num_tokens(a_x, a_y, b_x, b_y, x - b_x, y - b_y, num_pushes + 1)

    return min(push_a, push_b)


def part2():
    lines = [line.strip() for line in open("day13.txt").readlines()]
    line_index = 0
    needed_tokens = 0

    while line_index < len(lines):
        match = re.match(r"Button A: X\+(\d+), Y\+(\d+)", lines[line_index])
        a1, a2 = int(match.group(1)), int(match.group(2))

        match = re.match(r"Button B: X\+(\d+), Y\+(\d+)", lines[line_index + 1])
        b1, b2 = int(match.group(1)), int(match.group(2))

        match = re.match(r"Prize: X=(\d+), Y=(\d+)", lines[line_index + 2])
        c1, c2 = 10000000000000 + int(match.group(1)), 10000000000000 + int(match.group(2))

        # Apply Cramer's rule: https://en.wikipedia.org/wiki/Cramer%27s_rule
        a = (c1 * b2 - b1 * c2) / (a1 * b2 - b1 * a2)
        b = (a1 * c2 - c1 * a2) / (a1 * b2 - b1 * a2)
        num_tokens = 3 * a + b

        # Only consider the results for whole numbers of pushes
        if a == math.floor(a) and b == math.floor(b):
            needed_tokens += int(num_tokens)

        line_index += 4

    print(needed_tokens)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
