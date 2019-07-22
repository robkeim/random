import re

from functools import reduce


def decode(string):
    groups = [decode_single_match(m) for m in re.compile(r"(\d+)?(.)").finditer(string)]
    return reduce(lambda x, y: x + y, groups, "")


def decode_single_match(match):
    if not match.group(1):
        return match.group(2)

    return match.group(2) * int(match.group(1))


def encode(string):
    groups = [m.group() for m in re.compile(r"(.)\1*").finditer(string)]
    return reduce(lambda x, y: x + encode_single_value(y), groups, "")


def encode_single_value(string):
    if len(string) == 1:
        return string

    return str(len(string)) + string[0]
