def part1():
    dna_sequences = [line.strip()[2:] for line in open("quest09_p1.txt").readlines()]
    child = None

    for child_index in range(len(dna_sequences)):
        if child is not None:
            break

        for parent1_index in range(len(dna_sequences)):
            if parent1_index == child_index:
                continue

            for parent2_index in range(parent1_index + 1, len(dna_sequences)):
                if parent2_index == child_index:
                    continue

                valid = True

                for i in range(len(dna_sequences)):
                    if dna_sequences[child_index][i] != dna_sequences[parent1_index][i] and dna_sequences[child_index][i] != dna_sequences[parent2_index][i]:
                        valid = False
                        break

                if valid:
                    child = child_index

    assert child, "No child found"

    result = 1

    for parent_index in range(len(dna_sequences)):
        if parent_index == child:
            continue

        similarity = 0

        for i in range(len(dna_sequences[0])):
            if dna_sequences[child][i] == dna_sequences[parent_index][i]:
                similarity += 1

        result *= similarity

    print(result)


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
