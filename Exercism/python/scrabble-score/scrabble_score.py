def score(word):
    word = word.upper()
    result = 0

    for letter in word:
        result += score_letter(letter)

    return result


def score_letter(letter):
    if (letter == "A" or letter == "E" or letter == "I" or letter == "O" or letter == "U" or letter == "L"
            or letter == "N" or letter == "R" or letter == "S" or letter == "T"):
        return 1
    elif letter == "D" or letter == "G":
        return 2
    elif letter == "B" or letter == "C" or letter == "M" or letter =="P":
        return 3
    elif letter == "F" or letter == "H" or letter == "V" or letter == "W" or letter == "Y":
        return 4
    elif letter == "K":
        return 5
    elif letter == "J" or letter == "X":
        return 8
    elif letter == "Q" or letter == "Z":
        return 10
    else:
        raise Exception("Invalid letter: " + letter)
