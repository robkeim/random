def part1():
    opening = {"(", "[", "{", "<"}

    mapping = {
        ")": "(",
        "]": "[",
        "}": "{",
        ">": "<"
    }

    points = {
        ")": 3,
        "]": 57,
        "}": 1197,
        ">": 25137
    }

    lines = [line.strip() for line in open("day10.txt").readlines()]

    result = 0

    for line in lines:
        stack = []

        for char in line:
            if char in opening:
                stack.append(char)
            elif len(stack) > 0 and stack.pop() == mapping[char]:
                pass
            else:
                result += points[char]
                break

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
