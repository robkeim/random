from functools import reduce

"""
This exercise stub and the test suite contain several enumerated constants.

Since Python 2 does not have the enum module, the idiomatic way to write
enumerated constants has traditionally been a NAME assigned to an arbitrary,
but unique value. An integer is traditionally used because it’s memory
efficient.
It is a common practice to export both constants and functions that work with
those constants (ex. the constants in the os, subprocess and re modules).

You can learn more here: https://en.wikipedia.org/wiki/Enumerated_type
"""

# Score categories.
# Change the values as you see fit.
YACHT = 0
ONES = 1
TWOS = 2
THREES = 3
FOURS = 4
FIVES = 5
SIXES = 6
FULL_HOUSE = 7
FOUR_OF_A_KIND = 8
LITTLE_STRAIGHT = 9
BIG_STRAIGHT = 10
CHOICE = 11


def score(dice, category):
    if category == YACHT:
        return score_yacht(dice)
    elif category == ONES:
        return score_number(dice, 1)
    elif category == TWOS:
        return score_number(dice, 2)
    elif category == THREES:
        return score_number(dice, 3)
    elif category == FOURS:
        return score_number(dice, 4)
    elif category == FIVES:
        return score_number(dice, 5)
    elif category == SIXES:
        return score_number(dice, 6)
    elif category == FULL_HOUSE:
        return score_full_house(dice)
    elif category == FOUR_OF_A_KIND:
        return score_four_of_a_kind(dice)
    elif category == LITTLE_STRAIGHT:
        return score_little_straight(dice)
    elif category == BIG_STRAIGHT:
        return score_big_straight(dice)
    elif category == CHOICE:
        return score_choice(dice)
    else:
        raise Exception("Invalid category")


def score_yacht(dice):
    if len(set(dice)) == 1:
        return 50
    else:
        return 0


def score_number(dice, number):
    return len(list(filter(lambda x: x == number, dice))) * number


def score_full_house(dice):
    if len(set(dice)) == 2 and len(list(filter(lambda x: x == dice[0], dice))) >= 2:
        return reduce(lambda x, y: x + y, dice)
    else:
        return 0


def score_four_of_a_kind(dice):
    for i in range(1, 7):
        if len(list(filter(lambda x: x == i, dice))) >= 4:
            return 4 * i

    return 0


def score_little_straight(dice):
    dice = set(dice)

    if 1 in dice and 2 in dice and 3 in dice and 4 in dice and 5 in dice:
        return 30
    else:
        return 0


def score_big_straight(dice):
    dice = set(dice)

    if 2 in dice and 3 in dice and 4 in dice and 5 in dice and 6 in dice:
        return 30
    else:
        return 0


def score_choice(dice):
    return reduce(lambda x, y: x + y, dice)
