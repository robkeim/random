deltas = {
    "R": (1, 0),
    "L": (-1, 0),
    "D": (0, 1),
    "U": (0, -1)
}


def part1():
    lines = [line.split() for line in open("day09.txt").readlines()]

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

            tail = get_next_position(head, tail)

            visited.add(tuple(tail))

    print(len(visited))


def get_next_position(head, tail):
    tail = list(tail)
    if head == tail:
        return tail
    elif head[0] == tail[0]:
        if abs(head[1] - tail[1]) > 1:
            tail[1] += 1 if head[1] > tail[1] else -1
    elif head[1] == tail[1]:
        if abs(head[0] - tail[0]) > 1:
            tail[0] += 1 if head[0] > tail[0] else -1
    else:
        if abs(head[0] - tail[0]) + abs(head[1] - tail[1]) == 2:
            return tail

        tail[0] += 1 if head[0] > tail[0] else -1
        tail[1] += 1 if head[1] > tail[1] else -1

    return tail


def part2():
    lines = [line.split() for line in open("day09.txt").readlines()]

    nodes = []

    for i in range(10):
        nodes.append([0, 0])

    visited = set()
    visited.add((0, 0))

    for direction, num_steps in lines:
        num_steps = int(num_steps)

        for _ in range(num_steps):
            dx, dy = deltas[direction]
            nodes[0][0] += dx
            nodes[0][1] += dy

            for i in range(1, len(nodes)):
                nodes[i] = get_next_position(nodes[i - 1], nodes[i])

            print(nodes[8], nodes[9], direction)
            visited.add(tuple(nodes[-1]))

    print(len(visited))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
