def part1():
    lines = [line.strip() for line in open("day06.txt").readlines()]
    times = [int(time) for time in lines[0].split(":")[1].split()]
    records = [int(record) for record in lines[1].split(":")[1].split()]

    result = 1

    for i in range(len(times)):
        total_time = times[i]
        record = records[i]
        distances = [calc_score(total_time, seconds_to_hold) for seconds_to_hold in range(total_time)]
        result *= len([distance for distance in distances if distance > record])

    print(result)


def calc_score(total_time, seconds_to_hold):
    return seconds_to_hold * (total_time - seconds_to_hold)


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
