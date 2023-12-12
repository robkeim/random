def part1():
    lines = [line.strip().split() for line in open("day12.txt").readlines()]
    result = 0

    for pattern, springs in lines:
        result += count_arrangements(pattern, [int(spring) for spring in springs.split(",")])

    print(result)


def count_arrangements(pattern, springs):
    # No more springs left to place, check there are no springs remaining in the pattern
    if len(springs) == 0:
        return 1 if "#" not in pattern else 0

    spring = springs[0]

    # Spring is too big to be placed
    if spring > len(pattern):
        return 0

    result = 0

    # Place right away
    if "." not in pattern[:spring]:
        if len(pattern) == spring:
            result += count_arrangements("", springs[1:])
        elif pattern[spring] != "#":
            result += count_arrangements(pattern[spring + 1:], springs[1:])

    # Skip a space
    if pattern[0] != "#":
        result += count_arrangements(pattern[1:], springs)

    return result


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
