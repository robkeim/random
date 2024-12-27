import heapq


def part1():
    grid = [line.strip() for line in open("quest17_p1.txt").readlines()]
    solve_parts_1_and_2(grid)

def part2():
    grid = [line.strip() for line in open("quest17_p2.txt").readlines()]
    solve_parts_1_and_2(grid)


def solve_parts_1_and_2(grid):
    num_r = len(grid)
    num_c = len(grid[0])

    stars = []

    for r in range(num_r):
        for c in range(num_c):
            if grid[r][c] == "*":
                stars.append((r, c))

    # Calculate MST (minimum spanning tree) using Prim's algorithm
    missing = set(stars[1:])
    answer = len(stars)

    to_process = []

    for r, c in missing:
        heapq.heappush(to_process, (manhatten_distance(stars[0][0], stars[0][1], r, c), r, c))

    while to_process:
        distance, r, c = heapq.heappop(to_process)

        if (r, c) not in missing:
            continue

        answer += distance
        missing.remove((r, c))

        for next_r, next_c in missing:
            heapq.heappush(to_process, (manhatten_distance(r, c, next_r, next_c), next_r, next_c))

    print(answer)


def manhatten_distance(r1, c1, r2, c2):
    return abs(r1 - r2) + abs(c1 - c2)


def part3():
    grid = [line.strip() for line in open("quest17_p3.txt").readlines()]

    stars = set()

    for r in range(len(grid)):
        for c in range(len(grid[0])):
            if grid[r][c] == "*":
                stars.add((r, c))

    mst_sizes = []

    while len(stars) > 0:
        num_stars, stars = calculate_mst(stars)
        mst_sizes.append(num_stars)

    mst_sizes = sorted(mst_sizes, reverse=True)

    print(mst_sizes[0] * mst_sizes[1] * mst_sizes[2])


def calculate_mst(stars):
    start = list(stars)[0]
    stars.remove(start)
    answer = 1

    to_process = []

    for r, c in stars:
        distance = manhatten_distance(start[0], start[1], r, c)

        if distance < 6:
            heapq.heappush(to_process, (distance, r, c))

    while to_process:
        distance, r, c = heapq.heappop(to_process)

        if (r, c) not in stars:
            continue

        answer += 1 + distance
        stars.remove((r, c))

        for next_r, next_c in stars:
            distance = manhatten_distance(r, c, next_r, next_c)

            if distance < 6:
                heapq.heappush(to_process, (distance, next_r, next_c))

    return answer, stars


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
