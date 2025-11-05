def part1():
    raw_input = open("quest02_p1.txt").read().strip()
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
    return [x1 // x2 if x1 > 0 else -(-x1 // x2), y1 // y2 if y1 > 0 else -(-y1 // x2)]


def part2():
    raw_input = open("quest02_p2.txt").read().strip()
    left, right = raw_input[2:].strip("[]").split(",")
    A = [int(left), int(right)]
    num_engravings = 0

    for start_y in range(101):
        row = ""

        for start_x in range(101):
            result = [0, 0]
            point_under_examination = [A[0] + 10 * start_x, A[1] + 10 * start_y]

            should_engrave = True

            for _ in range(100):
                result = multiply(result, result)
                result = divide(result, [100000, 100000])
                result = add(result, point_under_examination)

                if not valid_range_to_engrave(result):
                    should_engrave = False
                    break

            if should_engrave:
                num_engravings += 1
                row += "x"
            else:
                row += "."

        #print(row)

    print(num_engravings)


def valid_range_to_engrave(num):
    x, y = num

    return -1000000 <= x <= 1000000 and -1000000 <= y <= 1000000


def part3():
    pass


def main():
    part1()
    part2()
    part3()


if __name__ == "__main__":
    main()
