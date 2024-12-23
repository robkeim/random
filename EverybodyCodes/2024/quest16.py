def part1():
    lines = [line.rstrip() for line in open("quest16_p1.txt").readlines()]
    num_positions = [int(value) for value in lines[0].split(",")]
    symbols = [[] for _ in range(len(num_positions))]

    for line in lines[2:]:
        for i in range(len(num_positions)):
            start = i * 4
            if start < len(line) and line[start] != " ":
                symbols[i].append(line[start:start + 3])

    result = ""

    for i, symbol in enumerate(symbols):
        result += symbol[((100 * num_positions[i]) % len(symbol))] + " "

    score = 0

    for item in set(result.replace(" ", "")):
        count = result.count(item)

        if count == 3:
            score += 1
        elif count > 3:
            score += count - 2

    print(result, score)


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
