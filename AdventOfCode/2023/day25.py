import networkx

# This is the minimum cut problem:
# https://en.wikipedia.org/wiki/Minimum_cut
# It can be solved by the Stoer-Wagner algorithm given the constraints we have (thank you networkx :))
# https://en.wikipedia.org/wiki/Stoer%E2%80%93Wagner_algorithm
def part1():
    lines = [line.strip() for line in open("day25.txt").readlines()]
    graph = networkx.Graph()

    for line in lines:
        source, destinations = line.split(":")

        for destination in destinations.split():
            graph.add_edge(source, destination)

    num_cuts, partitions = networkx.stoer_wagner(graph)

    assert num_cuts == 3, f"Unexpected number of cuts: {num_cuts}"

    print(len(partitions[0]) * len(partitions[1]))



def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
