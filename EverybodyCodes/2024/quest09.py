import sys


def part1():
    balls = [int(line.strip()) for line in open("quest09_p1.txt").readlines()]
    num_stamps = [sys.maxsize] * (max(balls) + 1)
    stamps = [1, 3, 5, 10]

    for stamp in stamps:
        num_stamps[stamp] = 1

    for num in range(2, len(num_stamps)):
        for stamp in stamps:
            if stamp <= num:
                num_stamps[num] = min(num_stamps[num], 1 + num_stamps[num - stamp])

    print(sum([num_stamps[ball] for ball in balls]))

def part2():
    balls = [int(line.strip()) for line in open("quest09_p2.txt").readlines()]
    num_stamps = [sys.maxsize] * (max(balls) + 1)
    stamps = [1, 3, 5, 10, 15, 16, 20, 24, 25, 30]

    for stamp in stamps:
        num_stamps[stamp] = 1

    for num in range(2, len(num_stamps)):
        for stamp in stamps:
            if stamp <= num:
                num_stamps[num] = min(num_stamps[num], 1 + num_stamps[num - stamp])

    print(sum([num_stamps[ball] for ball in balls]))


def part3():
    balls = [int(line.strip()) for line in open("quest09_p3.txt").readlines()]
    num_stamps = [sys.maxsize] * (max(balls) // 2 + 103)
    stamps = [1, 3, 5, 10, 15, 16, 20, 24, 25, 30, 37, 38, 49, 50, 74, 75, 100, 101]

    for stamp in stamps:
        num_stamps[stamp] = 1

    for num in range(2, len(num_stamps)):
        for stamp in stamps:
            if stamp <= num:
                num_stamps[num] = min(num_stamps[num], 1 + num_stamps[num - stamp])

    answer = 0

    for ball in balls:
        mid = ball // 2

        best = sys.maxsize

        for i in range(-102, 102):
            one = mid + i
            two = ball - one

            if abs(one - two) > 100:
                continue

            best = min(best, num_stamps[one] + num_stamps[two])

        answer += best

    print(answer)


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
