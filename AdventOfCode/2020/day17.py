def part1():
    grid = [line.strip() for line in open("day17.txt").readlines()]

    min_x = min_y = float("inf")
    max_x = max_y = float("-inf")
    min_z = max_z = 0
    state = set()

    for x in range(len(grid)):
        for y in range(len(grid[0])):
            if grid[x][y] == "#":
                state.add((x, y, 0))
                min_x = min(min_x, x)
                min_y = min(min_y, y)
                max_x = max(max_x, x)
                max_y = max(max_y, y)

    for cycle in range(6):
        min_x_next = min_y_next = min_z_next = float("inf")
        max_x_next = max_y_next = max_z_next = float("-inf")
        state_next = set()

        for x in range(min_x - 1, max_x + 2):
            for y in range(min_y - 1, max_y + 2):
                for z in range(min_z - 1, max_z + 2):
                    neighbors = 0

                    for dx in range(-1, 2):
                        for dy in range(-1, 2):
                            for dz in range(-1, 2):
                                if dx == 0 and dy == 0 and dz == 0:
                                    continue

                                if (x + dx, y + dy, z + dz) in state:
                                    neighbors += 1

                    if ((x, y, z) in state and 2 <= neighbors <= 3) or ((x, y, z) not in state and neighbors == 3):
                        state_next.add((x, y, z))
                        min_x_next = min(min_x_next, x)
                        min_y_next = min(min_y_next, y)
                        min_z_next = min(min_z_next, z)
                        max_x_next = max(max_x_next, x)
                        max_y_next = max(max_y_next, y)
                        max_z_next = max(max_z_next, z)

        state = state_next
        min_x = min_x_next
        min_y = min_y_next
        min_z = min_z_next
        max_x = max_x_next
        max_y = max_y_next
        max_z = max_z_next

    print(len(state))


def part2():
    grid = [line.strip() for line in open("day17.txt").readlines()]

    min_x = min_y = float("inf")
    max_x = max_y = float("-inf")
    min_z = max_z = min_w = max_w = 0
    state = set()

    for x in range(len(grid)):
        for y in range(len(grid[0])):
            if grid[x][y] == "#":
                state.add((x, y, 0, 0))
                min_x = min(min_x, x)
                min_y = min(min_y, y)
                max_x = max(max_x, x)
                max_y = max(max_y, y)

    for cycle in range(6):
        min_x_next = min_y_next = min_z_next = min_w_next = float("inf")
        max_x_next = max_y_next = max_z_next = max_w_next = float("-inf")
        state_next = set()

        for x in range(min_x - 1, max_x + 2):
            for y in range(min_y - 1, max_y + 2):
                for z in range(min_z - 1, max_z + 2):
                    for w in range(min_w - 1, max_w + 2):
                        neighbors = 0

                        for dx in range(-1, 2):
                            for dy in range(-1, 2):
                                for dz in range(-1, 2):
                                    for dw in range(-1, 2):
                                        if dx == 0 and dy == 0 and dz == 0 and dw == 0:
                                            continue

                                        if (x + dx, y + dy, z + dz, w + dw) in state:
                                            neighbors += 1

                        if ((x, y, z, w) in state and 2 <= neighbors <= 3) or ((x, y, z, w) not in state and neighbors == 3):
                            state_next.add((x, y, z, w))
                            min_x_next = min(min_x_next, x)
                            min_y_next = min(min_y_next, y)
                            min_z_next = min(min_z_next, z)
                            min_w_next = min(min_w_next, w)
                            max_x_next = max(max_x_next, x)
                            max_y_next = max(max_y_next, y)
                            max_z_next = max(max_z_next, z)
                            max_w_next = max(max_w_next, w)

        state = state_next
        min_x = min_x_next
        min_y = min_y_next
        min_z = min_z_next
        min_w = min_w_next
        max_x = max_x_next
        max_y = max_y_next
        max_z = max_z_next
        max_w = max_w_next

    print(len(state))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
