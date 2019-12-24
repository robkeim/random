def part1():
    lines = [line.strip() for line in open("day24.txt").readlines()]

    states = set()
    bugs = set()

    for y in range(len(lines)):
        for x in range(len(lines[0])):
            if lines[y][x] == "#":
                bugs.add((x, y))

    while hash_state(bugs) not in states:
        states.add(hash_state(bugs))
        bugs = calculate_next_iteration(bugs)

    biodiversity_rating = 0

    for bug in bugs:
        biodiversity_rating += 2 ** (5 * bug[1] + bug[0])

    print(biodiversity_rating)


def calculate_next_iteration(bugs):
    deltas = [(-1, 0), (1, 0), (0, -1), (0, 1)]
    result = set()

    for y in range(5):
        for x in range(5):
            if x == 4 and y == 0:
                x = 4

            cur = "#" if (x, y) in bugs else "."
            num_bug_neighbors = 0

            for delta in deltas:
                if (x + delta[0], y + delta[1]) in bugs:
                    num_bug_neighbors += 1

            if cur == "#":
                if num_bug_neighbors == 1:
                    result.add((x, y))
            else:
                assert cur == "."
                if num_bug_neighbors == 1 or num_bug_neighbors == 2:
                    result.add((x, y))

    return result


def hash_state(bugs):
    result = ""
    for y in range(5):
        for x in range(5):
            if (x, y) in bugs:
                result += "#"
            else:
                result += "."
        result += "\n"

    return result


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
