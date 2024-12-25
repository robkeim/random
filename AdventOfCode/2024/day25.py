def part1():
    grids = open("day25.txt").read().split("\n\n")
    tops = []
    bottoms = []

    for grid in grids:
        grid = [line.strip() for line in grid.split("\n")]
        num_r = len(grid)
        num_c = len(grid[0])

        if grid[0][0] == "#":
            top = []

            for c in range(num_c):
                index = -1

                while index + 1 < num_r and grid[index + 1][c] == "#":
                    index += 1

                top.append(index)

            tops.append(top)
        elif grid[-1][0] == "#":
            bottom = []

            for c in range(num_c):
                index = num_r - 1

                while index - 1 >= 0 and grid[index - 1][c] == "#":
                    index -= 1

                bottom.append(num_r - index - 1)

            bottoms.append(bottom)
        else:
            assert False, "Invalid grid"

    answer = 0

    for top in tops:
        for bottom in bottoms:
            is_valid = True

            for c in range(5):
                if top[c] + bottom[c] > 5:
                    is_valid = False

            if is_valid:
                answer += 1

    print(answer)


def print_grid(grid):
    for row in grid:
        print("".join(row))

    print()


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
