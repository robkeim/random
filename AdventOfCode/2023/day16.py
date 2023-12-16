up = 0
right = 1
down = 2
left = 3


def part1():
    grid = [line.strip() for line in open("day16.txt").readlines()]
    print(run_simulation(grid, (0, 0, right)))


def run_simulation(grid, start):
    rows = len(grid)
    cols = len(grid[0])

    seen = set()
    positions = set()
    to_process = [start]

    while len(to_process) > 0:
        row, col, direction = to_process.pop()

        if (row, col, direction) in seen or row < 0 or col < 0 or row >= rows or col >= cols:
            continue

        seen.add((row, col, direction))
        positions.add((row, col))

        cur_square = grid[row][col]

        if cur_square == ".":
            pass  # No direction change
        elif cur_square == "/":
            if direction == up:
                direction = right
            elif direction == right:
                direction = up
            elif direction == down:
                direction = left
            elif direction == left:
                direction = down
            else:
                assert False, "Invalid direction: {}".format(direction)
        elif cur_square == "\\":
            if direction == up:
                direction = left
            elif direction == right:
                direction = down
            elif direction == down:
                direction = right
            elif direction == left:
                direction = up
            else:
                assert False, "Invalid direction: {}".format(direction)
        elif cur_square == "|":
            if direction == up or direction == down:
                pass  # The splitter has no effect
            elif direction == right or direction == left:
                to_process.append((row - 1, col, up))
                to_process.append((row + 1, col, down))
                continue
            else:
                assert False, "Invalid direction: {}".format(direction)
        elif cur_square == "-":
            if direction == up or direction == down:
                to_process.append((row, col - 1, left))
                to_process.append((row, col + 1, right))
                continue
            elif direction == right or direction == left:
                pass  # The splitter has no effect
            else:
                assert False, "Invalid direction: {}".format(direction)
        else:
            assert False, "Invalid square: {}".format(cur_square)

        if direction == up:
            to_process.append((row - 1, col, direction))
        elif direction == right:
            to_process.append((row, col + 1, direction))
        elif direction == down:
            to_process.append((row + 1, col, direction))
        elif direction == left:
            to_process.append((row, col - 1, direction))
        else:
            assert False, "Invalid direction: {}".format(direction)

    return len(positions)


def part2():
    grid = [line.strip() for line in open("day16.txt").readlines()]
    rows = len(grid)
    cols = len(grid[0])

    result = 0

    for row in range(rows):
        result = max(result, run_simulation(grid, (row, 0, right)))
        result = max(result, run_simulation(grid, (row, cols - 1, left)))

    for col in range(cols):
        result = max(result, run_simulation(grid, (0, col, down)))
        result = max(result, run_simulation(grid, (rows - 1, col, up)))

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
