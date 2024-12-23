from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day23.txt").readlines()]
    graph = defaultdict(set)

    for line in lines:
        a, b = line.split("-")
        graph[a].add(b)
        graph[b].add(a)

    answer = set()

    for key1 in graph:
        for key2 in graph:
            for key3 in graph:
                if "t" not in key1[0] + key2[0] + key3[0]:
                    continue

                if len(set([key1, key2, key3])) != 3:
                    continue

                if key2 in graph[key1] and key3 in graph[key1] and key2 in graph[key3]:
                    answer.add(tuple(sorted([key1, key2, key3])))

    print(answer)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
