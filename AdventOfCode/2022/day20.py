def part1():
    start = [int(line.strip()) for line in open("day20.txt").readlines()]
    print(mix(start)[1])


def mix(arr2):
    num_elements = len(arr2)
    start = [(index, value) for (index, value) in enumerate(arr2)]
    arr = start[::]

    for (index, num) in start:
        start_index = arr.index((index, num))
        shift = num

        if shift < 0:
            offset = (shift % (num_elements - 1)) - num_elements
            shift = num_elements + offset #- 1
            assert 0 <= shift < num_elements
        elif shift > 0:
            shift %= num_elements - 1

        end_index = start_index + shift
        if end_index >= num_elements:
            end_index %= num_elements

        if start_index < end_index:
            arr.insert(end_index + 1, (index, num))
            del arr[start_index]
        elif start_index > end_index:
            del arr[start_index]
            arr.insert(end_index + 1, (index, num))
        else:
            pass  # Nothing to do

    zero_index = None
    for (index, (_, num)) in enumerate(arr):
        if num == 0:
            zero_index = index
            break

    if zero_index == None:
        raise Exception(f"Zero not found in arr: {arr}")

    total = 0

    for i in range(1, 4):
        total += arr[(zero_index + (i * 1000)) % num_elements][1]

    return arr, total


def sort_arr(arr):
    for (index, (_, num)) in enumerate(arr):
        if num == 0:
            result = arr[index:] + arr[:index]
            return [value for (_, value) in result]

    raise Exception(f"Zero not found in array: {arr}")


def part2():
    pass


def test(in_arr, expected_arr, expected_sum):
    actual_arr, actual_sum = mix(in_arr)
    actual_arr = sort_arr(actual_arr)

    if expected_arr != actual_arr:
        raise Exception(f"Unexpected final arr: {actual_arr}")

    if expected_sum != actual_sum:
        raise Exception(f"Unexpected sum: {actual_sum}")


def main():
    test([1, 2, -3, 3, -2, 0, 4], [0, 3, -2, 1, 2, -3, 4], 3)
    test([12, 0, 1, 2, 3, 4], [0, 3, 12, 4, 1, 2], 13)
    test([-12, 0, -4, -3, -2, -1], [0, -4, -2, -12, -1, -3], -3)
    part1()
    part2()


if __name__ == "__main__":
    main()
