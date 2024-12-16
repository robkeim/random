def part1():
    grid, instructions = open("day15.txt").read().split("\n\n")
    grid = [list(line.strip()) for line in grid.split()]
    num_r = len(grid)
    num_c = len(grid[0])
    instructions = instructions.replace("\n", "")

    dirs = {
        "^": (-1, 0),
        ">": (0, 1),
        "v": (1, 0),
        "<": (0, -1)
    }

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "@":
                cur_r = r
                cur_c = c
                grid[r][c] = "."

    assert cur_r and cur_c

    for instruction in instructions:
        dr, dc = dirs[instruction]
        next_r = cur_r + dr
        next_c = cur_c + dc
        next_val = grid[next_r][next_c]

        if next_val == ".":
            cur_r = next_r
            cur_c = next_c
        elif next_val == "#":
            # Not able to move, so skip the move
            pass
        elif next_val == "O":
            length = 1

            while grid[cur_r + dr * length][cur_c + dc * length] == "O":
                length += 1

            final_r = cur_r + dr * length
            final_c = cur_c + dc * length

            if grid[final_r][final_c] == ".":
                grid[final_r][final_c] = "O"
                grid[next_r][next_c] = "."
                cur_r = next_r
                cur_c = next_c
            elif grid[final_r][final_c] == "#":
                # Can't move the stones because behind them is a wall
                pass
            else:
                assert False, f"Unexpected type: {grid[final_r][final_c]}"
        else:
            assert False, f"Invalid grid item: {next_val}"

    gps_sum = 0

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "O":
                gps_sum += 100 * r + c

    print(gps_sum)


def print_grid(grid, cur_r, cur_c):
    for r in range(len(grid)):
        row = ""
        for c in range(len(grid[0])):
            if r == cur_r and c == cur_c:
                row += "@"
            else:
               row += grid[r][c]

        print(row)


def part2():
    grid, instructions = open("day15.txt").read().split("\n\n")
    grid = expand_grid(grid.split())
    num_r = len(grid)
    num_c = len(grid[0])
    instructions = instructions.replace("\n", "")

    dirs = {
        "^": (-1, 0),
        ">": (0, 1),
        "v": (1, 0),
        "<": (0, -1)
    }

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "@":
                cur_r = r
                cur_c = c
                grid[r][c] = "."

    assert cur_r and cur_c

    for instruction in instructions:
        dr, dc = dirs[instruction]
        next_r = cur_r + dr
        next_c = cur_c + dc
        next_val = grid[next_r][next_c]

        if next_val == ".":
            cur_r = next_r
            cur_c = next_c
        elif next_val == "#":
            # Not able to move, so skip the move
            pass
        elif next_val in "[]":
            if instruction in "<>":
                length = 1

                while grid[cur_r + dr * length][cur_c + dc * length] in "[]":
                    length += 1

                final_r = cur_r + dr * length
                final_c = cur_c + dc * length

                if grid[final_r][final_c] == ".":
                    grid[final_r][final_c] = "["

                    while length > 0:
                        rr = cur_r + dr * length
                        cc = cur_c + dc * length

                        grid[rr][cc] = grid[rr - dr][cc - dc]
                        length -= 1

                    grid[next_r][next_c] = "."
                    cur_r = next_r
                    cur_c = next_c
                elif grid[final_r][final_c] == "#":
                    # Can't move the stones because behind them is a wall
                    pass
                else:
                    assert False, f"Unexpected type: {grid[final_r][final_c]}"
            else:
                cur_level_boxes = set()
                cur_level_boxes.add(cur_c)

                if grid[next_r][cur_c] == "[":
                    cur_level_boxes.add(cur_c + 1)
                else:
                    cur_level_boxes.add(cur_c - 1)

                length = 1
                hit_wall = False
                to_replace = set()
                replacements = dict()

                while not hit_wall:
                    next_level_boxes = set()
                    next_row = cur_r + dr * length
                    has_box = False

                    for c in cur_level_boxes:
                        if grid[next_row][c] == "#":
                            hit_wall = True
                        elif grid[next_row][c] in "[]":
                            next_level_boxes.add(c)

                            if grid[next_row][c] == "[":
                                next_level_boxes.add(c + 1)
                            else:
                                next_level_boxes.add(c - 1)
                            has_box = True

                    if hit_wall or not has_box:
                        break

                    for c in next_level_boxes:
                        to_replace.add((next_row, c))
                        replacements[(next_row, c)] = grid[next_row][c]

                    cur_level_boxes = next_level_boxes
                    length += 1

                length -= 1

                if hit_wall:
                    continue

                for (r, c) in to_replace:
                    grid[r][c] = "."

                for ((r, c), value) in replacements.items():
                    grid[r + dr][c + dc] = value

                cur_r = next_r
                cur_c = next_c
        else:
            assert False, f"Invalid grid item: {next_val}"

    gps_sum = 0

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "[":
                gps_sum += 100 * r + c

    print(gps_sum)

def expand_grid(grid):
    expand = {
        "#": "##",
        "O": "[]",
        ".": "..",
        "@": "@."
    }

    result = []
    for row in grid:
        result.append(list("".join([expand[value] for value in row])))

    return result


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
