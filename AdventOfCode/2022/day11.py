from collections import defaultdict


def part1():
    items, operations, destinations = get_input(False)
    inspected = defaultdict(int)

    for cur_round in range(20):
        for i in range(len(items)):
            for item in items[i]:
                inspected[i] += 1
                new_value = operations[i](item)
                items[destinations[i](new_value)].append(new_value)

            items[i] = []

    values = sorted(inspected.values())
    print(values[-1] * values[-2])


def get_input(example):
    if example:
        items = [
            [79, 98],
            [54, 65, 75, 74],
            [79, 60, 97],
            [74]
        ]

        operations = [
            lambda old: (old * 19) // 3,
            lambda old: (old + 6) // 3,
            lambda old: (old * old) // 3,
            lambda old: (old + 3) // 3
        ]

        destinations = [
            lambda x: 2 if x % 23 == 0 else 3,
            lambda x: 2 if x % 19 == 0 else 0,
            lambda x: 1 if x % 13 == 0 else 3,
            lambda x: 0 if x % 17 == 0 else 1
        ]
    else:
        items = [
            [92, 73, 86, 83, 65, 51, 55, 93],
            [99, 67, 62, 61, 59, 98],
            [81, 89, 56, 61, 99],
            [97, 74, 68],
            [78, 73],
            [50],
            [95, 88, 53, 75],
            [50, 77, 98, 85, 94, 56, 89]
        ]

        operations = [
            lambda old: (old * 5) // 3,
            lambda old: (old * old) // 3,
            lambda old: (old * 7) // 3,
            lambda old: (old + 1) // 3,
            lambda old: (old + 3) // 3,
            lambda old: (old + 5) // 3,
            lambda old: (old + 8) // 3,
            lambda old: (old + 2) // 3,
        ]

        destinations = [
            lambda x: 3 if x % 11 == 0 else 4,
            lambda x: 6 if x % 2 == 0 else 7,
            lambda x: 1 if x % 5 == 0 else 5,
            lambda x: 2 if x % 17 == 0 else 5,
            lambda x: 2 if x % 19 == 0 else 3,
            lambda x: 1 if x % 7 == 0 else 6,
            lambda x: 0 if x % 3 == 0 else 7,
            lambda x: 4 if x % 13 == 0 else 0,
        ]

    return items, operations, destinations


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
