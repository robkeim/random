from itertools import permutations


def part1():
    lines = [line.split(" | ")[1].strip().split(" ") for line in open("day08.txt").readlines()]
    print(sum([len([word for word in line if len(word) in {2, 3, 4, 7}]) for line in lines]))


combinations = {
    "ABCEFG": 0,
    "CF": 1,
    "ACDEG": 2,
    "ACDFG": 3,
    "BCDF": 4,
    "ABDFG": 5,
    "ABDEFG": 6,
    "ACF": 7,
    "ABCDEFG": 8,
    "ABCDFG": 9
}


def part2():
    result = 0

    for line in open("day08.txt").readlines():
        scrambled_inputs, scrambled_numbers = line.split(" | ")

        for permutation in permutations("ABCDEFG"):
            output = scrambled_inputs.replace("a", permutation[0]).replace("b", permutation[1])\
                .replace("c", permutation[2]).replace("d", permutation[3]).replace("e", permutation[4])\
                .replace("f", permutation[5]).replace("g", permutation[6])

            output = ["".join(sorted(digit)) for digit in output.split(" ")]

            if not all(combination in output for combination in combinations):
                continue

            scrambled_numbers = ["".join(sorted(digit)) for digit in scrambled_numbers.strip().split(" ")]

            number = ""

            for scrambled_number in scrambled_numbers:
                digit = ""

                for scrambled_digit in scrambled_number:
                    digit += permutation[ord(scrambled_digit) - ord("a")]

                number += str(combinations["".join(sorted(digit))])

            result += int(number)
            break

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
