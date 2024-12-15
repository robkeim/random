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
        #print_grid(grid, cur_r, cur_c)
        #print(f"\nMove {instruction}:")
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
            pass
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
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
