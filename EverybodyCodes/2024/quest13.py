import heapq


def part1():
    grid = [list(line.strip()) for line in open("quest13_p1.txt").readlines()]
    solve_part_one_and_two(grid)


def part2():
    grid = [list(line.strip()) for line in open("quest13_p2.txt").readlines()]
    solve_part_one_and_two(grid)


def solve_part_one_and_two(grid):
    num_r = len(grid)
    num_c = len(grid[0])
    start_r = start_c = end_r = end_c = None

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "S":
                start_r = r
                start_c = c
                grid[r][c] = 0
            elif grid[r][c] == "E":
                end_r = r
                end_c = c
                grid[r][c] = 0
            elif "0" <= grid[r][c] <= "9":
                grid[r][c] = int(grid[r][c])

    assert start_r != None and start_c != None and end_r != None and end_c != None, "Can't find start or end"

    end_points = set()
    end_points.add((end_r, end_c))

    print(dijkstra(grid, start_r, start_c, end_points))


def dijkstra(grid, start_r, start_c, end_points):
    num_r = len(grid)
    num_c = len(grid[0])
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    heap = [(0, start_r, start_c)]
    seen = set()

    while heap:
        cost, r, c = heapq.heappop(heap)

        if (r, c) in seen:
            continue

        seen.add((r, c))

        if (r, c) in end_points:
            return cost

        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] != "#":
                min_value = min(grid[r][c], grid[next_r][next_c])
                max_value = max(grid[r][c], grid[next_r][next_c])
                step_cost = min(max_value - min_value, abs(max_value - (min_value + 10)))

                heapq.heappush(heap, (cost + step_cost + 1, next_r, next_c))


def part3():
    grid = [list(line.strip()) for line in open("quest13_p3.txt").readlines()]
    end_points = []

    num_r = len(grid)
    num_c = len(grid[0])

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "S":
                end_points.append((r, c))
                grid[r][c] = 0
            elif grid[r][c] == "E":
                start_r = r
                start_c = c
                grid[r][c] = 0
            elif "0" <= grid[r][c] <= "9":
                grid[r][c] = int(grid[r][c])

    print(dijkstra(grid, start_r, start_c, end_points))


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
