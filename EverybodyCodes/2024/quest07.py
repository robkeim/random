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
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
