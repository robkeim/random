import more_itertools

from functools import reduce


def slices(series, length):
    if length <= 0 or length > len(series):
        raise ValueError("Invalid input")

    return list(map(lambda l: reduce(lambda x, y: x + y, l), more_itertools.windowed(series, length)))
