def part1():
    lines = [(line[0], int(line.strip()[1:])) for line in open("day12.txt").readlines()]
    dirs = [(0, 1), (1, 0), (0, -1), (-1, 0)]
    cur_dir = 1
    x = y = 0

    for instruction, value in lines:
        if instruction == "N":
            y += value
        elif instruction == "S":
            y -= value
        elif instruction == "E":
            x += value
        elif instruction == "W":
            x -= value
        elif instruction == "L":
            cur_dir = (cur_dir - value // 90 + 4) % 4
        elif instruction == "R":
            cur_dir = (cur_dir + value // 90) % 4
        elif instruction == "F":
            x += dirs[cur_dir][0] * value
            y += dirs[cur_dir][1] * value
        else:
            raise Exception("Invalid instruction: " + instruction)

    print(abs(x) + abs(y))


def part2():
    lines = [(line[0], int(line.strip()[1:])) for line in open("day12.txt").readlines()]
    ship_x = ship_y = 0
    waypoint_x = 10
    waypoint_y = 1

    for instruction, value in lines:
        if instruction == "N":
            waypoint_y += value
        elif instruction == "S":
            waypoint_y -= value
        elif instruction == "E":
            waypoint_x += value
        elif instruction == "W":
            waypoint_x -= value
        elif instruction == "L" or instruction == "R":
            if instruction == "L":
                value = ((value * -1) + 360) % 360

            delta_x = ship_x - waypoint_x
            delta_y = ship_y - waypoint_y

            if value == 90:
                waypoint_x = ship_x - delta_y
                waypoint_y = ship_y + delta_x
            elif value == 180:
                waypoint_x = ship_x + delta_x
                waypoint_y = ship_y + delta_y
            elif value == 270:
                waypoint_x = ship_x + delta_y
                waypoint_y = ship_y - delta_x
            else:
                raise Exception("Invalid rotation: " + str(value))
        elif instruction == "F":
            delta_x = (waypoint_x - ship_x) * value
            delta_y = (waypoint_y - ship_y) * value
            ship_x += delta_x
            ship_y += delta_y
            waypoint_x += delta_x
            waypoint_y += delta_y
        else:
            raise Exception("Invalid instruction: " + instruction)

    print(abs(ship_x) + abs(ship_y))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
