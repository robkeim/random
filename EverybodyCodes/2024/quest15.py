import heapq
from collections import defaultdict, deque


def part1():
    grid = [line.strip() for line in open("quest15_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    seen = set()
    to_process = deque()

    for c in range(num_c):
        if grid[0][c] == ".":
            to_process.append((0, 0, c))

    while to_process:
        cost, r, c = to_process.popleft()

        if (r, c) in seen:
            continue

        seen.add((r, c))

        if grid[r][c] == "H":
            print(cost * 2)
            break

        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] != "#":
                to_process.append((cost + 1, next_r, next_c))


def part2():
    grid = [line.strip() for line in open("quest15_p2.txt").readlines()]
    solve_parts_2_and_3(grid)


def part3():
    grid = [line.strip() for line in open("quest15_p3.txt").readlines()]
    solve_parts_2_and_3(grid)


def solve_parts_2_and_3(grid):
    num_r = len(grid)
    num_c = len(grid[0])
    unique_types = set()
    special_nodes = dict()
    start_c = None

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] not in "#.~":
                special_nodes[(r, c)] = grid[r][c]
                unique_types.add(grid[r][c])

    for c in range(num_c):
        if grid[0][c] == ".":
            special_nodes[(0, c)] = "S"
            start_c = c

    distances = defaultdict(set)

    for i, (r1, c1) in enumerate(special_nodes):
        for j, (r2, c2)  in enumerate(special_nodes):
            if j <= i:
                continue

            type1 = special_nodes[(r1, c1)]
            type2 = special_nodes[(r2, c2)]

            if type1 == type2:
                continue

            distance = bfs(grid, r1, c1, r2, c2)

            distances[(r1, c1)].add((r2, c2, distance))
            distances[(r2, c2)].add((r1, c1, distance))

    global_seen = set()
    to_process = []
    heapq.heappush(to_process, (0, 0, start_c, set()))

    processed = 0
    while to_process:
        processed += 1

        if processed % 100_000 == 0:
            print(f"Processed: {processed}")
        cost, r, c, seen = heapq.heappop(to_process)

        if (r, c, frozenset(seen)) in global_seen:
            continue

        global_seen.add((r, c, frozenset(seen)))

        if len(seen) > len(unique_types):
            print(cost)
            break

        if len(seen) == len(unique_types):
            for next_r, next_c, distance in distances[(r, c)]:
                if special_nodes[(next_r, next_c)] == "S":
                    next_seen = seen.copy()
                    next_seen.add("S")
                    heapq.heappush(to_process, (cost + distance, next_r, next_c, next_seen))

            continue

        for next_r, next_c, distance in distances[(r, c)]:
            next_type = special_nodes[(next_r, next_c)]

            if next_type in seen or next_type == "S":
                continue

            next_seen = seen.copy()
            next_seen.add(next_type)
            heapq.heappush(to_process, (cost + distance, next_r, next_c, next_seen))


def bfs(grid, start_r, start_c, target_r, target_c):
    num_r = len(grid)
    num_c = len(grid[0])
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    seen = set()
    to_process = deque()
    to_process.append((0, start_r, start_c))

    while to_process:
        cost, r, c = to_process.popleft()

        if (r, c) in seen:
            continue

        seen.add((r, c))

        if target_r == r and target_c == c:
            return cost

        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in seen and grid[next_r][next_c] not in "#~":
                to_process.append((cost + 1, next_r, next_c))

    assert False, "No path found"


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
