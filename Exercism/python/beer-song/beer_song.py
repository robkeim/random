def recite(start, take=1):
    result = []

    while take > 0:
        result += verse(start)

        start -= 1
        if start < 0:
            start = 99
        take -= 1

        if take > 0:
            result.append("")

    return result


def verse(number):
    if number == 0:
        return [
            "No more bottles of beer on the wall, no more bottles of beer.",
            "Go to the store and buy some more, 99 bottles of beer on the wall."
        ]
    elif number == 1:
        return [
            "1 bottle of beer on the wall, 1 bottle of beer.",
            "Take it down and pass it around, no more bottles of beer on the wall."
        ]
    elif number == 2:
        return [
            "2 bottles of beer on the wall, 2 bottles of beer.",
            "Take one down and pass it around, 1 bottle of beer on the wall."
        ]
    else:
        return [
            "{} bottles of beer on the wall, {} bottles of beer.".format(number, number),
            "Take one down and pass it around, {} bottles of beer on the wall.".format(number - 1)
        ]