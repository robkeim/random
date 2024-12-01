from collections import Counter

def part1():
    lines = [line.strip() for line in open("day01.txt").readlines()]
    col1 = []
    col2 = []

    for line in lines:
        one, two = line.split()
        col1.append(int(one))
        col2.append(int(two))

    col1 = sorted(col1)
    col2 = sorted(col2)

    result = 0

    for i in range(len(col1)):
        result += abs(col2[i] - col1[i])

    print(result)

def part2():
    lines = [line.strip() for line in open("day01.txt").readlines()]
    col1 = []
    col2 = []

    for line in lines:
        one, two = line.split()
        col1.append(int(one))
        col2.append(int(two))

    col1 = sorted(col1)
    col2 = Counter(col2)

    similarity_score = 0

    for id in col1:
        similarity_score += id * col2[id]

    print(similarity_score)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
