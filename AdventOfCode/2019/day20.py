from collections import defaultdict, deque


def part1():
    lines = [line.strip("\n") for line in open("day20.txt").readlines()]
    num_r = len(lines)
    num_c = len(lines[0])
    grid = defaultdict(str)

    for r in range(num_r):
        for c in range(num_c):
            grid[(r, c)] = lines[r][c]

    paths = set()
    portals = defaultdict(set)
    coordinates_to_portal = dict()

    for r in range(num_r):
        for c in range(num_c):
            if grid[(r, c)] == ".":
                paths.add((r, c))
            elif "A" <= grid[(r, c)] <= "Z":
                for dr, dc in [(0, 1), (0, -1), (1, 0), (-1, 0)]:
                    second_letter = grid[(r + dr, c + dc)]
                    coordinate = grid[(r + 2 * dr, c + 2 * dc)]

                    if "A" <= second_letter <= "Z" and coordinate == ".":
                        portal = grid[(r, c)] + grid[(r + dr, c + dc)]

                        if dr == -1 or dc == -1:
                            portal = portal[::-1]

                        coordinates = (r + 2 * dr, c + 2 * dc)

                        if portal == "AA":
                            start_r, start_c = coordinates
                        elif portal == "ZZ":
                            target_r, target_c = coordinates
                        else:
                            portals[portal].add(coordinates)
                            coordinates_to_portal[coordinates] = portal
                        break

    to_process = deque()
    to_process.append((0, start_r, start_c))
    seen = set()

    while len(to_process):
        cost, r, c = to_process.popleft()

        if (r, c) in seen:
            continue

        seen.add((r, c))

        if r == target_r and c == target_c:
            print(cost)
            break

        if (r, c) in coordinates_to_portal:
            for (next_r, next_c) in portals[coordinates_to_portal[(r, c)]]:
                to_process.append((cost + 1, next_r, next_c))

        for dr, dc in [(0, 1), (0, -1), (1, 0), (-1, 0)]:
            next_r = r + dr
            next_c = c + dc

            if (next_r, next_c) in paths:
                to_process.append((cost + 1, next_r, next_c))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
