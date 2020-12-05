def part1():
    boarding_passes = [line.strip() for line in open("day05.txt").readlines()]
    max_id = 0

    for boarding_pass in boarding_passes:
        row = binary_search(0, 127, boarding_pass[:-3])
        col = binary_search(0, 7, boarding_pass[-3:])
        seat_id = row * 8 + col

        max_id = max(max_id, seat_id)

    print(max_id)


def binary_search(low, high, instructions):
    for instruction in instructions:
        if instruction == "F" or instruction == "L":
            high = low + (high - low) // 2
        elif instruction == "B" or instruction == "R":
            low = low + (high - low) // 2 + 1
        else:
            raise Exception("Invalid instruction: " + instruction)

    if low != high:
        raise Exception("Instructions didn't result in a single answer")

    return low


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
