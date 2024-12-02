def part1():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    safe = 0

    for line in lines:
        levels = [int(value) for value in line.split()]
        if is_report_safe(levels, True, False) or is_report_safe(levels, False, False):
            safe += 1

    print(safe)


def part2():
    lines = [line.strip() for line in open("day02.txt").readlines()]

    safe = 0

    for line in lines:
        levels = [int(value) for value in line.split()]

        if is_report_safe(levels, True, True) or is_report_safe(levels[1:], True, False) or is_report_safe(levels, False, True) or is_report_safe(levels[1:], False, False):
            safe += 1

    print(safe)


def is_report_safe(levels, is_increasing, can_skip):
    i = 0

    while i < len(levels) - 1:
        valid = is_valid_interval(is_increasing, levels[i], levels[i + 1])

        if not valid:
            if not can_skip:
                return False

            if i < len(levels) - 2 and not is_valid_interval(is_increasing, levels[i], levels[i + 2]):
                return False

            can_skip = False
            i += 1

        i += 1

    return True


def is_valid_interval(is_increasing, first, second):
    if (is_increasing and first >= second) or (not is_increasing and first <= second):
        return False

    return 1 <= abs(first - second) <= 3


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
