import random
from functools import reduce
from math import floor


class Character:
    def __init__(self):
        self.strength = self.ability()
        self.dexterity = self.ability()
        self.constitution = self.ability()
        self.intelligence = self.ability()
        self.wisdom = self.ability()
        self.charisma = self.ability()
        self.hitpoints = 10 + modifier(self.constitution)

    def ability(self):
        dice = [random.randint(1, 7), random.randint(1, 7), random.randint(1, 7), random.randint(1, 7)]
        dice.sort()
        dice = dice[0:3]
        return reduce(lambda x, y: x + y, dice)


def modifier(constitution):
    return floor((constitution - 10) / 2)