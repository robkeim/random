class Allergies(object):

    def __init__(self, score):
        self.score = score

    def allergic_to(self, item):
        item_score = {
            "eggs": 1,
            "peanuts": 2,
            "shellfish": 4,
            "strawberries": 8,
            "tomatoes": 16,
            "chocolate": 32,
            "pollen": 64,
            "cats": 128
        }

        return self.score & item_score[item] != 0

    @property
    def lst(self):
        allergens = [
            (1, "eggs"),
            (2, "peanuts"),
            (4, "shellfish"),
            (8, "strawberries"),
            (16, "tomatoes"),
            (32, "chocolate"),
            (64, "pollen"),
            (128, "cats")
        ]

        result = []

        for value, allergen in allergens:
            if self.score & value != 0:
                result.append(allergen)

        return result
