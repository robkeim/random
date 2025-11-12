from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("quest07_p1.txt").readlines()]
    names = lines[0].split(",")

    pairs = set()

    for line in lines[2:]:
        first, remaining = line.split(" > ")
        for char in remaining.split(","):
            pairs.add(first + char)

    for name in names:
        found = True
        for i in range(len(name) - 1):
            if name[i:i + 2] not in pairs:
                found = False
                break

        if found:
            print(name)
            return

    assert False, "No solution found"


def part2():
    lines = [line.strip() for line in open("quest07_p2.txt").readlines()]
    names = lines[0].split(",")

    pairs = set()

    for line in lines[2:]:
        first, remaining = line.split(" > ")
        for char in remaining.split(","):
            pairs.add(first + char)

    result = 0

    for index, name in enumerate(names):
        found = True
        for i in range(len(name) - 1):
            if name[i:i + 2] not in pairs:
                found = False
                break

        if found:
            result += index + 1

    print(result)


def part3():
    lines = [line.strip() for line in open("quest07_p3.txt").readlines()]
    prefixes = lines[0].split(",")

    mappings = defaultdict(set)

    for line in lines[2:]:
        first, remaining = line.split(" > ")
        for char in remaining.split(","):
            mappings[first].add(char)

    result = set()

    for prefix in prefixes:
        valid_prefix = True

        for i in range(len(prefix) - 1):
            if prefix[i + 1] not in mappings[prefix[i]]:
                valid_prefix = False
                break

        if valid_prefix:
            for target_length in range(7, 12):
                remaining = target_length - len(prefix)
                result |= all_combinations(prefix, remaining, mappings)

    print(len(result))


def all_combinations(word, remaining, mappings):
    if remaining == 0:
        return {word}

    result = set()

    for next_char in mappings[word[-1]]:
        result |= all_combinations(word + next_char, remaining - 1, mappings)

    return result


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
