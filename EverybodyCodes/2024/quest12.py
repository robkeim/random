def part1():
    grid = [line.strip() for line in open("quest12_p1.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])

    projectiles = []
    targets = []

    for r in range(num_r):
        for c in range(num_c):
            value = grid[r][c]

            if value in "ABC":
                projectiles.append((ord(value) - ord("A") + 1, num_r - r - 1, c))

            if value == "T":
                targets.append((num_r - r - 1, c))

    answer = 0

    for target_r, target_c in targets:
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
                    answer += segment_number * power
                    break

                power += 1

            if found:
                break

    print(answer)

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
