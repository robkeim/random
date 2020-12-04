def part1():
    raw_input = open("day04.txt").read().strip()
    raw_input = raw_input.replace("\n\n", "_").replace("\n", " ")
    passports = [dict(map(lambda s: s.split(":"), line.split(" "))) for line in raw_input.split("_")]

    print(len([passport for passport in passports if len(passport) == 8 or len(passport) == 7 and "cid" not in passport]))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
