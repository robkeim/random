from collections import defaultdict


def part1():
    grid = [line.strip() for line in open("day03.txt").readlines()]
    print(part1logic(grid))


def part1logic(grid):
    width = len(grid[0])
    height = len(grid)
    digits = set("0123456789")
    symbols = set()
    numbers = []

    # Extract numbers and symbols
    for y in range(height):
        num = None
        for x in range(width):
            char = grid[y][x]

            if char in digits:
                if num:
                    num += char
                else:
                    num = char

                continue

            if char != ".":
                symbols.add("{},{}".format(x, y))

            if num:
                start_x = x - len(num)
                numbers.append((start_x, y, num))
                num = None

        if num:
            start_x = width - len(num)
            numbers.append((start_x, y, num))

    result = 0

    # Find non-adjacent numbers
    for start_x, start_y, number in numbers:
        has_adjacent = False

        for x in range(start_x - 1, start_x + len(number) + 1): # Is this +1 or 2 right?
            if has_adjacent:
                break

            for y in range(start_y - 1, start_y + 2):
                if "{},{}".format(x, y) in symbols:
                    has_adjacent = True
                    result += int(number)
                    break

    return result


def part1tests():
    test_cases = [
        ([
            "1..",
            "...",
            "..."
        ], 1),
        ([
             ".1.",
             "...",
             "..."
         ], 1),
        ([
             "..1",
             "...",
             "..."
         ], 1),
        ([
             "...",
             "1..",
             "..."
         ], 1),
        ([
             "...",
             ".1.",
             "..."
         ], 1),
        ([
             "...",
             "..1",
             "..."
         ], 1),
        ([
             "...",
             "...",
             "1.."
         ], 1),
        ([
             "...",
             "...",
             ".1."
         ], 1),
        ([
             "...",
             "...",
             "..1"
         ], 1),
        ([
             "1..",
             ".*.",
             "..."
         ], 0),
        ([
             ".1.",
             ".*.",
             "..."
         ], 0),
        ([
             "..1",
             ".*.",
             "..."
         ], 0),
        ([
             "...",
             "1*.",
             "..."
         ], 0),
        ([
             "...",
             ".*1",
             "..."
         ], 0),
        ([
             "...",
             ".*.",
             "1.."
         ], 0),
        ([
             "...",
             ".*.",
             ".1."
         ], 0),
        ([
             "...",
             ".*.",
             "..1"
         ], 0),
        ([
             "12.",
             "..*",
             "..."
         ], 0),
        ([
             ".123..",
             ".....*",
             "......"
         ], 123),
    ]

    for grid, expected_result in test_cases:
        result = part1logic(grid)

        if expected_result != result:
            raise Exception("Expected {}, Actual {}".format(expected_result, result))


def part2():
    grid = [line.strip() for line in open("day03.txt").readlines()]

    width = len(grid[0])
    height = len(grid)
    digits = set("0123456789")
    stars = set()
    numbers = []

    # Extract numbers and symbols
    for y in range(height):
        num = None
        for x in range(width):
            char = grid[y][x]

            if char in digits:
                if num:
                    num += char
                else:
                    num = char

                continue

            if char == "*":
                stars.add("{},{}".format(x, y))

            if num:
                start_x = x - len(num)
                numbers.append((start_x, y, num))
                num = None

        if num:
            start_x = width - len(num)
            numbers.append((start_x, y, num))

    # Find gears
    adjacents = defaultdict(set)

    for start_x, start_y, number in numbers:
        for x in range(start_x - 1, start_x + len(number) + 1): # Is this +1 or 2 right?
            for y in range(start_y - 1, start_y + 2):
                coordinate = "{},{}".format(x, y)

                if coordinate in stars:
                    adjacents[coordinate].add(number)

    result = 0

    for coordinate in adjacents:
        adjacent_numbers = adjacents[coordinate]

        if len(adjacent_numbers) == 2:
            gear_ratio = 1

            for adjacent_number in adjacent_numbers:
                gear_ratio *= int(adjacent_number)

            result += gear_ratio

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
