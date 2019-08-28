scores_to_letters = {
    1 : 'A, E, I, O, U, L, N, R, S, T',
    2 : 'D, G',
    3 : 'B, C, M, P',
    4 : 'F, H, V, W, Y',
    5 : 'K',
    8 : 'J, X',
    10: 'Q, Z'
}

letters_to_score = {
    letter: score
    for score, letters in scores_to_letters.items()
    for letter in letters.split(", ")
}


def score(word):
    return sum([score_letter(letter) for letter in word.upper()])


def score_letter(letter):
    if letter not in letters_to_score:
        raise Exception("Invalid letter: " + letter)

    return letters_to_score[letter]
