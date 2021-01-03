import re


def part1():
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


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
