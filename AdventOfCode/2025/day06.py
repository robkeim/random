def part1():
    lines = [line.strip() for line in open("day06.txt").readlines()]
    numbers = [[int(num) for num in line.split()] for line in lines[:-1]]
    operations = lines[-1].split()

    num_r = len(numbers)
    num_c = len(numbers[0])

    total = 0

    for c in range(num_c):
        operation = operations[c]

        result = 1 if operation == "*" else 0

        for r in range(num_r):
            if operation == "+":
                result += numbers[r][c]
            else:
                result *= numbers[r][c]

        total += result

    print(total)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
