def part1():
    columns = [int(line.strip()) for line in open("quest11_p1.txt").readlines()]

    # Phase one
    has_moved = True
    round_number = 0

    while has_moved:
        has_moved = False
        round_number += 1

        for i in range(len(columns) - 1):
            if columns[i] > columns[i + 1]:
                columns[i] -= 1
                columns[i + 1] += 1
                has_moved = True

        if round_number == 11:
            print(calculate_checksum(columns))
            return

    # Phase two
    has_moved = True

    while has_moved:
        has_moved = False
        round_number += 1

        for i in range(len(columns) - 1):
            if columns[i] < columns[i + 1]:
                columns[i] += 1
                columns[i + 1] -= 1
                has_moved = True

        if round_number == 11:
            print(calculate_checksum(columns))
            return


def calculate_checksum(columns):
    return sum([(index + 1) * value for index, value in enumerate(columns)])


def part2():
    columns = [int(line.strip()) for line in open("quest11_p2.txt").readlines()]

    # Phase one
    has_moved = True
    round_number = 0

    while has_moved:
        has_moved = False
        round_number += 1

        for i in range(len(columns) - 1):
            if columns[i] > columns[i + 1]:
                columns[i] -= 1
                columns[i + 1] += 1
                has_moved = True

    # Phase two
    has_moved = True

    while has_moved:
        has_moved = False
        round_number += 1

        for i in range(len(columns) - 1):
            if columns[i] < columns[i + 1]:
                columns[i] += 1
                columns[i + 1] -= 1
                has_moved = True

    # Subtract the two loops where we're just checking that there are no more moves
    print(round_number - 2)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
