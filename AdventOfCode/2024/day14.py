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
                row += "X"
            else:
                row += "."

        print(row)

def part2():
    lines = [line.strip() for line in open("day14.txt").readlines()]
    num_r = 103
    num_c = 101
    robots = []

    for line in lines:
        p, v = line.split()
        p_x, p_y = [int(value) for value in p[2:].split(",")]
        v_x, v_y = [int(value) for value in v[2:].split(",")]

        robots.append((p_x, p_y, v_x, v_y))

    num_seconds = 0

    while True:
        if num_seconds % 1000 == 0:
            print("Num seconds:", num_seconds)

        robot_positions = set()
        robot_rows = defaultdict(list)

        for p_x, p_y, v_x, v_y in robots:
            x = (p_x + (v_x + num_c) * num_seconds) % num_c
            y = (p_y + (v_y + num_r) * num_seconds) % num_r

            robot_positions.add((x, y))
            robot_rows[x].append(y)

        possible_answer = False
        for row in robot_rows:
            if possible_answer:
                break

            cols = sorted(robot_rows[row])

            if cols == [0, 8, 12, 15, 16, 30, 31, 55, 58, 59, 77, 82, 88]:
                pass

            max_continuous = 0
            i = 0
            j = 0

            while i < len(cols):
                while j + 1 < len(cols) and cols[j + 1] == cols[j] + 1:
                    j += 1

                max_continuous = max(max_continuous, j - i)
                i = j + 1
                j = i

            max_continuous = max(max_continuous, j - i)

            if max_continuous >= 10:
                possible_answer = True
                print("Potential answer after", num_seconds, "seconds")
                print_grid(num_r, num_c, robot_positions)

        num_seconds += 1


def main():
    part1()
    # Commenting out part 2 because it requires manually looking at the output while the algorithm runs forever
    # part2()


if __name__ == "__main__":
    main()
