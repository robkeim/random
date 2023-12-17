import heapq


def part1():
    grid = [[int(value) for value in list(line.strip())] for line in open("day17.txt").readlines()]
    print(find_shortest_path(grid))


def find_shortest_path(grid):
    rows = len(grid)
    cols = len(grid[0])
    seen = set()
    heap = []
    # cost, row, col, prev
    heapq.heappush(heap, (0, 0, 0, "R"))
    heapq.heappush(heap, (0, 0, 0, "D"))
    while len(heap) > 0:
        cost, row, col, prev = heapq.heappop(heap)

        if len(prev) > 3 or (row, col, prev) in seen:
            continue

        seen.add((row, col, prev))

        if row == rows - 1 and col == cols - 1:
            return cost

        # Up
        if row > 0 and prev[-1] != "D":
            heapq.heappush(heap, (cost + grid[row - 1][col], row - 1, col, get_prev(prev, "U")))

        # Right
        if col < cols - 1 and prev[-1] != "L":
            heapq.heappush(heap, (cost + grid[row][col + 1], row, col + 1, get_prev(prev, "R")))

        # Down
        if row < rows - 1 and prev[-1] != "U":
            heapq.heappush(heap, (cost + grid[row + 1][col], row + 1, col, get_prev(prev, "D")))

        # Left
        if col > 0 and prev[-1] != "R":
            heapq.heappush(heap, (cost + grid[row][col - 1], row, col - 1, get_prev(prev, "L")))

    raise Exception("No path found")


def get_prev(prev, direction):
    prev += direction

    if len(set(prev)) > 1:
        return prev[-1]

    return prev


def part2():
    pass


def test_find_shortest_path():
    grid1 = """
111
111
"""

    grid2 = """
122
111
"""

    grid3 = """
11111
99991
"""

    tests = [
        (3, grid1),
        (3, grid2),
        (13, grid3)
    ]

    for expected, grid in tests:
        parsed_grid = [[int(value) for value in list(line.strip())] for line in grid.split()]
        actual = find_shortest_path(parsed_grid)

        if actual != expected:
            assert False, "Actual={}, Expected={}, Grid={}".format(actual, expected, grid.replace("\n", "  "))


def main():
    part1()
    part2()
    # test_find_shortest_path()


if __name__ == "__main__":
    main()
