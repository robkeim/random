def part1():
    lines = [list(line.strip()) for line in open("quest19_p1.txt").readlines()]
    directions = lines[0]
    grid = lines[2:]

    count = 0

    for r in range(1, len(grid) - 1):
        for c in range(1, len(grid[0]) - 1):
            grid = rotate(grid, r, c, directions[count % len(directions)])
            count += 1

    print_grid(grid)

def rotate(grid, r, c, direction):
    neighbors = [(-1, -1), (-1, 0), (-1, 1), (0, 1), (1, 1), (1, 0), (1, -1), (0, -1)]
    old = [grid[r + dr][c + dc] for (dr, dc) in neighbors]

    if direction == "R":
        old = [old[-1]] + old[:-1]
    elif direction == "L":
        old = old[1:] + [old[0]]
    else:
        assert False, f"Unsupported direction {direction}"

    for i, (dr, dc) in enumerate(neighbors):
        grid[r + dr][c + dc] = old[i]

    return grid


def print_grid(grid):
    for row in grid:
        print("".join(row))


def part2():
    lines = [list(line.strip()) for line in open("quest19_p2.txt").readlines()]
    directions = lines[0]
    grid = lines[2:]

    for _ in range(100):
        count = 0

        for r in range(1, len(grid) - 1):
            for c in range(1, len(grid[0]) - 1):
                grid = rotate(grid, r, c, directions[count % len(directions)])
                count += 1

    print_grid(grid)


def part3():
    lines = [list(line.strip()) for line in open("quest19_p3.txt").readlines()]
    directions = lines[0]
    grid = lines[2:]
    num_r = len(grid)
    num_c = len(grid[0])
    coordinates_grid = [[(r, c) for c in range(num_c)] for r in range(num_r)]
    count = 0

    for r in range(1, num_r - 1):
        for c in range(1, num_c - 1):
            coordinates_grid = rotate(coordinates_grid, r, c, directions[count % len(directions)])
            count += 1

    permutation = dict()

    for r in range(num_r):
        for c in range(num_c):
            permutation[(r, c)] = coordinates_grid[r][c]

    num_iterations = 1048576000
    permutation = exp_by_squaring(permutation, num_iterations)

    final_grid = [["." for c in range(num_c)] for r in range(num_r)]

    for start_r, start_c in permutation:
        dest_r, dest_c = permutation[(start_r, start_c)]

        final_grid[start_r][start_c] = grid[dest_r][dest_c]

    print_grid(final_grid)


# https://en.wikipedia.org/wiki/Exponentiation_by_squaring
def exp_by_squaring(x, n):
   if n == 1:
      return x
   elif n % 2 == 0:
       return exp_by_squaring(multiply(x, x), n // 2)
   else:
        return multiply(x, exp_by_squaring(multiply(x, x), (n - 1) // 2))


def multiply(x1, x2):
    result = dict()

    for key in x1.keys():
        result[key] = x1[x2[key]]

    return result


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
