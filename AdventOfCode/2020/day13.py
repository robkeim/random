def part1():
    lines = [line.strip() for line in open("day13.txt").readlines()]
    min_time = int(lines[0])
    buses = [int(bus) for bus in lines[1].split(",") if bus != "x"]

    min_wait = float("inf")
    min_bus = -1

    for bus in buses:
        wait_time = bus - (min_time % bus)

        if wait_time < min_wait:
            min_wait = wait_time
            min_bus = bus

    print(min_bus * min_wait)


# After trying a bunch of differnet things and reading about the Chinese Remainder Theorem, I'm giving up on trying
# to implement this problem and pulling a solution from here:
# https://dev.to/qviper/advent-of-code-2020-python-solution-day-13-24k4
def part2():
    buses = open("day13.txt").readlines()[1].strip().split(",")

    mods = { int(bus): -i % int(bus) for i, bus in enumerate(buses) if bus != "x" }
    values = list(reversed(sorted(mods)))
    result = mods[values[0]]
    cycle = values[0]

    for bus in values[1:]:
        while result % bus != mods[bus]:
            result += cycle
        cycle *= bus

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
