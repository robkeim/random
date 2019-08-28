import re


def abbreviate(words):
    split_words = words.upper().replace("-", " ").split()
    words_only_letters = map(keep_letters, split_words)
    return "".join(word[0] for word in words_only_letters)


def keep_letters(word):
    return re.sub('[^a-zA-Z]+', '', word)