def part1():
    lines = [line.split() for line in open("day09.txt").readlines()]

    deltas = {
        "R": (1, 0),
        "L": (-1, 0),
        "D": (0, 1),
        "U": (0, -1)
    }

    head = [0, 0]
    tail = [0, 0]
    visited = set()
    visited.add((0, 0))

    for direction, num_steps in lines:
        num_steps = int(num_steps)

        for _ in range(num_steps):
            dx, dy = deltas[direction]
            head[0] += dx
            head[1] += dy

            if head == tail:
                continue
            elif head[0] == tail[0]:
                if abs(head[1] - tail[1]) > 1:
                    tail[1] += dy
            elif head[1] == tail[1]:
                if abs(head[0] - tail[0]) > 1:
                    tail[0] += dx
            else:
                if abs(head[0] - tail[0]) + abs(head[1] - tail[1]) == 2:
                    continue

                tail[0] += 1 if head[0] > tail[0] else -1
                tail[1] += 1 if head[1] > tail[1] else -1

            visited.add(tuple(tail))

    print(len(visited))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
