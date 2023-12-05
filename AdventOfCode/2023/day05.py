import re


def part1():
    lines = [line.strip() for line in open("day05.txt").readlines()]
    cur_iteration = [int(seed) for seed in re.findall(r"\d+", lines[0])]

    calculate_lowest_location(lines, cur_iteration)


def part2():
    return  # The brute force solution doesn't work here because the number of seeds is too big
    lines = [line.strip() for line in open("day05.txt").readlines()]
    first_line = [int(seed) for seed in re.findall(r"\d+", lines[0])]
    seeds = []

    for i in range(0, len(first_line), 2):
        for offset in range(first_line[i + 1]):
            seeds.append(first_line[i] + offset)

    calculate_lowest_location(lines, seeds)


def calculate_lowest_location(lines, cur_iteration):
    i = 3
    mappings = []
    lines = lines + [""]  # Simplify the parsing below

    while i < len(lines):
        if len(lines[i]) == 0:
            next_iteration = []

            for item in cur_iteration:
                found_replacement = False
                for destination_start, source_start, length in mappings:
                    if source_start <= item < (source_start + length):
                        offset = item - source_start
                        next_iteration.append(destination_start + offset)
                        found_replacement = True
                        break

                if not found_replacement:
                    next_iteration.append(item)

            cur_iteration = next_iteration

            mappings = []
            i += 1
        else:
            mappings.append([int(value) for value in lines[i].split()])

        i += 1

    print(min(cur_iteration))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
