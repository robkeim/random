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
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
