from collections import defaultdict


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
    lines = [line.strip() for line in open("quest06_p2.txt").readlines()]

    graph = defaultdict(set)

    for line in lines:
        source, destinations = line.split(":")

        for destination in destinations.split(","):
            graph[source].add(destination)

    lengths = defaultdict(set)
    to_process = [("R", 2, "RR")]

    while to_process:
        word, length, prev = to_process.pop(0)

        if prev == "@":
            lengths[length].add(word)
            continue

        for destination in graph[prev]:
            to_process.append((word + destination[0], length + len(destination), destination))

    max_length = 0
    answer = None
    for length, words in lengths.items():
        if len(words) == 1 and length > max_length:
            answer = list(words)[0]
            max_length = length

    print(answer)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
