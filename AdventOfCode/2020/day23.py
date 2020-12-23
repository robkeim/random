def part1():
    cups = [int(cup) for cup in open("day23.txt").read().strip()]

    for _ in range(100):
        cur_cup_val = cups[0]
        pickup = cups[1:4]
        remaining = set(cups[4:])

        next_value = max(remaining)

        for i in range(cur_cup_val - 1, 0, -1):
            if i in remaining:
                next_value = i
                break

        index = cups.index(next_value)
        next_cups = [cur_cup_val] + cups[4:index + 1] + pickup + cups[index + 1:]

        cups = next_cups[1:] + [cups[0]]

    index = cups.index(1)
    result = [str(value) for value in cups[index + 1:] + cups[:index]]

    print("".join(result))


def part2():
    cups = [int(cup) for cup in open("day23.txt").read().strip()]

    nodes = dict()

    next_value = max(cups) + 1
    while len(cups) < 1000000:
        cups.append(next_value)
        next_value += 1

    len_cups = len(cups)

    for cup in cups:
        nodes[cup] = Node(cup)

    for i in range(len_cups - 1):
        nodes[cups[i]].next_ = nodes[cups[i + 1]]

    nodes[cups[len_cups - 1]].next_ = nodes[cups[0]]

    cur_node = nodes[cups[0]]

    for _ in range(10000000):
        cur_cup_val = cur_node.val
        pickup_start = cur_node.next_
        pickup = [pickup_start.val, pickup_start.next_.val, pickup_start.next_.next_.val]
        pickup_end = pickup_start.next_.next_

        next_value = None

        for i in range(cur_cup_val - 1, 0, -1):
            if i not in pickup:
                next_value = i
                break

        if not next_value:
            next_value = len_cups

            while next_value in pickup:
                next_value -= 1

        next_node = nodes[next_value]
        next_node_next = next_node.next_

        cur_node.next_ = pickup_end.next_
        next_node.next_ = pickup_start
        pickup_end.next_ = next_node_next

        cur_node = cur_node.next_

    result = nodes[1]
    print(result.next_.val * result.next_.next_.val)


class Node:
    def __init__(self, val):
        self.val = val
        self.next_ = None


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
