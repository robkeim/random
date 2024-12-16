import heapq


def part1():
    grid = [line.strip() for line in open("day16.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    available_spaces = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "S":
                start_r, start_c = r, c

            if grid[r][c] == "E":
                target_r, target_c = r, c

            if grid[r][c] != "#":
                available_spaces.add((r, c))

    assert start_r and start_c and target_r and target_c, "Start or target not found in grid"

    directions = [(-1, 0), (0, 1), (1, 0), (0, -1)]
    seen = set()
    to_process = []
    heapq.heappush(to_process, (0, start_r, start_c, 1))

    while len(to_process) > 0:
        cost, r, c, direction = heapq.heappop(to_process)

        if (r, c, direction) in seen:
            continue

        seen.add((r, c, direction))

        if r == target_r and c == target_c:
            print(cost)
            break

        heapq.heappush(to_process, (cost + 1000, r, c, (direction - 1 + 4) % 4))
        heapq.heappush(to_process, (cost + 1000, r, c, (direction + 1) % 4))

        dr, dc = directions[direction]
        next_r = r + dr
        next_c = c + dc

        if (next_r, next_c) in available_spaces:
            heapq.heappush(to_process, (cost + 1, next_r, next_c, direction))

def part2():
    grid = [line.strip() for line in open("day16.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    available_spaces = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "S":
                start_r, start_c = r, c

            if grid[r][c] == "E":
                target_r, target_c = r, c

            if grid[r][c] != "#":
                available_spaces.add((r, c))

    assert start_r and start_c and target_r and target_c, "Start or target not found in grid"

    directions = [(-1, 0), (0, 1), (1, 0), (0, -1)]
    seen = dict()
    to_process = []
    path = set()
    path.add((start_r, start_c))
    heapq.heappush(to_process, (0, start_r, start_c, 1, path))
    result = set()
    best_cost = None

    while len(to_process) > 0:
        cost, r, c, direction, path = heapq.heappop(to_process)

        if (r, c, direction) in seen and seen[(r, c, direction)] < cost:
            continue

        seen[(r, c, direction)] = cost

        if r == target_r and c == target_c and (not best_cost or best_cost == cost):
            result = result | path
            best_cost = cost

        heapq.heappush(to_process, (cost + 1000, r, c, (direction - 1 + 4) % 4, path))
        heapq.heappush(to_process, (cost + 1000, r, c, (direction + 1) % 4, path))

        dr, dc = directions[direction]
        next_r = r + dr
        next_c = c + dc

        if (next_r, next_c) in available_spaces:
            next_path = path.copy()
            next_path.add((next_r, next_c))
            heapq.heappush(to_process, (cost + 1, next_r, next_c, direction, next_path))

    print(len(result))

def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
