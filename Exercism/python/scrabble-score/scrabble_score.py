def score(word):
    word = word.upper()
    result = 0

    for letter in word:
        result += score_letter(letter)

    return result


def score_letter(letter):
    if letter in set("AEIOULNRST"):
        return 1
    elif letter in set("DG"):
        return 2
    elif letter in set("BCMP"):
        return 3
    elif letter in set("FHVWY"):
        return 4
    elif letter in set("K"):
        return 5
    elif letter in set("JX"):
        return 8
    elif letter in set("QZ"):
        return 10
    else:
        raise Exception("Invalid letter: " + letter)
