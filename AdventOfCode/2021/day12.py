from collections import defaultdict


def part1():
    count_possible_paths(False)


def part2():
    count_possible_paths(True)


def count_possible_paths(can_revisit_small_cave):
    lines = [line.strip() for line in open("day12.txt").readlines()]

    graph = defaultdict(list)

    for line in lines:
        start, destination = line.split("-")

        if destination != "start":
            graph[start].append(destination)

        if start != "start":
            graph[destination].append(start)

    total_paths = 0
    to_process = [("start", "start", False)]

    while len(to_process) > 0:
        path_so_far, cur_node, has_revisited_small_cave = to_process.pop()

        if cur_node == "end":
            total_paths += 1
            continue

        for next_node in graph[cur_node]:
            if next_node.upper() == next_node or next_node not in path_so_far\
                    or (can_revisit_small_cave and not has_revisited_small_cave):
                revisit_small_cave = next_node.upper() != next_node and next_node in path_so_far

                to_process.append((
                    path_so_far + "," + next_node,
                    next_node,
                    has_revisited_small_cave or revisit_small_cave
                ))

    print(total_paths)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
