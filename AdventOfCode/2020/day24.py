import re


def part1():
    lines = [line.strip() for line in open("day24.txt").readlines()]

    black_tiles = set()

    for directions in lines:
        position = (0, 0, 0)

        for direction in re.findall("nw|ne|e|se|sw|w", directions):
            position = get_next_position(position, direction)

        if position in black_tiles:
            black_tiles.remove(position)
        else:
            black_tiles.add(position)

    print(len(black_tiles))


def part2():
    lines = [line.strip() for line in open("day24.txt").readlines()]

    black_tiles = set()

    for directions in lines:
        position = (0, 0, 0)

        for direction in re.findall("nw|ne|e|se|sw|w", directions):
            position = get_next_position(position, direction)

        if position in black_tiles:
            black_tiles.remove(position)
        else:
            black_tiles.add(position)

    for day in range(100):
        all_potential_tiles = set()

        for tile in black_tiles:
            all_potential_tiles.add(tile)

            for neighbor in get_neighbors(tile):
                all_potential_tiles.add(neighbor)

        next_black_tiles = set()

        for tile in all_potential_tiles:
            num_black_neighbors = len([neighbor for neighbor in get_neighbors(tile) if neighbor in black_tiles])

            if (tile in black_tiles and 1 <= num_black_neighbors <= 2) or (tile not in black_tiles and num_black_neighbors == 2):
                next_black_tiles.add(tile)

        black_tiles = next_black_tiles

    print(len(black_tiles))


# Use cube coordinates defined here: https://www.redblobgames.com/grids/hexagons
def get_next_position(position, direction):
    if direction == "nw":
        return position[0], position[1] + 1, position[2] - 1
    elif direction == "ne":
        return position[0] + 1, position[1], position[2] - 1
    elif direction == "e":
        return position[0] + 1, position[1] - 1, position[2]
    elif direction == "se":
        return position[0], position[1] - 1, position[2] + 1
    elif direction == "sw":
        return position[0] - 1, position[1], position[2] + 1
    elif direction == "w":
        return position[0] - 1, position[1] + 1, position[2]
    else:
        assert False, "Invalid direction: " + direction


def get_neighbors(position):
    return [get_next_position(position, direction) for direction in ["nw", "ne", "e", "se", "sw", "w"]]


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
