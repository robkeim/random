def part1():
    lines = [line.strip() for line in open("quest02_p1.txt").readlines()]
    words = lines[0].split(":")[1].split(",")
    text = lines[2]

    print(sum([text.count(word) for word in words]))


def part2():
    lines = [line.strip() for line in open("quest02_p2.txt").readlines()]
    words = lines[0].split(":")[1].split(",")
    words += [word[::-1] for word in words]

    answer = 0

    for line in lines[2:]:
        indexes = set()

        for i in range(len(line)):
            for word in words:
                if line[i:].startswith(word):
                    for j in range(len(word)):
                        indexes.add(i + j)

        answer += len(indexes)

    print(answer)


def part3():
    lines = [line.strip() for line in open("quest02_p3.txt").readlines()]
    words = lines[0].split(":")[1].split(",")
    words += [word[::-1] for word in words]
    grid = lines[2:]
    num_r = len(grid)
    num_c = len(grid[0])

    indexes = set()

    for r, line in enumerate(grid):
        for i in range(len(line)):
            for word in words:
                word_indexes = set()
                match = True

                for j in range(len(word)):
                    c = (i + j) % num_c
                    if word[j] == line[c]:
                        word_indexes.add((r, c))
                    else:
                        match = False
                        break

                if match:
                    indexes = indexes.union(word_indexes)

    for c in range(num_c):
        for start_r in range(num_r):
            for word in words:
                word_indexes = set()
                match = True

                for i in range(len(word)):
                    r = start_r + i
                    if r < num_r and word[i] == grid[r][c]:
                        word_indexes.add((r, c))
                    else:
                        match = False
                        break

                if match:
                    indexes = indexes.union(word_indexes)

    print(len(indexes))


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
