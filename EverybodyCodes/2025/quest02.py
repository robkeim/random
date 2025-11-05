import os


def part1():
    base_dir = os.path.dirname(__file__)
    raw_input = open(os.path.join(base_dir, "quest02_p1.txt")).read().strip()
    left, right = raw_input[2:].strip("[]").split(",")
    A = [int(left), int(right)]

    result = [0, 0]

    for _ in range(3):
        result = multiply(result, result)
        result = divide(result, [10, 10])
        result = add(result, A)

    print(str(result).replace(" ", ""))

    

def add(x, y):
    x1, y1 = x
    x2, y2 = y
    return [x1 + x2, y1 + y2]


def multiply(x, y):
    x1, y1 = x
    x2, y2 = y
    return [x1 * x2 - y1 * y2, x1 * y2 + y1 * x2]


def divide(x, y):
    x1, y1 = x
    x2, y2 = y
    return [x1 // x2, y1 // y2]


def part2():
    pass


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
