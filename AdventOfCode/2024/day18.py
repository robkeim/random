import heapq


def part1():
    walls = set()

    for line in open("day18.txt").readlines()[:1024]:
        r, c = line.split(",")
        walls.add((int(r), int(c)))

    heap = [(0, 0, 0)]
    seen = set()
    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    size = 70

    while heap:
        cost, r, c = heapq.heappop(heap)

        if r == size and c == size:
            print(cost)
            break

        if (r, c) in seen:
            continue

        seen.add((r, c))

        for dr, dc in dirs:
            next_r = r + dr
            next_c = c + dc

            if 0 <= next_r <= size and 0 <= next_c <= size and (next_r, next_c) not in walls:
                heapq.heappush(heap, (cost + 1, next_r, next_c))


def print_grid(walls, size):
    for r in range(size + 1):
        row = ""

        for c in range(size + 1):
            if (r, c) in walls:
                row += "#"
            else:
                row += "."

        print(row)


def part2():
    walls = set()
    lines = []

    for line in open("day18.txt").readlines():
        r, c = line.split(",")
        lines.append((int(r), int(c)))

    for i in range(1024):
        walls.add(lines[i])

    cur_index = 1024

    while True:
        walls.add(lines[cur_index])

        heap = [(0, 0, 0)]
        seen = set()
        dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
        size = 70
        has_solution = False

        while heap:
            cost, r, c = heapq.heappop(heap)

            if r == size and c == size:
                has_solution = True
                break

            if (r, c) in seen:
                continue

            seen.add((r, c))

            for dr, dc in dirs:
                next_r = r + dr
                next_c = c + dc

                if 0 <= next_r <= size and 0 <= next_c <= size and (next_r, next_c) not in walls:
                    heapq.heappush(heap, (cost + 1, next_r, next_c))

        if not has_solution:
            r, c = lines[cur_index]
            print(f"{r},{c}")
            break

        cur_index += 1


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
