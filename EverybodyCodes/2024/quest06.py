from collections import defaultdict


# RRXKQXBSGRMW
# Your answer length is: incorrect
# The first character of your answer is: correct
def part1():
    lines = [line.strip() for line in open("quest06_p1.txt").readlines()]

    graph = defaultdict(set)

    for line in lines:
        source, destinations = line.split(":")

        for destination in destinations.split(","):
            graph[source].add(destination)

    lengths = defaultdict(set)
    to_process = [("RR", "RR")]

    while to_process:
        word, prev = to_process.pop(0)

        if prev == "@":
            lengths[len(word)].add(word)
            continue

        for destination in graph[prev]:
            to_process.append((word + destination, destination))

    for words in lengths.values():
        if len(words) == 1:
            print(list(words)[0])
            break


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
