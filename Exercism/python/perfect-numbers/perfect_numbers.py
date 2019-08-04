import math


def classify(number):
    if number <= 0:
        raise ValueError("Number must be positive")

    factor_sum = sum(filter(lambda x: number % x == 0, range(1, math.floor(number / 2) + 1)))

    if factor_sum > number:
        return "abundant"
    elif factor_sum < number:
        return "deficient"
    else:
        return "perfect"
