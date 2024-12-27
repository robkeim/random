import heapq


def part1():
    grid = [line.strip() for line in open("quest17_p1.txt").readlines()]
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


def part2():
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
