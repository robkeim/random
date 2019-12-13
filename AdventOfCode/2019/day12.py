import re


def part1():
    planets = [(extract_starting_point(line), [0, 0, 0]) for line in open("day12.txt").readlines()]

    for _ in range(1000):
        new_planets = []

        for pos, velocity in planets:
            new_velocity = velocity[:]
            for other, _ in planets:
                for i in range(3):
                    if pos[i] > other[i]:
                        new_velocity[i] -= 1
                    elif pos[i] < other[i]:
                        new_velocity[i] += 1
            new_planets.append(([pos[0] + new_velocity[0], pos[1] + new_velocity[1], pos[2] + new_velocity[2]], new_velocity))

        planets = new_planets

    total_energy = 0

    for pos, velocity in planets:
        potential_energy = 0
        kinetic_energy = 0
        for i in range(3):
            potential_energy += abs(pos[i])
            kinetic_energy += abs(velocity[i])

        total_energy += potential_energy * kinetic_energy

    print(total_energy)

    pass


def extract_starting_point(line):
    match = re.search("x=(-?\d+), y=(-?\d+), z=(-?\d+)", line)

    if not match:
        raise Exception("Invalid line format: " + line)

    return [int(match[1]), int(match[2]), int(match[3])]


def part2():
    pass


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
