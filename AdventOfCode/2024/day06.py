def part1():
    grid = [line.strip() for line in open("day06.txt").readlines()]

    num_r = len(grid)
    num_c = len(grid[0])
    obstacles = get_obstacles(grid)
    r, c = get_starting_position(grid)

    print(len(get_path(obstacles, num_r, num_c, r, c, 0)))


def part2():
    grid = [line.strip() for line in open("day06.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    r, c = get_starting_position(grid)
    obstacles = get_obstacles(grid)
    result = 0

    for obstacle_r, obstacle_c in get_path(obstacles, num_r, num_c, r, c, 0):
        new_obstacles = set(obstacles)
        new_obstacles.add((obstacle_r, obstacle_c))

        if has_loop(new_obstacles, num_r, num_c, r, c, 0):
            result += 1

    print(result)


def get_starting_position(grid):
    for r in range(len(grid)):
        for c in range(len(grid[0])):
            if grid[r][c] == "^":
                return r, c

    raise Exception("No starting position found!")


def get_obstacles(grid):
    obstacles = set()

    for r in range(len(grid)):
        for c in range(len(grid[0])):
            if grid[r][c] == "#":
                obstacles.add((r, c))

    return obstacles


def get_path(obstacles, num_r, num_c, r, c, cur_dir):
    deltas = [(-1, 0), (0, 1), (1, 0), (0, -1)]  # Up, Right, Down, Left
    seen = set()

    while True:
        seen.add((r, c))

        next_r = r + deltas[cur_dir][0]
        next_c = c + deltas[cur_dir][1]

        if next_r < 0 or next_r >= num_r or next_c < 0 or next_c >= num_c:
            break

        if (next_r, next_c) in obstacles:
            cur_dir = (cur_dir + 1) % 4
        else:
            r = next_r
            c = next_c

    return seen


def has_loop(obstacles, num_r, num_c, r, c, cur_dir):
    deltas = [(-1, 0), (0, 1), (1, 0), (0, -1)]  # Up, Right, Down, Left
    seen = set()

    while True:
        if (r, c, cur_dir) in seen:
            return True

        seen.add((r, c, cur_dir))

        next_r = r + deltas[cur_dir][0]
        next_c = c + deltas[cur_dir][1]

        if next_r < 0 or next_r >= num_r or next_c < 0 or next_c >= num_c:
            return False

        if (next_r, next_c) in obstacles:
            cur_dir = (cur_dir + 1) % 4
        else:
            r = next_r
            c = next_c


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
