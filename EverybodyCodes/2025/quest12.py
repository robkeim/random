def part1():
    grid = [line.strip() for line in open("quest12_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    seen = set()
    to_process = [(0, 0)]

    while to_process:
        r, c = to_process.pop()

        if r < 0 or c < 0 or r >= num_r or c >= num_c or (r, c) in seen:
            continue

        seen.add((r, c))

        for dr, dc in ((-1, 0), (1, 0), (0, -1), (0, 1)):
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in seen and grid[next_r][next_c] <= grid[r][c]:
                to_process.append((next_r, next_c))

    print(len(seen))


def part2():
    grid = [line.strip() for line in open("quest12_p2.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    seen = set()
    to_process = [(0, 0), (num_r - 1, num_c - 1)]

    while to_process:
        r, c = to_process.pop()

        if r < 0 or c < 0 or r >= num_r or c >= num_c or (r, c) in seen:
            continue

        seen.add((r, c))

        for dr, dc in ((-1, 0), (1, 0), (0, -1), (0, 1)):
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c and (next_r, next_c) not in seen and grid[next_r][next_c] <= \
                    grid[r][c]:
                to_process.append((next_r, next_c))

    print(len(seen))


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
