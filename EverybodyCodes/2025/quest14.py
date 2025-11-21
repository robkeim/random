def part1():
    grid = [line.strip() for line in open("quest14_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    active = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "#":
                active.add((r, c))

    result = 0

    for _ in range(10):
        next_active = set()

        for r in range(num_r):
            for c in range(num_c):
                num_active_neighbors = 0
                for dr, dc in [(-1, -1), (-1, 1), (1, 1), (1, -1)]:
                    next_r = r + dr
                    next_c = c + dc

                    if (next_r, next_c) in active:
                        num_active_neighbors += 1

                if ((r, c) in active and num_active_neighbors % 2 == 1) or ((r, c) not in active and num_active_neighbors % 2 == 0):
                    next_active.add((r, c))

        result += len(next_active)
        active = next_active

    print(result)


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
