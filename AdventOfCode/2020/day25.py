def part1():
    card_public_key, door_public_key = [int(line.strip()) for line in open("day25.txt").readlines()]

    subject_number = 7
    result = 1
    card_loop_size = 0

    while result != card_public_key:
        result = (result * subject_number) % 20201227
        card_loop_size += 1

    result = door_public_key

    for _ in range(card_loop_size - 1):
        result = (result * door_public_key) % 20201227

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
