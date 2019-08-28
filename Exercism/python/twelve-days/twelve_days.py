ordinal_numbers = {
    1: "first",
    2: "second",
    3: "third",
    4: "fourth",
    5: "fifth",
    6: "sixth",
    7: "seventh",
    8: "eighth",
    9: "ninth",
    10: "tenth",
    11: "eleventh",
    12: "twelfth"
}

gifts = {
    1: "a Partridge in a Pear Tree",
    2: "two Turtle Doves",
    3: "three French Hens",
    4: "four Calling Birds",
    5: "five Gold Rings",
    6: "six Geese-a-Laying",
    7: "seven Swans-a-Swimming",
    8: "eight Maids-a-Milking",
    9: "nine Ladies Dancing",
    10: "ten Lords-a-Leaping",
    11: "eleven Pipers Piping",
    12: "twelve Drummers Drumming"
}


def recite(start_verse, end_verse):
    return [recite_verse(i) for i in range(start_verse, end_verse + 1)]


def recite_verse(verse):
    result = "On the " + ordinal_numbers[verse] + " day of Christmas my true love gave to me: "

    if verse == 1:
        result += "a Partridge in a Pear Tree."
        return result

    for i in range(verse, 1, -1):
        result += gifts[i] + ", "

    result += "and a Partridge in a Pear Tree."

    return result
