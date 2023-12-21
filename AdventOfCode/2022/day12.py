import heapq


def part1():
    grid = [list(line.strip()) for line in open("day12.txt").readlines()]
    rows = len(grid)
    cols = len(grid[0])

    for r in range(rows):
        for c in range(cols):
            if grid[r][c] == "S":
                start = (r, c)
                grid[r][c] = "a"
            elif grid[r][c] == "E":
                end = (r, c)
                grid[r][c] = "z"

    to_process = [(0, start[0], start[1])]
    heapq.heapify(to_process)
    deltas = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    seen = set()

    while len(to_process) > 0:
        steps, r, c = heapq.heappop(to_process)

        if (r, c) == end:
            print(steps)
            return

        if (r, c) in seen:
            continue

        seen.add((r, c))

        for dr, dc in deltas:
            rr = r + dr
            cc = c + dc

            if 0 <= rr < rows and 0 <= cc < cols and (rr, cc) not in seen:
                cur_value = ord(grid[r][c])
                next_value = ord(grid[rr][cc])

                if cur_value + 1 >= next_value:
                    heapq.heappush(to_process, (steps + 1, rr, cc))

    assert False, "No path found"


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
