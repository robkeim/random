def part1():
    grid = [line.strip() for line in open("day08.txt").readlines()]

    height = len(grid)
    width = len(grid[0])

    visible = set()

    for y in range(height):
        visible.add((0, y))
        max_tree = grid[y][0]
        for x in range(width):
            if grid[y][x] > max_tree:
                visible.add((x, y))
                max_tree = grid[y][x]

    for y in range(height - 1, -1, -1):
        visible.add((width - 1, y))
        max_tree = grid[y][width - 1]
        for x in range(width - 1, -1, -1):
            if grid[y][x] > max_tree:
                visible.add((x, y))
                max_tree = grid[y][x]

    for x in range(width):
        visible.add((x, 0))
        max_tree = grid[0][x]
        for y in range(height):
            if grid[y][x] > max_tree:
                visible.add((x, y))
                max_tree = grid[y][x]

    for x in range(width - 1, -1, -1):
        visible.add((x, height - 1))
        max_tree = grid[height - 1][x]
        for y in range(height - 1, -1, -1):
            if grid[y][x] > max_tree:
                visible.add((x, y))
                max_tree = grid[y][x]

    print(len(visible))


def part2():
    grid = [line.strip() for line in open("day08.txt").readlines()]

    height = len(grid)
    width = len(grid[0])

    result = 0

    for y in range(1, height - 1):
        for x in range(1, width - 1):
            score = 1
            tree_height = grid[y][x]

            delta_x = 1

            while x + delta_x < width - 1 and grid[y][x + delta_x] < tree_height:
                delta_x += 1

            score *= delta_x

            delta_x = 1

            while x - delta_x > 0 and grid[y][x - delta_x] < tree_height:
                delta_x += 1

            score *= delta_x

            delta_y = 1

            while y + delta_y < height - 1 and grid[y + delta_y][x] < tree_height:
                delta_y += 1

            score *= delta_y

            delta_y = 1

            while y - delta_y > 0 and grid[y - delta_y][x] < tree_height:
                delta_y += 1

            score *= delta_y

            result = max(result, score)

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
