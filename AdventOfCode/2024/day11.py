from functools import lru_cache


def part1():
    stones = [int(stone) for stone in open("day11.txt").read().split()]

    for _ in range(25):
        next_iteration = []

        for stone in stones:
            if stone == 0:
                next_iteration.append(1)
            elif len(str(stone)) % 2 == 0:
                stone = str(stone)
                length = len(stone)
                next_iteration.append(int(stone[:length // 2]))
                next_iteration.append(int(stone[length // 2:]))
            else:
                next_iteration.append(stone * 2024)

        stones = next_iteration

    print(len(stones))


def part2():
    stones = [int(stone) for stone in open("day11.txt").read().split()]
    print(sum([calculate_num_stones(stone, 75) for stone in stones]))


@lru_cache(1_000_000)
def calculate_num_stones(stone, iterations):
    if iterations == 0:
        return 1

    if stone == 0:
        return calculate_num_stones(1, iterations - 1)
    elif len(str(stone)) % 2 == 0:
        stone = str(stone)
        length = len(stone)

        return calculate_num_stones(int(stone[:length // 2]), iterations - 1) + calculate_num_stones(int(stone[length // 2:]), iterations - 1)
    else:
        return calculate_num_stones(stone * 2024, iterations - 1)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
