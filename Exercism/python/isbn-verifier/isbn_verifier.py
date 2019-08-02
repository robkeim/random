import re


def is_valid(isbn):
    isbn = re.sub("[^0-9X]", "", isbn)

    if not re.fullmatch("[0-9]{9}[0-9X]", isbn):
        return False

    return sum(map(lambda i: (10 - i) * (10 if isbn[i] == "X" else int(isbn[i])), range(0, 10))) % 11 == 0
