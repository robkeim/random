from collections import defaultdict


def part1():
    lines = [line.strip() for line in open("day04.txt").readlines()]

    numbers_drawn = lines[0].split(",")
    lines = lines[2:]

    boards = []

    while len(lines) > 0:
        boards.append([line.replace("  ", " ").split(" ") for line in lines[:5]])
        lines = lines[6:]

    while len(numbers_drawn):
        number = numbers_drawn[0]
        numbers_drawn = numbers_drawn[1:]

        for board in boards:
            for i in range(5):
                for j in range(5):
                    if board[i][j] == number:
                        board[i][j] = "X"

                        if has_won(board):
                            print(count_score(board) * int(number))
                            return

    raise Exception("No more numbers available, but no winner found yet")


def has_won(board):
    for i in range(5):
        row = True
        col = True
        for j in range(5):
            if board[i][j] != "X":
                row = False

            if board[j][i] != "X":
                col = False

        if row or col:
            return True

    return False


def count_score(board):
    score = 0

    for i in range(5):
        for j in range(5):
            if board[i][j] != "X":
                score += int(board[i][j])

    return score


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
