def part1():
    value_of_valid_equations(False)

def part2():
    value_of_valid_equations(True)


def value_of_valid_equations(can_concatenate):
    lines = [line.strip() for line in open("day07.txt").readlines()]
    result = 0

    for line in lines:
        target, values = line.split(":")
        target = int(target)
        values = [int(value) for value in values.split()]

        to_process = [(values[0], 1)]

        while len(to_process) > 0:
            total, index = to_process.pop()

            if index == len(values):
                if total == target:
                    result += target
                    break

                continue

            if total > target:
                continue

            to_process.append((total + values[index], index + 1))
            to_process.append((total * values[index], index + 1))

            if can_concatenate:
                to_process.append((int(str(total) + str(values[index])), index + 1))

    print(result)

def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
