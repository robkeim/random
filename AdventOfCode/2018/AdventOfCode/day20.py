from collections import defaultdict, deque
from functools import lru_cache


doors = defaultdict(set)
rooms = set()


def part1():
    path = open("day20.txt").read().strip()[1:-1]
    rooms.add((0, 0))
    process_path(0, 0, path)

    seen = set()
    max_distance = 0
    one_thousand_doors = 0
    to_process = deque()
    to_process.append((0, 0, 0))

    while len(to_process) > 0:
        num_steps, r, c = to_process.popleft()

        if (r, c) in seen:
            continue

        seen.add((r, c))
        max_distance = max(max_distance, num_steps)
        if num_steps >= 1000:
            one_thousand_doors += 1

        for (next_r, next_c) in doors[(r, c)]:
            to_process.append((num_steps + 1, next_r, next_c))

    print(max_distance)
    print(one_thousand_doors)


@lru_cache(1_000_000)
def process_path(r, c, path):
    dirs = {
        "N": (-1, 0),
        "E": (0, 1),
        "S": (1, 0),
        "W": (0, -1)
    }

    i = 0

    while i < len(path):
        if path[i] in "NEWS":
            dr, dc = dirs[path[i]]
            next_r = r + dr
            next_c = c + dc

            doors[(r, c)].add((next_r, next_c))
            rooms.add((next_r, next_c))

            r = next_r
            c = next_c
        elif path[i] == "(":
            count = 1
            j = i + 1
            options = []

            while True:
                if path[j] == "(":
                    count += 1
                elif path[j] == "|":
                    if count == 1:
                        options.append(path[i + 1:j])
                        i = j
                elif path[j] == ")":
                    count -= 1

                    if count == 0:
                        options.append(path[i + 1:j])
                        break

                j += 1

            for option in options:
                process_path(r, c, option + path[j + 1:])
            break
        else:
            raise Exception(f"Unexpected character: {path[i]}")

        i += 1


def part2():
    # Done as part of part 1 as the code is almost identical
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
