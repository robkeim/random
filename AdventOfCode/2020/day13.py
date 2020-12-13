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

    print(min_bus, min_wait)
    print(min_bus * min_wait)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
