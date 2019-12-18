import math
import re


def part1():
    print(calculate_ore_needed(1))


def calculate_ore_needed(num_fuel):
    # Build recipes dictionary of chemical -> (num_produced, list of required chemicals)
    lines = open("day14.txt").readlines()
    recipes = {}
    materials = set()
    materials.add("ORE")

    for line in lines:
        match = re.match("(.+) => (.+)", line)
        if match is None:
            raise Exception("Invalid line format: " + line)

        target = extract_reaction(match[2])
        inputs = [extract_reaction(value) for value in match[1].split(",")]

        recipes[target[1]] = (target[0], inputs)
        materials.add(target[1])

    # Build distance dictionary of chemical -> amount of steps away from ORE
    distances = {"ORE": 0}

    while len(distances) < len(materials):
        for material in materials:
            if material in distances:
                continue

            not_found = False
            for _, product in recipes[material][1]:
                if product not in distances:
                    not_found = True
                    break

            if not_found:
                continue

            distance = max(distances[product[1]] for product in recipes[material][1]) + 1
            distances[material] = distance

    remaining = {"FUEL": num_fuel}

    while len(remaining) > 1 or "ORE" not in remaining:
        max_product = max(remaining, key=lambda x: distances[x])
        value = remaining[max_product]
        del remaining[max_product]

        quantity, ingredients = recipes[max_product]
        batches_to_make = math.ceil(value / quantity)

        for ingredient_quantity, ingredient in ingredients:
            if ingredient not in remaining:
                remaining[ingredient] = ingredient_quantity * batches_to_make
            else:
                remaining[ingredient] += ingredient_quantity * batches_to_make

    return remaining["ORE"]


def extract_reaction(value):
    match = re.search("(\d+) ([A-Z]+)", value)
    if match is None:
        raise Exception("Invalid reaction: " + value)

    return int(match[1]), match[2]


def part2():
    low = 1
    high = 1000000000000

    while low < high:
        mid = (low + high) // 2

        ore_needed = calculate_ore_needed(mid)

        if ore_needed < 1000000000000:
            low = mid + 1
        else:
            high = mid - 1

    print(low)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
