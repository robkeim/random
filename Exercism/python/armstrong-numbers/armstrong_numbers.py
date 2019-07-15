def is_armstrong_number(number):
    number_str = str(number)
    length = len(number_str)

    result = 0

    for digit in number_str:
        result += int(digit) ** length

    return result == number
