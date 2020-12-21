import re
from collections import Counter


def part1():
    lines = [line.strip() for line in open("day21.txt").readlines()]
    allergen_to_ingredients = dict()
    ingredients_counter = Counter()

    for line in lines:
        match = re.fullmatch("(.+) \(contains (.+)\)", line)
        assert match, "Invalid line format: " + line

        ingredients = set(match.group(1).split())

        ingredients_counter.update(ingredients)

        for allergen in match.group(2).split(", "):
            if allergen not in allergen_to_ingredients:
                allergen_to_ingredients[allergen] = ingredients
            else:
                allergen_to_ingredients[allergen] = allergen_to_ingredients[allergen].intersection(ingredients)

    allergic_ingredients = set()

    for allergen in allergen_to_ingredients:
        for ingredient in allergen_to_ingredients[allergen]:
            allergic_ingredients.add(ingredient)

    result = 0

    for ingredient in ingredients_counter:
        if ingredient not in allergic_ingredients:
            result += ingredients_counter[ingredient]

    print(result)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
