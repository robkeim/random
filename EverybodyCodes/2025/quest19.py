import sys
from functools import lru_cache


walls = dict()


def part1():
    lines = [line.strip() for line in open("quest19_p1.txt").readlines()]

    for line in lines:
        col, low, high = line.split(",")
        walls[int(col)] = (int(low) - 1, int(low) + int(high))

    print(min_flaps(int(lines[-1].split(",")[0]), 0, 0, 0))


@lru_cache(maxsize=None)
def min_flaps(target_row, cur_row, cur_height, cur_flaps):
    if cur_row == target_row:
        low, high = walls[cur_row]

        if low < cur_height < high:
            return cur_flaps
        else:
            return sys.maxsize

    if cur_row in walls:
        low, high = walls[cur_row]

        if not (low < cur_height < high):
            return sys.maxsize

    if cur_height < 0:
        return sys.maxsize

    return min(min_flaps(target_row, cur_row + 1, cur_height + 1, cur_flaps + 1), min_flaps(target_row, cur_row + 1, cur_height - 1, cur_flaps))


def part2():
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
