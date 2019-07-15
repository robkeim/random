import re

from collections import defaultdict


def count_words(sentence):
    result = defaultdict(int)

    words = re.sub("[^a-zA-Z0-9']+", " ", sentence).lower().split()

    for word in words:
        result[word.strip("'")] += 1

    return result
