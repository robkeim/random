from collections import defaultdict


def part1():
    grid = [line.strip() for line in open("quest10_p1.txt").readlines()]
    sheep = set()
    num_r = len(grid)
    num_c = len(grid[0])

    for r in range(len(grid)):
        for c in range(len(grid[0])):
            if grid[r][c] == "S":
                sheep.add((r, c))
            elif grid[r][c] == "D":
                start_r, start_c = r, c

    assert start_r is not None and start_c is not None, "Dragon not found"

    seen = defaultdict(int)
    to_process = [(start_r, start_c, 4)]

    while to_process:
        r, c, num_steps = to_process.pop()

        if seen[(r, c)] >= num_steps:
            continue

        for dr, dc in [(-1, -2), (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2)]:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c:
                to_process.append((next_r, next_c, num_steps - 1))

    print(len(seen.keys() & sheep))


def part2():
    grid = [line.strip() for line in open("quest10_p2.txt").readlines()]
    num_r = len(grid)
    num_c = len(grid[0])
    dragon_positions = None
    sheep = set()
    hideouts = set()

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "D":
                dragon_positions = {(r, c)}
            elif grid[r][c] == "S":
                sheep.add((r, c))
            elif grid[r][c] == "#":
                hideouts.add((r, c))
            elif grid[r][c] == ".":
                pass  # Nothing to do
            else:
                assert False, f"Invalid character: {grid[r][c]}"

    sheep_eaten = 0

    for turn in range(20):
        dragon_positions = get_next_dragon_positions(dragon_positions, num_r, num_c)

        next_sheep = set()

        for r, c in sheep:
            if ((r + turn, c) in dragon_positions and (r + turn, c) not in hideouts) or ((r + turn + 1, c) in dragon_positions and (r + turn + 1, c) not in hideouts):
                sheep_eaten += 1
            else:
                next_sheep.add((r, c))

        sheep = next_sheep

    print(sheep_eaten)


def get_next_dragon_positions(positions, num_r, num_c):
    next_positions = set()

    for r, c in positions:
        for dr, dc in [(-1, -2), (-2, -1), (-2, 1), (-1, 2), (1, 2), (2, 1), (2, -1), (1, -2)]:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r < num_r and 0 <= next_c < num_c:
                next_positions.add((next_r, next_c))

    return next_positions


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
