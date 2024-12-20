from collections import deque

def part1():
    grid = [list(line.strip()) for line in open("day20.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    start_r = start_c = end_r = end_c = None

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "S":
                start_r = r
                start_c = c
                grid[r][c] = "."
            elif grid[r][c] == "E":
                end_r = r
                end_c = c
                grid[r][c] = "."

    assert start_r and start_c and end_r and end_c, "Did not find start or end points"

    distances = get_distances(grid, start_r, start_c, end_r, end_c)
    shortest_distance = max(distances.values())

    print(find_num_cheats(grid, start_r, start_c, end_r, end_c, shortest_distance, distances))


def get_distances(grid, start_r, start_c, end_r, end_c):
    num_r = len(grid)
    num_c = len(grid[0])
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    to_process = deque([(0, start_r, start_c)])
    seen = set()
    distances = dict()

    while to_process:
        num_steps, r, c = to_process.popleft()

        if (r, c) in seen:
            continue

        seen.add((r, c))
        distances[(r, c)] = num_steps

        if r == end_r and c == end_c:
            return distances

        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] == ".":
                to_process.append((num_steps + 1, next_r, next_c))

    assert False, "Didn't find a path through the maze"


def find_num_cheats(grid, start_r, start_c, end_r, end_c, shortest_distance, distances):
    num_r = len(grid)
    num_c = len(grid[0])
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    to_process = deque([(0, start_r, start_c)])
    seen = set()

    num_paths = 0

    while to_process:
        num_steps, r, c = to_process.popleft()

        if r == end_r and c == end_c:
            break

        if (r, c) in seen:
            continue

        seen.add((r, c))

        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] == ".":
                to_process.append((num_steps + 1, next_r, next_c))
            elif 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] == "#":
                next_r += dr
                next_c += dc

                if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] == ".":
                    total_steps = num_steps + 2 + (shortest_distance - distances[(next_r, next_c)])

                    if total_steps + 100 <= shortest_distance:
                        num_paths += 1

    return num_paths

def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
