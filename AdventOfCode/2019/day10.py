import math

from collections import defaultdict


def part1():
    rows = [line.strip() for line in open("day10.txt").readlines()]

    asteroids = set()
    for y in range(len(rows)):
        for x in range(len(rows[0])):
            if rows[y][x] == "#":
                asteroids.add((x, y))

    max_visible = 0
    max_visible_coordinates = 0

    for start in asteroids:
        num_visible = 0
        for target in asteroids:
            if start == target:
                continue

            dx, dy = find_dx_dy(start, target)

            is_blocked = False

            cur = start

            while True:
                cur = cur[0] + dx, cur[1] + dy

                if cur == target:
                    break

                if cur in asteroids:
                    is_blocked = True
                    break

            if not is_blocked:
                num_visible += 1

        if num_visible > max_visible:
            max_visible = num_visible
            max_visible_coordinates = start

    print(max_visible)
    return max_visible_coordinates


def find_dx_dy(first, second):
    delta_x = abs(first[0] - second[0])
    delta_y = abs(first[1] - second[1])

    gcd = find_gcd(delta_x, delta_y)

    delta_x //= gcd
    delta_y //= gcd

    if first[0] > second[0]:
        delta_x *= -1

    if first[1] > second[1]:
        delta_y *= -1

    return delta_x, delta_y


def find_gcd(x, y):
    while y:
        x, y = y, x % y

    return x


def part2():
    target_x, target_y = part1()

    rows = [line.strip() for line in open("day10.txt").readlines()]

    asteroids = set()
    for y in range(len(rows)):
        for x in range(len(rows[0])):
            if rows[y][x] == "#":
                asteroids.add((x, y))

    values = defaultdict(list)

    for asteroid in asteroids:
        value = (math.degrees(math.atan2(asteroid[1] - target_y, asteroid[0] - target_x)) + 90) % 360
        values[value].append(asteroid)

    if len(values) < 200:
        raise Exception("Multiple rotations not implemented yet")

    key = sorted(values)[199]
    results = values[key]

    if len(results) > 1:
        raise Exception("Choosing closest asteroid not yet implemented")

    print(results[0][0] * 100 + results[0][1])


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
