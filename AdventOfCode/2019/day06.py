from collections import defaultdict


def part1():
    orbits = [line.strip().split(")") for line in open("aoc.txt").readlines()]
    dependencies = dict()

    for orbit in orbits:
        dependencies[orbit[1]] = orbit[0]

    total_orbits = 0

    for dependency in dependencies:
        total_orbits += 1
        while dependencies[dependency] in dependencies:
            total_orbits +=1
            dependency = dependencies[dependency]

    print(total_orbits)


def part2():
    orbits = [line.strip().split(")") for line in open("aoc.txt").readlines()]
    dependencies = defaultdict(set)

    for orbit in orbits:
        if orbit[1] == "YOU":
            you = orbit[0]

        dependencies[orbit[0]].add(orbit[1])
        dependencies[orbit[1]].add(orbit[0])

    visited = set()
    queue = [(you, 0)]

    while True:
        item, distance = queue[0]
        queue = queue[1:]

        if item in visited:
            continue
        else:
            visited.add(item)

        if item in dependencies:
            for value in dependencies[item]:
                if value == "SAN":
                    print(distance)
                    return

                queue.append((value, distance + 1))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
