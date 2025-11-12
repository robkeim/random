def part1():
    lines = [line.strip() for line in open("quest07_p1.txt").readlines()]
    names = lines[0].split(",")

    pairs = set()

    for line in lines[2:]:
        first, remaining = line.split(" > ")
        for char in remaining.split(","):
            pairs.add(first + char)

    for name in names:
        found = True
        for i in range(len(name) - 1):
            if name[i:i + 2] not in pairs:
                found = False
                break

        if found:
            print(name)
            return

    assert False, "No solution found"


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
