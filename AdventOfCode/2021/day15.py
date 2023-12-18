import heapq


def part1():
    lines = [line.strip() for line in open("day15.txt").readlines()]
    length = len(lines)

    grid = dict()

    for y in range(length):
        for x in range(length):
            grid[(x, y)] = int(lines[y][x])

    for i in range(1, length):
        grid[(0, i)] += grid[(0, i - 1)]
        grid[(i, 0)] += grid[(i - 1, 0)]

    for y in range(1, length):
        for x in range(1, length):
            grid[(x, y)] += min(grid[(x - 1, y)], grid[(x, y - 1)])

    print(grid[(length - 1, length - 1)] - grid[(0, 0)])


def print_grid(grid, length, with_spaces):
    for y in range(length):
        line = ""

        for x in range(length):
            line += str(grid[(x, y)])

            if with_spaces:
                line += " "

        print(line)


def part2():
    lines = [line.strip() for line in open("day15.txt").readlines()]
    length = len(lines)

    grid = dict()

    for y in range(length):
        for x in range(length):
            grid[(x, y)] = int(lines[y][x])

    # Expand grid
    for offset in range(1, 5):
        for y in range(length):
            for x in range(length):
                next_x = offset * length + x

                next_value = grid[(x, y)] + offset
                if next_value > 9:
                    next_value -= 9

                grid[(next_x, y)] = next_value

    for offset in range(1, 5):
        for y in range(length):
            for x in range(5 * length):
                next_y = offset * length + y

                next_value = grid[(x, y)] + offset
                if next_value > 9:
                    next_value -= 9

                grid[(x, next_y)] = next_value

    length *= 5

    # Calculate path
    rows = length
    cols = length
    seen = set()
    heap = []
    heapq.heappush(heap, (0, 0, 0))
    while len(heap) > 0:
        cost, row, col = heapq.heappop(heap)

        if (row, col) in seen:
            continue

        seen.add((row, col))

        if row == rows - 1 and col == cols - 1:
            print(cost)
            break

        # Up
        if row > 0:
            heapq.heappush(heap, (cost + grid[(row - 1, col)], row - 1, col))

        # Right
        if col < cols - 1:
            heapq.heappush(heap, (cost + grid[(row, col + 1)], row, col + 1))

        # Down
        if row < rows - 1:
            heapq.heappush(heap, (cost + grid[(row + 1, col)], row + 1, col))

        # Left
        if col > 0:
            heapq.heappush(heap, (cost + grid[(row, col - 1)], row, col - 1))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
