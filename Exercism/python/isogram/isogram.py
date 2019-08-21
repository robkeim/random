import re


def is_isogram(string):
    string = re.sub("[- ]+", "", string.lower())

    return len(string) == len(set(string))
