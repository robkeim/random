from collections import defaultdict, deque


def part1():
    lines = [line.strip() for line in open("day20.txt").readlines()]
    destinations = defaultdict(list)
    flip_flops = set()
    conjunctions = set()
    state = {}

    # Parse input
    for line in lines:
        source, destination = line.split(" -> ")

        if source[0] == "%":
            flip_flops.add(source[1:])
            state[source[1:]] = "off"

        if source[0] == "&":
            conjunctions.add(source[1:])
            state[source[1:]] = {}

        if source[0] in "%&":
            source = source[1:]

        destinations[source] = destination.split(", ")

    for source in destinations:
        for destination in destinations[source]:
            if destination in conjunctions:
                state[destination][source] = "low"

    # Process
    low_pulses = 0
    high_pulses = 0
    for press in range(1000):

        to_process = deque([("broadcaster", "low", "")])

        while len(to_process) > 0:
            start, signal, prev = to_process.popleft()

            if signal == "low":
                low_pulses += 1
            else:
                high_pulses += 1

            if prev == "output":
                print("Output =", signal)
                break

            if start in flip_flops:
                if signal == "high":
                    continue

                if state[start] == "on":
                    state[start] = "off"
                    signal = "low"
                else:
                    state[start] = "on"
                    signal = "high"

                for destination in destinations[start]:
                    to_process.append((destination, signal, start))
            elif start in conjunctions:
                state[start][prev] = signal

                for destination in destinations[start]:
                    if len([value for value in state[start].values() if value != "high"]) == 0:
                        to_process.append((destination, "low", start))
                    else:
                        to_process.append((destination, "high", start))
            else:
                for destination in destinations[start]:
                    to_process.append((destination, signal, start))

    print(low_pulses * high_pulses)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
