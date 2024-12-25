from collections import defaultdict
from itertools import permutations


def part1():
    lines = [line.strip() for line in open("quest07_p1.txt").readlines()]
    ranking = []

    for line in lines:
        key, plan = line.split(":")
        plan = plan.split(",")
        total_power = 0
        cur_power = 10

        for i in range(10):
            value = plan[i % len(plan)]

            if value == "+":
                cur_power += 1
            elif value == "-":
                cur_power -= 1

            total_power += cur_power

        ranking.append((-total_power, key))

    ranking = sorted(ranking)
    print("".join([value[1] for value in ranking]))


def part2():
    track = parse_track()

    lines = [line.strip() for line in open("quest07_p2.txt").readlines()]

    ranking = []

    for line in lines:
        key, plan = line.split(":")
        plan = plan.split(",")
        total_power = 0
        cur_power = 10

        for i in range(10 * len(track)):
            track_value = track[i % len(track)]

            if track_value == "+":
                cur_power += 1
            elif track_value == "-":
                cur_power -= 1
            else:
                value = plan[i % len(plan)]

                if value == "+":
                    cur_power += 1
                elif value == "-":
                    cur_power -= 1

            total_power += cur_power

        ranking.append((-total_power, key))

    ranking = sorted(ranking)
    print("".join([value[1] for value in ranking]))


raw_track = """
S-=++=-==++=++=-=+=-=+=+=--=-=++=-==++=-+=-=+=-=+=+=++=-+==++=++=-=-=--
-                                                                     -
=                                                                     =
+                                                                     +
=                                                                     +
+                                                                     =
=                                                                     =
-                                                                     -
--==++++==+=+++-=+=-=+=-+-=+-=+-=+=-=+=--=+++=++=+++==++==--=+=++==+++-
"""


def parse_track():
    grid = [line.strip() for line in raw_track.strip().split("\n")]
    num_r = len(grid)
    num_c = len(grid[0])

    top = ""
    bottom = ""

    for c in range(1, num_c - 1):
        top += grid[0][c]
        bottom += grid[-1][-c - 1]

    left = ""
    right = ""

    for r in range(num_r):
        right += grid[r][-1]
        left += grid[-r - 1][0]

    return top + right + bottom + left


def part3():
    track = parse_track_part3()
    opponents_plan = "-,-,+,=,+,+,=,+,-,+,="
    opponents_score = execute_plan_part3(track, opponents_plan.split(","))

    unique_plans = set(permutations("+++++---==="))

    answer = 0

    for plan in unique_plans:
        if execute_plan_part3(track, plan) > opponents_score:
            answer += 1

    print(answer)


track_part3 = """
S+= +=-== +=++=     =+=+=--=    =-= ++=     +=-  =+=++=-+==+ =++=-=-=--
- + +   + =   =     =      =   == = - -     - =  =         =-=        -
= + + +-- =-= ==-==-= --++ +  == == = +     - =  =    ==++=    =++=-=++
+ + + =     +         =  + + == == ++ =     = =  ==   =   = =++=
= = + + +== +==     =++ == =+=  =  +  +==-=++ =   =++ --= + =
+ ==- = + =   = =+= =   =       ++--          +     =   = = =--= ==++==
=     ==- ==+-- = = = ++= +=--      ==+ ==--= +--+=-= ==- ==   =+=    =
-               = = = =   +  +  ==+ = = +   =        ++    =          -
-               = + + =   +  -  = + = = +   =        +     =          -
--==++++==+=+++-= =-= =-+-=  =+-= =-= =--   +=++=+++==     -=+=++==+++-
"""

def parse_track_part3():
    grid = [line.strip() for line in track_part3.strip().split("\n")]
    num_r = len(grid)
    num_c = len(grid[0])

    paths = defaultdict(str)

    for r in range(num_r):
        for c in range(num_c):
            if c < len(grid[r]) and grid[r][c] != " ":
                paths[(r, c)] = grid[r][c]

    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    track = grid[0][1]
    seen = set()
    r = 0
    c = 1

    while grid[r][c] != "S":
        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if (next_r, next_c) not in seen and paths[(next_r, next_c)] != "":
                seen.add((r, c))

                r = next_r
                c = next_c
                track += grid[r][c]

    return track


def execute_plan_part3(track, plan):
    total_power = 0
    cur_power = 10

    for i in range(2024 * len(track)):
        track_value = track[i % len(track)]

        if track_value == "+":
            cur_power += 1
        elif track_value == "-":
            cur_power -= 1
        else:
            value = plan[i % len(plan)]

            if value == "+":
                cur_power += 1
            elif value == "-":
                cur_power -= 1

        total_power += cur_power

    return total_power


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
