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
    lines = [line.strip() for line in open("day14.txt").readlines()]

    mem = dict()
    mask = None

    for line in lines:
        mask_regex = re.fullmatch("mask = ([01X]+)", line)

        if mask_regex:
            mask = mask_regex.group(1)
            continue

        mem_regex = re.fullmatch("mem\[(\d+)\] = (\d+)", line)

        if not mem_regex:
            raise Exception("Invalid line format: " + line)

        address = bin(int(mem_regex.group(1)))[2:]
        value = int(mem_regex.group(2))

        while len(address) < len(mask):
            address = "0" + address

        result = []

        for i in range(len(mask)):
            if mask[i] == "0":
                result.append(address[i])
            elif mask[i] == "1":
                result.append("1")
            elif mask[i] == "X":
                result.append("X")
            else:
                raise Exception("Invalid value in mask: " + mask)

        for option in calculate_all_values("".join(result)):
            mem[int(option, 2)] = value

    result = 0

    for key in mem:
        result += mem[key]

    print(result)


def calculate_all_values(mask):
    if "X" not in mask:
        return [mask]

    index = mask.find("X")

    return calculate_all_values(mask[:index] + "0" + mask[index + 1:]) + calculate_all_values(mask[:index] + "1" + mask[index + 1:])


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
