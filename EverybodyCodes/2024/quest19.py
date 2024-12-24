def part1():
    lines = [list(line.strip()) for line in open("quest19_p1.txt").readlines()]
    directions = lines[0]
    grid = lines[2:]

    count = 0

    for r in range(1, len(grid) - 1):
        for c in range(1, len(grid[0]) - 1):
            grid = rotate(grid, r, c, directions[count % len(directions)])
            count += 1

    print_grid(grid)

def rotate(grid, r, c, direction):
    neighbors = [(-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1)]
    old = [grid[r + dr][c + dc] for (dr, dc) in neighbors]

    if direction == "R":
        old = [old[-1]] + old[:-1]
    elif direction == "L":
        old = old[1:] + [old[0]]
    else:
        assert False, f"Unsupported direction {direction}"

    for i, (dr, dc) in enumerate(neighbors):
        grid[r + dr][c + dc] = old[i]

    return grid


def print_grid(grid):
    for row in grid:
        print("".join(row))


def part2():
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
