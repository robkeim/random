def part1():
    grid = [line.strip() for line in open("quest12_p1.txt").readlines()]
    solve_parts_1_and_2(grid)

def part2():
    grid = [line.strip() for line in open("quest12_p2.txt").readlines()]
    solve_parts_1_and_2(grid)


def solve_parts_1_and_2(grid):
    num_r = len(grid)
    num_c = len(grid[0])

    projectiles = []
    targets = []

    for r in range(num_r):
        for c in range(num_c):
            value = grid[r][c]

            if value in "ABC":
                projectiles.append((ord(value) - ord("A") + 1, num_r - r - 1, c))

            if value in "HT":
                targets.append((num_r - r - 1, c, value))

    answer = 0

    for target_r, target_c, block_type in targets:
        for segment_number, start_r, start_c in projectiles:
            found = False
            power = 1

            while True:
                r = start_r + power
                c = start_c + 2 * power

                if c >= target_c:
                    break

                if target_c - c == r - target_r:
                    found = True
                    multiplier = 2 if block_type == "H" else 1
                    answer += segment_number * power * multiplier
                    break

                power += 1

            if found:
                break

    print(answer)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
