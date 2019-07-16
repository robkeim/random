import re

from functools import reduce


def abbreviate(words):
    split_words = words.upper().replace("-", " ").split()
    words_only_letters = map(keep_letters, split_words)
    return reduce(lambda result, word: result + word[0], words_only_letters, "")


def keep_letters(word):
    return re.sub('[^a-zA-Z]+', '', word)