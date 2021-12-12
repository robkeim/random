from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day12.txt").readlines()]

    graph = defaultdict(list)

    for line in lines:
        start, destination = line.split("-")

        if destination != "start":
            graph[start].append(destination)

        if start != "start":
            graph[destination].append(start)

    total_paths = 0
    to_process = [("", "start")]
    seen = set()

    while len(to_process) > 0:
        path_so_far, cur_node = to_process.pop()

        if path_so_far in seen:
            continue

        seen.add(path_so_far)

        if cur_node == "end":
            total_paths += 1
            continue

        for next_node in graph[cur_node]:
            if next_node.upper() == next_node or next_node not in path_so_far:
                to_process.append((path_so_far + "-" + next_node, next_node))

    print(total_paths)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
