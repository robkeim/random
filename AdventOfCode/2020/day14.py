import re


def part1():
    lines = [line.strip() for line in open("day14.txt").readlines()]

    and_mask = or_mask = None
    mem = dict()

    for line in lines:
        mask_regex = re.fullmatch("mask = ([01X]+)", line)

        if mask_regex:
            and_mask = int(mask_regex.group(1).replace("X", "1"), 2)
            or_mask = int(mask_regex.group(1).replace("X", "0"), 2)
            continue

        mem_regex = re.fullmatch("mem\[(\d+)\] = (\d+)", line)

        if not mem_regex:
            raise Exception("Invalid line format: " + line)

        value = int(mem_regex.group(2))
        value &= and_mask
        value |= or_mask

        mem[int(mem_regex.group(1))] = value

    result = 0

    for key in mem:
        result += mem[key]

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
