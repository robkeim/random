def test():
    assert decimal_to_snafu(1) == "1"
    assert decimal_to_snafu(2) == "2"
    assert decimal_to_snafu(3) == "1="
    assert decimal_to_snafu(4) == "1-"
    assert decimal_to_snafu(5) == "10"
    assert decimal_to_snafu(6) == "11"
    assert decimal_to_snafu(7) == "12"
    assert decimal_to_snafu(8) == "2="
    assert decimal_to_snafu(9) == "2-"
    assert decimal_to_snafu(10) == "20"
    assert decimal_to_snafu(15) == "1=0"
    assert decimal_to_snafu(20) == "1-0"
    assert decimal_to_snafu(2022) == "1=11-2"
    assert decimal_to_snafu(12345) == "1-0---0"
    assert decimal_to_snafu(314159265) == "1121-1110-1=0"

    assert snafu_to_decimal("1=-0-2") == 1747
    assert snafu_to_decimal("12111") == 906
    assert snafu_to_decimal("2=0=") == 198
    assert snafu_to_decimal("21") == 11
    assert snafu_to_decimal("2=01") == 201
    assert snafu_to_decimal("111") == 31
    assert snafu_to_decimal("20012") == 1257
    assert snafu_to_decimal("112") == 32
    assert snafu_to_decimal("1=-1=") == 353
    assert snafu_to_decimal("1-12") == 107
    assert snafu_to_decimal("12") == 7
    assert snafu_to_decimal("1=") == 3
    assert snafu_to_decimal("122") == 37


def part1():
    lines = [line.strip() for line in open("day25.txt").readlines()]
    print(decimal_to_snafu(sum([snafu_to_decimal(line) for line in lines])))

def decimal_to_snafu(number):
    base5 = convert_to_base_five(number)[::-1]
    result = ""
    carry = False

    for digit in base5:
        if carry:
            digit += 1
            carry = False

        if digit < 3:
            result += str(digit)
        elif digit == 3:
            result += "="
            carry = True
        elif digit == 4:
            result += "-"
            carry = True
        elif digit == 5:
            result += "0"
            carry = True
        else:
            assert False, f"Invalid digit: {digit}"

    if carry:
        result += "1"

    return result[::-1]


def convert_to_base_five(number):
    digits = []

    while number:
        digits.append(int(number % 5))
        number //= 5

    return digits[::-1]


def snafu_to_decimal(number):
    result = 0

    for i, digit in enumerate(number[::-1]):
        place = 5 ** i

        if digit == "=":
            multiplier = -2
        elif digit == "-":
            multiplier = -1
        else:
            multiplier = int(digit)

        result += place * multiplier

    return result


def part2():
    pass


def main():
    # test()
    part1()
    part2()


if __name__ == "__main__":
    main()
