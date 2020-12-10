def part1():
    volts = sorted([int(line.strip()) for line in open("day10.txt").readlines()])
    volts = [0] + volts + [volts[-1] + 3]

    one = three = 0

    for i in range(1, len(volts)):
        diff = volts[i] - volts[i - 1]

        if diff == 1:
            one += 1
        elif diff == 3:
            three += 1

    print(one * three)


def part2():
    volts = set([int(line.strip()) for line in open("day10.txt").readlines()])

    target = max(volts) + 3
    volts.add(target)

    print(num_valid(0, target, volts, {target: 1}))


def num_valid(cur, target, volts, cache):
    if cur in cache:
        return cache[cur]

    result = 0

    for i in range(1, 4):
        if cur + i in volts:
            result += num_valid(cur + i, target, volts, cache)

    cache[cur] = result
    return result


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
