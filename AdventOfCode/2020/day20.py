import re
from collections import defaultdict
from copy import deepcopy


def part1():
    placed_tiles = get_placed_tiles()

    min_x = float("inf")
    max_x = float("-inf")
    min_y = float("inf")
    max_y = float("-inf")

    for x, y in placed_tiles:
        min_x = min(min_x, x)
        max_x = max(max_x, x)
        min_y = min(min_y, y)
        max_y = max(max_y, y)

    result = 1

    result *= placed_tiles[(min_x, max_y)][0]
    result *= placed_tiles[(max_x, max_y)][0]
    result *= placed_tiles[(max_x, min_y)][0]
    result *= placed_tiles[(min_x, min_y)][0]

    print(result)


def part2():
    giant_tile = extract_giant_tile()

    transformations = [
        giant_tile,
        rotate(giant_tile),
        rotate(rotate(giant_tile)),
        rotate(rotate(rotate(giant_tile))),
        vertical_flip(giant_tile),
        vertical_flip(rotate(giant_tile)),
        vertical_flip(rotate(rotate(giant_tile))),
        vertical_flip(rotate(rotate(rotate(giant_tile))))
    ]

    found = False

    for transformation in transformations:
        result = count_non_sea_monsters(transformation)

        if result > 0:
            found = True
            print(result)
            break

    assert found, "No sea monsters found"


def count_non_sea_monsters(tile):
    len_ = len(tile)

    tile = deepcopy(tile)

    for i in range(len_):
        tile[i] = list(tile[i])

    offsets = [(0, 18), (1, 0), (1, 5), (1, 6), (1, 11), (1, 12), (1, 17), (1, 18), (1, 19), (2, 1), (2, 4), (2, 7), (2, 10), (2, 13), (2, 16)]

    found = False

    for r in range(len_ - 3):
        for c in range(len_ - 20):
            is_sea_monster = True

            for dr, dc in offsets:
                if tile[r + dr][c + dc] != "#":
                    is_sea_monster = False
                    break

            if is_sea_monster:
                found = True

                for dr, dc in offsets:
                    tile[r + dr][c + dc] = "_"


    if found:
        result = 0

        for r in range(len_):
            for c in range(len_):
                if tile[r][c] == "#":
                    result += 1

        return result

    return 0


def get_placed_tiles():
    tiles = parse_tiles()

    # Place first tile
    placed_tiles = dict()

    first_tile_id, first_tile, _ = tiles[0]

    first_tile_edges = extract_edges(first_tile)

    placed_tiles[(0, 0)] = (first_tile_id, first_tile, first_tile_edges)

    tiles = tiles[1:]

    # Place remaining tiles

    while len(tiles) > 0:
        found = False
        for placed_tile_key in placed_tiles:
            _, placed_tile, placed_tile_edges = placed_tiles[placed_tile_key]

            for tile_id, tile, tile_edges in tiles:
                intersection = placed_tile_edges.intersection(tile_edges)
                if len(intersection) > 0:
                    assert len(intersection) == 1, "There should only be one overlap"

                    intersection = list(intersection)[0]

                    # Find direction
                    if extract_top(placed_tile) == intersection:
                        direction = (0, 1)
                        test = extract_bottom
                    elif extract_bottom(placed_tile) == intersection:
                        direction = (0, -1)
                        test = extract_top
                    elif extract_left(placed_tile) == intersection:
                        direction = (-1, 0)
                        test = extract_right
                    elif extract_right(placed_tile) == intersection:
                        direction = (1, 0)
                        test = extract_left
                    else:
                        assert False, "No valid direction found for tile"

                    new_position = (placed_tile_key[0] + direction[0], placed_tile_key[1] + direction[1])

                    if test(tile) == intersection:
                        pass
                    elif test(rotate(tile)) == intersection:
                        tile = rotate(tile)
                    elif test(rotate(rotate(tile))) == intersection:
                        tile = rotate(rotate(tile))
                    elif test(rotate(rotate(rotate(tile)))) == intersection:
                        tile = rotate(rotate(rotate(tile)))
                    elif test(vertical_flip(tile)) == intersection:
                        tile = vertical_flip(tile)
                    elif test(vertical_flip(rotate(tile))) == intersection:
                        tile = vertical_flip(rotate(tile))
                    elif test(vertical_flip(rotate(rotate(tile)))) == intersection:
                        tile = vertical_flip(rotate(rotate(tile)))
                    elif test(vertical_flip(rotate(rotate(rotate(tile))))) == intersection:
                        tile = vertical_flip(rotate(rotate(rotate(tile))))
                    else:
                        assert False, "No valid rotation/flip found"

                    new_edges = extract_edges(tile)
                    placed_tiles[new_position] = (tile_id, tile, new_edges)

                    tiles = [tile for tile in tiles if tile[0] != tile_id]
                    found = True

                    break

            if found:
                break

    return placed_tiles


def parse_tiles():
    lines = [line.strip() for line in open("day20.txt").readlines()]

    tiles = []

    i = 0

    while i < len(lines):
        match = re.fullmatch("Tile (\d+):", lines[i])
        assert match, "Invalid tile id: " + lines[i]

        tile_id = int(match.group(1))
        tile = []

        for j in range(10):
            tile.append(lines[i + 1 + j])

        sides = set()
        sides.add(extract_top(tile))
        sides.add(extract_top(tile)[::-1])
        sides.add(extract_bottom(tile))
        sides.add(extract_bottom(tile)[::-1])
        sides.add(extract_left(tile))
        sides.add(extract_left(tile)[::-1])
        sides.add(extract_right(tile))
        sides.add(extract_right(tile)[::-1])

        tiles.append((tile_id, tile, sides))

        i += 12

    return tiles


def extract_top(tile):
    return "".join(tile[0][:])


def extract_bottom(tile):
    return "".join(tile[len(tile) - 1][:])


def extract_left(tile):
    result = []

    for i in range(len(tile)):
        result.append(tile[i][0])

    return "".join(result)


def extract_right(tile):
    result = []

    for i in range(len(tile)):
        result.append(tile[i][len(tile) - 1])

    return "".join(result)


def extract_edges(tile):
    result = set()

    result.add(extract_top(tile))
    result.add(extract_bottom(tile))
    result.add(extract_left(tile))
    result.add(extract_right(tile))

    return result


def rotate(tile):
    len_ = len(tile)

    new = [["_" for _ in range(len_)] for _ in range(len_)]

    for i in range(len_):
        for j in range(len_):
            new[i][j] = tile[len_ - j - 1][i]

    result = []

    for row in new:
        result.append("".join(row))

    return result


def vertical_flip(tile):
    return tile[::-1]


def extract_giant_tile():
    tiles = get_placed_tiles()

    min_x = float("inf")
    max_x = float("-inf")
    min_y = float("inf")
    max_y = float("-inf")

    for x, y in tiles:
        min_x = min(min_x, x)
        max_x = max(max_x, x)
        min_y = min(min_y, y)
        max_y = max(max_y, y)

    # Remove borders
    borderless_tiles = dict()

    for key in tiles:
        tile = tiles[key][1]
        result = []

        for index in range(1, len(tile) - 1):
            result.append(tile[index][1:-1])

        borderless_tiles[key] = result[::-1]

    tiles = borderless_tiles

    rows = defaultdict(list)
    row = 0

    for y in range(min_y, max_y + 1):
        for x in range(min_x, max_x + 1):
            tile = tiles[(x, y)]

            for index, tile_row in enumerate(tile):
                rows[row + index].append(tile_row)

        row += 8

    giant_tile = []

    for i in range(row):
        giant_tile.append("".join(rows[i]))

    return giant_tile


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
