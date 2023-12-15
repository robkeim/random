def part1():
    items = open("day15.txt").read().strip().split(",")
    print(sum([get_hash(item) for item in items]))


def get_hash(item):
    result = 0

    for character in item:
        result = ((result + ord(character)) * 17) % 256

    return result


def part2():
    items = open("day15.txt").read().strip().split(",")
    boxes = [[] for _ in range(256)]

    for item in items:
        if item.endswith("-"):
            value = item[:-1]
            box = get_hash(value)
            boxes[box] = [item for item in boxes[box] if item[0] != value]
        else:
            index = item.index("=")
            value = item[:index]
            box = get_hash(value)
            found = False

            for i in range(len(boxes[box])):
                if boxes[box][i][0] == value:
                    boxes[box][i] = (value, int(item[index + 1:]))
                    found = True

            if not found:
                boxes[box].append((value, int(item[index + 1:])))

    result = 0

    for i in range(len(boxes)):
        box = boxes[i]

        for j in range(len(box)):
            focal_length = box[j][1]
            result += (i + 1) * (j + 1) * focal_length

    print(result)


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
