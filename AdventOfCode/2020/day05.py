def part1():
    print(max(parse_boarding_passes()))


def part2():
    boarding_passes = sorted(parse_boarding_passes())

    prev = boarding_passes[0]

    for i in range(1, len(boarding_passes)):
        if boarding_passes[i] != prev + 1:
            print(prev + 1)
            return

        prev = boarding_passes[i]

    raise Exception("No ticket found")


def parse_boarding_passes():
    boarding_passes = [line.strip() for line in open("day05.txt").readlines()]
    result = []

    for boarding_pass in boarding_passes:
        row = binary_search(0, 127, boarding_pass[:-3])
        col = binary_search(0, 7, boarding_pass[-3:])
        seat_id = row * 8 + col

        result.append(seat_id)

    return result


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


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
