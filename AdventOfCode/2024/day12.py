def part1():
    grid = [line.strip() for line in open("day12.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    seen = set()
    deltas = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    result = 0

    for start_r in range(num_r):
        for start_c in range(num_c):
            if (start_r, start_c) in seen:
                continue

            area = 0
            perimeter = 0
            cur_val = grid[start_r][start_c]
            to_process = [(start_r, start_c)]
            seen.add((start_r, start_c))

            while len(to_process) > 0:
                r, c = to_process.pop()
                area += 1

                for dr, dc in deltas:
                    next_r = r + dr
                    next_c = c + dc

                    if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] == cur_val:
                        if (next_r, next_c) not in seen:
                            to_process.append((next_r, next_c))
                            seen.add((next_r, next_c))
                    else:
                        perimeter += 1

            result += area * perimeter

    print(result)


def part2():
    grid = [line.strip() for line in open("day12.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    seen = set()
    deltas = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    result = 0

    for start_r in range(num_r):
        for start_c in range(num_c):
            if (start_r, start_c) in seen:
                continue

            area = 0
            cur_val = grid[start_r][start_c]
            to_process = [(start_r, start_c)]
            seen.add((start_r, start_c))
            cur_region = {(start_r, start_c)}

            while len(to_process) > 0:
                r, c = to_process.pop()
                area += 1

                for dr, dc in deltas:
                    next_r = r + dr
                    next_c = c + dc

                    if 0 <= next_r < num_r and 0 <= next_c < num_c and grid[next_r][next_c] == cur_val:
                        if (next_r, next_c) not in seen:
                            to_process.append((next_r, next_c))
                            seen.add((next_r, next_c))
                            cur_region.add((next_r, next_c))

            result += area * num_sides(cur_region)

    print(result)


def num_sides(plants):
    sides = set()

    for (r, c) in plants:
        # Nothing above
        if (r - 1, c) not in plants:
            min_c = c

            while (r, min_c - 1) in plants and (r - 1, min_c - 1) not in plants:
                min_c -= 1

            max_c = c

            while (r, max_c + 1) in plants and (r - 1, max_c + 1) not in plants:
                max_c += 1

            sides.add((r, r, min_c, max_c, "top"))

        # Nothing to the right
        if (r, c + 1) not in plants:
            min_r = r

            while (min_r - 1, c) in plants and (min_r - 1, c + 1) not in plants:
                min_r -= 1

            max_r = r

            while (max_r + 1, c) in plants and (max_r + 1, c + 1) not in plants:
                max_r += 1

            sides.add((min_r, max_r, c, c, "right"))

        # Nothing below
        if (r + 1, c) not in plants:
            min_c = c

            while (r, min_c - 1) in plants and (r + 1, min_c - 1) not in plants:
                min_c -= 1

            max_c = c

            while (r, max_c + 1) in plants and (r + 1, max_c + 1) not in plants:
                max_c += 1

            sides.add((r, r, min_c, max_c, "bottom"))

        # Nothing to the left
        if (r, c - 1) not in plants:
            min_r = r

            while (min_r - 1, c) in plants and (min_r - 1, c - 1) not in plants:
                min_r -= 1

            max_r = r

            while (max_r + 1, c) in plants and (max_r + 1, c - 1) not in plants:
                max_r += 1

            sides.add((min_r, max_r, c, c, "left"))

    return len(sides)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
