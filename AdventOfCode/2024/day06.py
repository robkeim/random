def part1():
    grid = [line.strip() for line in open("day06.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    r = None
    c = None
    deltas = [(-1, 0), (0, 1), (1, 0), (0, -1)]  # Up, Right, Down, Left
    cur_dir = 0
    seen = set()

    for i in range(num_r):
        for j in range(num_c):
            if grid[i][j] == "^":
                r = i
                c = j
                break

    while True:
        seen.add((r, c))

        next_r = r + deltas[cur_dir][0]
        next_c = c + deltas[cur_dir][1]

        if next_r < 0 or next_r >= num_r or next_c < 0 or next_c >= num_c:
            break

        if grid[next_r][next_c] == "#":
            cur_dir = (cur_dir + 1) % 4
        else:
            r = next_r
            c = next_c

    print(len(seen))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
