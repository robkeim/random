def part1():
    lines = [line.strip() for line in open("quest07_p1.txt").readlines()]
    ranking = []

    for line in lines:
        key, plan = line.split(":")
        plan = plan.split(",")
        total_power = 0
        cur_power = 10

        for i in range(10):
            value = plan[i % len(plan)]

            if value == "+":
                cur_power += 1
            elif value == "-":
                cur_power -= 1

            total_power += cur_power

        ranking.append((-total_power, key))

    ranking = sorted(ranking)
    print("".join([value[1] for value in ranking]))


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
