def part1():
    print(max([sum([int(calories) for calories in elf.split()]) for elf in open("day01.txt").read().split("\n\n")]))


def part2():
    print(sum(sorted([sum([int(calories) for calories in elf.split()]) for elf in open("day01.txt").read().split("\n\n")], reverse=True)[:3]))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
