import re


def part1():
    raw_input = open("day04.txt").read().strip()
    raw_input = raw_input.replace("\n\n", "_").replace("\n", " ")
    passports = [dict(map(lambda s: s.split(":"), line.split(" "))) for line in raw_input.split("_")]

    print(len([passport for passport in passports if len(passport) == 8 or (len(passport) == 7 and "cid" not in passport)]))


def part2():
    raw_input = open("day04.txt").read().strip()
    raw_input = raw_input.replace("\n\n", "_").replace("\n", " ")
    passports = [dict(map(lambda s: s.split(":"), line.split(" "))) for line in raw_input.split("_")]

    valid_passports = 0

    for passport in passports:
        if not (len(passport) == 8 or (len(passport) == 7 and "cid" not in passport)):
            continue

        # Birth year
        birth_year = int(passport["byr"])

        if birth_year < 1920 or birth_year > 2002:
            continue

        # Issue year
        issue_year = int(passport["iyr"])

        if issue_year < 2010 or issue_year > 2020:
            continue

        # Expiration year
        expiration_year = int(passport["eyr"])

        if expiration_year < 2020 or expiration_year > 2030:
            continue

        # Height
        match = re.fullmatch("([1-9][0-9]*)(cm|in)", passport["hgt"])

        if not match:
            continue

        height = int(match.group(1))
        height_unit = match.group(2)

        if height_unit == "cm":
            if height < 150 or height > 193:
                continue
        elif height_unit == "in":
            if height < 59 or height > 76:
                continue
        else:
            continue

        # Hair color
        if not re.fullmatch("#[0-9a-f]{6}", passport["hcl"]):
            continue

        # Eye color
        if passport["ecl"] not in {"amb", "blu", "brn", "gry", "grn", "hzl", "oth"}:
            continue

        # Passport id
        if not re.fullmatch("[0-9]{9}", passport["pid"]):
            continue

        valid_passports += 1

    print(valid_passports)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
