import re


def part1():
    lines = [line.strip() for line in open("day05.txt").readlines()]
    cur_iteration = [int(seed) for seed in re.findall(r"\d+", lines[0])]

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


def part2():
    lines = [line.strip() for line in open("day05.txt").readlines()]
    first_line = [int(seed) for seed in re.findall(r"\d+", lines[0])]
    seeds = [create_range(first_line[i], first_line[i + 1]) for i in range(len(first_line)) if i % 2 == 0]

    mappings = parse_mappings(lines)
    for mapping in mappings:
        seeds = apply_mapping(seeds, mapping)

    print(min([seed[0] for seed in seeds]))


def create_range(start, length):
    return start, start + length - 1


def parse_mappings(lines):
    i = 3
    mappings = []
    cur_mappings = []
    lines = lines + [""]  # Simplify the parsing below

    while i < len(lines):
        if len(lines[i]) == 0:
            mappings.append(cur_mappings)
            cur_mappings = []
            i += 1
        else:
            dest_start, source_start, length = [int(value) for value in lines[i].split()]
            cur_mappings.append((source_start, source_start + length - 1, dest_start))

        i += 1

    return mappings


def apply_mapping(seeds, mapping):
    to_process = list(seeds)
    result = []

    while len(to_process) > 0:
        seed_start, seed_end = to_process.pop()
        found = False

        for source_start, source_end, dest_start in mapping:
            if seed_end < source_start or seed_start > source_end:
                continue

            found = True

            overlap_start = max(source_start, seed_start)
            overlap_end = min(source_end, seed_end)

            if seed_start < overlap_start:
                to_process.append((seed_start, overlap_start - 1))

            if seed_end > overlap_end:
                to_process.append((overlap_end + 1, seed_end))

            offset = overlap_start - source_start
            length = overlap_end - overlap_start + 1

            result.append((dest_start + offset, dest_start + offset + length))

        if not found:
            result.append((seed_start, seed_end))

    return result


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
