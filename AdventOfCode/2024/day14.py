from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day14.txt").readlines()]
    num_seconds = 100
    num_r = 103
    num_c = 101
    num_robots = defaultdict(int)
    robot_positions = defaultdict(int)

    for line in lines:
        p, v = line.split()
        p_x, p_y = [int(value) for value in p[2:].split(",")]
        v_x, v_y = [int(value) for value in v[2:].split(",")]

        x = (p_x + (v_x + num_c) * num_seconds) % num_c
        y = (p_y + (v_y + num_r) * num_seconds) % num_r

        assert x >= 0 and y >= 0

        robot_positions[(x, y)] += 1

        if (x == num_c // 2) or (y == num_r // 2):
            continue

        top = y < num_r // 2
        left = x < num_c // 2

        num_robots[(top, left)] += 1

    result = 1

    for value in num_robots.values():
        result *= value

    print(result)


def print_grid(num_r, num_c, robot_positions):
    for r in range(num_r):
        row = ""

        for c in range(num_c):
            if (r, c) in robot_positions:
                row += str(robot_positions[(r, c)])
            else:
                row += "."

        print(row)

def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
