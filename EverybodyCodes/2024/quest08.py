def part1():
    target = int(open("quest08_p1.txt").read().strip())

    total = 1
    cur_level = 1

    while total < target:
        cur_level += 2
        total += cur_level

    print(cur_level * (total - target))


def part2():
    num_priests = int(open("quest08_p2.txt").read().strip())

    num_priest_acolytes = 1111
    num_available_blocks = 20_240_000
    thickness = 1
    width = 1
    total_blocks = 1

    while total_blocks < num_available_blocks:
        width += 2
        thickness = (thickness * num_priests) % num_priest_acolytes
        blocks = width * thickness
        total_blocks += blocks

    print((total_blocks - num_available_blocks) * width)


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
