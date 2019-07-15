def recite(start_verse, end_verse):
    result = []

    for i in range(start_verse, end_verse + 1):
        result.append(recite_verse(i))

    return result


def recite_verse(verse):
    result = "On the " + ordinal_number(verse) + " day of Christmas my true love gave to me: "

    if verse == 1:
        result += "a Partridge in a Pear Tree."
        return result

    for i in range(verse, 1, -1):
        result += gift(i) + ", "

    result += "and a Partridge in a Pear Tree."

    return result


def ordinal_number(number):
    if number == 1:
        return "first"
    elif number == 2:
        return "second"
    elif number == 3:
        return "third"
    elif number == 4:
        return "fourth"
    elif number == 5:
        return "fifth"
    elif number == 6:
        return "sixth"
    elif number == 7:
        return "seventh"
    elif number == 8:
        return "eighth"
    elif number == 9:
        return "ninth"
    elif number == 10:
        return "tenth"
    elif number == 11:
        return "eleventh"
    elif number == 12:
        return "twelfth"
    else:
        raise Exception("Value out of range")


def gift(number):
    if number == 1:
        return "a Partridge in a Pear Tree"
    elif number == 2:
        return "two Turtle Doves"
    elif number == 3:
        return "three French Hens"
    elif number == 4:
        return "four Calling Birds"
    elif number == 5:
        return "five Gold Rings"
    elif number == 6:
        return "six Geese-a-Laying"
    elif number == 7:
        return "seven Swans-a-Swimming"
    elif number == 8:
        return "eight Maids-a-Milking"
    elif number == 9:
        return "nine Ladies Dancing"
    elif number == 10:
        return "ten Lords-a-Leaping"
    elif number == 11:
        return "eleven Pipers Piping"
    elif number == 12:
        return "twelve Drummers Drumming"
    else:
        raise Exception("Value out of range")