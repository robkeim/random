import sys


def part1():
    width = 25
    height = 6
    image = [d for d in open("day08.txt").read()]

    min_zeros = sys.maxsize
    result = 0

    for layer in chunks(image, width * height):
        num_zeros = len([d for d in layer if d == "0"])

        if num_zeros < min_zeros:
            min_zeros = num_zeros
            result = len([d for d in layer if d == "1"]) * len([d for d in layer if d == "2"])

    print(result)


def part2():
    pass


def chunks(elements, size):
    return [elements[i * size:(i + 1) * size] for i in range((len(elements) + size - 1) // size)]


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
