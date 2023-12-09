from collections import defaultdict


# Make the assumption that we're only going to visit each directory once
def part1():
    lines = [line.strip() for line in open("day07.txt").readlines()]
    sizes = get_directory_sizes(lines)

    print(sum([size for size in sizes.values() if size <= 100000]))


def get_directory_sizes(lines):
    index = 0
    cur_dir = None
    sizes = defaultdict(int)

    while index < len(lines):
        line = lines[index]

        if line.startswith("$ cd"):
            argument = line.split()[2]

            if argument == "/":
                cur_dir = ["/"]
            elif argument == "..":
                cur_dir.pop()
            else:
                cur_dir.append(argument)

            index += 1
        elif line == "$ ls":
            index += 1

            while index < len(lines) and not lines[index].startswith("$"):
                if not lines[index].startswith("dir"):
                    file_size = int(lines[index].split()[0])

                    to_add = list(cur_dir)

                    while len(to_add) > 0:
                        sizes[tuple(to_add)] += file_size
                        to_add.pop()

                index += 1
        else:
            assert False, "Unexpected line: {}".format(line)

    return sizes


def part2():
    lines = [line.strip() for line in open("day07.txt").readlines()]
    sizes = sorted(get_directory_sizes(lines).values())
    total_disk_size = 70000000
    min_required = 30000000
    cur_used = sizes[-1]
    need_to_free = min_required - (total_disk_size - cur_used)

    for i in range(len(sizes)):
        if sizes[i] >= need_to_free:
            print(sizes[i])
            break


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
