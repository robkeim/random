def part1():
    values = [int(value) for value in open("day09.txt").read().strip()]
    left_pointer = 0
    right_pointer = len(values) - 2 if len(values) % 2 == 0 else len(values) - 1
    remaining_right = values[right_pointer]
    checksum = 0
    index = 0

    while left_pointer < right_pointer:
        if left_pointer % 2 == 0:
            value = (left_pointer // 2)
            count = values[left_pointer]
            for i in range(count):
                checksum += (index + i) * value

            index += count
        else:
            needed = values[left_pointer]

            while needed > 0 and left_pointer < right_pointer:
                if remaining_right >= needed:
                    value = (right_pointer // 2)
                    for i in range(needed):
                        checksum += (index + i) * value

                    index += needed
                    remaining_right -= needed
                    break

                value = (right_pointer // 2)
                for i in range(remaining_right):
                    checksum += (index + i) * value

                index += remaining_right
                needed -= remaining_right

                right_pointer -= 2
                remaining_right = values[right_pointer]

        left_pointer += 1

    value = (right_pointer // 2)
    for i in range(remaining_right):
        checksum += (index + i) * value

    index += remaining_right

    print(checksum)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
