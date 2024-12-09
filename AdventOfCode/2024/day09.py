def part1():
    values = [int(value) for value in open("day09.txt").read().strip()]
    left_pointer = 0
    right_pointer = len(values) - 2 if len(values) % 2 == 0 else len(values) - 1
    remaining_right = values[right_pointer]
    checksum = 0
    index = 0

    while left_pointer < right_pointer:
        if left_pointer % 2 == 0:
            value = left_pointer // 2
            count = values[left_pointer]
            for i in range(count):
                checksum += (index + i) * value

            index += count
        else:
            needed = values[left_pointer]

            while needed > 0 and left_pointer < right_pointer:
                if remaining_right >= needed:
                    value = right_pointer // 2
                    for i in range(needed):
                        checksum += (index + i) * value

                    index += needed
                    remaining_right -= needed
                    break

                value = right_pointer // 2
                for i in range(remaining_right):
                    checksum += (index + i) * value

                index += remaining_right
                needed -= remaining_right

                right_pointer -= 2
                remaining_right = values[right_pointer]

        left_pointer += 1

    value = right_pointer // 2
    for i in range(remaining_right):
        checksum += (index + i) * value

    index += remaining_right

    print(checksum)


def part2():
    values = [int(value) for value in open("day09.txt").read().strip()]
    intervals = []
    start = 0

    for i, value in enumerate(values):
        if i % 2 == 0:
            intervals.append((start, start + value - 1, i // 2))
            start += value
        else:
            start += value

    right_index = len(intervals) - 1

    while right_index > 0:
        replace_start, replace_end, replace_value = intervals[right_index]
        needed = replace_end - replace_start + 1
        found = False

        for i in range(right_index):
            cur_start, cur_end, _ = intervals[i]
            next_start, next_end, _ = intervals[i + 1]
            available = next_start - cur_end - 1

            if needed <= available:
                one = intervals[:i + 1]
                new = [(cur_end + 1, cur_end + needed, replace_value)]
                two = intervals[i + 1:right_index]
                three = intervals[right_index + 1:]
                intervals = one + new + two + three
                found = True
                break

        if not found:
            right_index -= 1

    checksum = 0

    for start, end, value in intervals:
        checksum += ((end + start) * (end - start + 1) // 2) * value

    print(checksum)


def print_intervals(intervals):
    index = 0
    string = ""

    for start, end, value in intervals:
        while index < start:
            string += "."
            index += 1

        for _ in range(end - start + 1):
            string += str(value)
            index += 1

    print(string)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()