from collections import defaultdict


def part1():
    line = open("quest06_p1.txt").read().strip()
    result = 0
    num_mentors = 0

    for char in line:
        if char == "A":
            num_mentors += 1
        elif char == "a":
            result += num_mentors

    print(result)


def part2():
    line = open("quest06_p2.txt").read().strip()
    mentors = defaultdict(int)
    result = 0

    for char in line:
        if char.isupper():
            mentors[char] += 1
        else:
            result += mentors[char.upper()]

    print(result)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
