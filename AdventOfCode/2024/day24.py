def part1():
    lines = [line.strip() for line in open("day24.txt").readlines()]
    values = dict()
    rules = dict()

    for line in lines:
        if ":" in line:
            wire, value = line.split(":")
            values[wire] = int(value)
        elif "->" in line:
            wire1, op, wire2, _, output_wire = line.split()
            rules[output_wire] = (wire1, op, wire2)

    max_z = 0
    missing = True

    while missing:
        missing = False

        for output_wire in rules:
            if output_wire in values:
                continue

            wire1, op, wire2 = rules[output_wire]

            if wire1 not in values or wire2 not in values:
                missing = True
                continue

            if op == "AND":
                values[output_wire] = values[wire1] & values[wire2]
            elif op == "OR":
                values[output_wire] = values[wire1] | values[wire2]
            elif op == "XOR":
                values[output_wire] = values[wire1] ^ values[wire2]
            else:
                assert False, f"Unknown op: {op}"

            if output_wire[0] == "z":
                max_z = max(max_z, int(output_wire[1:]))

    result = ""

    for i in range(max_z + 1):
        i_str = str(i) if i >= 10 else "0" + str(i)
        result += str(values["z" + i_str])

    print(result[::-1])
    print(int(result[::-1], 2))


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
