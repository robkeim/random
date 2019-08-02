def square(number):
    if number <= 0 or number > 64:
        raise ValueError("Square out of range")

    return 2 ** (number - 1)


def total(number):
    if number <= 0 or number > 64:
        raise ValueError("Square out of range")

    return sum([square(i + 1) for i in range(0, number)])
