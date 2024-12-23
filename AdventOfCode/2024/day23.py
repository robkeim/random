import networkx
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
    lines = [line.strip() for line in open("day23.txt").readlines()]
    graph = networkx.Graph()

    for line in lines:
        source, destination = line.split("-")
        graph.add_edge(source, destination)

    # https://en.m.wikipedia.org/wiki/Clique_problem
    # Thank you networkx :)
    cliques = list(networkx.find_cliques(graph))
    max_length = 0
    max_clique = None

    for clique in cliques:
        if len(clique) > max_length:
            max_length = len(clique)
            max_clique = clique

    print(",".join(sorted(max_clique)))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
