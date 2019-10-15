import re

from collections import Counter


def count_words(sentence):
    return Counter([word.strip("'") for word in re.sub("[^a-zA-Z0-9']+", " ", sentence).lower().split()])
