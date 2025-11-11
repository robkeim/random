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
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
