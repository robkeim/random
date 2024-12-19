from functools import lru_cache


def part1():
    lines = [line.strip() for line in open("day19.txt").readlines()]

    patterns = [word.strip() for word in lines[0].split(",")]

    @lru_cache(1_000_000)
    def is_match(design, index):
        if index == len(design):
            return True

        for pattern in patterns:
            if design[index:].startswith(pattern) and is_match(design, index + len(pattern)):
                return True

        return False

    ans = 0

    for design in lines[2:]:
        if is_match(design, 0):
            ans += 1

    print(ans)


def part2():
    lines = [line.strip() for line in open("day19.txt").readlines()]

    patterns = [word.strip() for word in lines[0].split(",")]

    @lru_cache(1_000_000)
    def num_match(design, index):
        if index == len(design):
            return 1

        num_matches = 0

        for pattern in patterns:
            if design[index:].startswith(pattern):
                num_matches += num_match(design, index + len(pattern))

        return num_matches

    ans = 0

    for design in lines[2:]:
        ans += num_match(design, 0)

    print(ans)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
