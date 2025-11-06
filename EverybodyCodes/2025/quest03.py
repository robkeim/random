from collections import Counter


def part1():
    print(sum(set([int(num) for num in open("quest03_p1.txt").read().strip().split(",")])))


def part2():
    nums = sorted([int(num) for num in open("quest03_p2.txt").read().strip().split(",")])
    result = 0
    num_found = 0
    prev = None

    for num in nums:
        if num_found == 20:
            print(result)
            return

        if num != prev:
            result += num
            num_found += 1
            prev = num

    assert False, "No solution found"


def part3():
    counter = Counter([int(num) for num in open("quest03_p3.txt").read().strip().split(",")])
    print(max(counter.values()))


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
