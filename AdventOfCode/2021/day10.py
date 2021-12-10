opening = {"(", "[", "{", "<"}

close_to_open_mapping = {
    ")": "(",
    "]": "[",
    "}": "{",
    ">": "<"
}


def part1():
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
            elif len(stack) > 0 and stack.pop() == close_to_open_mapping[char]:
                pass
            else:
                result += points[char]
                break

    print(result)


def part2():
    points = {
        "(": 1,
        "[": 2,
        "{": 3,
        "<": 4
    }

    lines = [line.strip() for line in open("day10.txt").readlines()]

    scores = []

    for line in lines:
        stack = []
        corrupted = False

        for char in line:
            if char in opening:
                stack.append(char)
            elif len(stack) > 0 and stack.pop() == close_to_open_mapping[char]:
                pass
            else:
                corrupted = True
                break

        if not corrupted:
            score = 0

            for element in stack[::-1]:
                score *= 5
                score += points[element]

            scores.append(score)

    print(sorted(scores)[len(scores) // 2])


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
