def part1():
    target = int(open("quest08_p1.txt").read().strip())

    total = 1
    cur_level = 1

    while total < target:
        cur_level += 2
        total += cur_level

    print(cur_level * (total - target))


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
