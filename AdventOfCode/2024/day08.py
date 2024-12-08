def part1():
    grid = [line.strip() for line in open("day08.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    antennas = []

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] != ".":
                antennas.append((grid[r][c], r, c))

    antinodes = set()

    for i in range(len(antennas)):
        for j in range(i + 1, len(antennas)):
            f1, r1, c1 = antennas[i]
            f2, r2, c2 = antennas[j]

            if f1 != f2:
                continue

            dr = r1 - r2
            dc = c1 - c2

            new_antinodes = [(r1 + dr, c1 + dc), (r2 - dr, c2 - dc)]

            for r, c in new_antinodes:
                if 0 <= r < num_r and 0 <= c < num_c:
                    antinodes.add((r, c))

    print(len(antinodes))


def print_grid(grid, antinodes):
    print("Grid:")
    for r in range(len(grid)):
        row = ""
        for c in range(len(grid[0])):
            if (r, c) in antinodes:
                row += "#"
            else:
                row += grid[r][c]

        print(row)


def part2():
    grid = [line.strip() for line in open("day08.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    antennas = []

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] != ".":
                antennas.append((grid[r][c], r, c))

    antinodes = set()

    for i in range(len(antennas)):
        for j in range(i + 1, len(antennas)):
            f1, r1, c1 = antennas[i]
            f2, r2, c2 = antennas[j]

            if f1 != f2:
                continue

            dr = r1 - r2
            dc = c1 - c2

            directions = [(r1, dr, c1, dc), (r2, -dr, c2, -dc)]

            for r, dr, c, dc in directions:
                multiplier = 0

                while True:
                    next_r = r + dr * multiplier
                    next_c = c + dc * multiplier

                    if next_r < 0 or next_r >= num_r or next_c < 0 or next_c >= num_c:
                        break

                    antinodes.add((next_r, next_c))

                    multiplier += 1

    print(len(antinodes))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
