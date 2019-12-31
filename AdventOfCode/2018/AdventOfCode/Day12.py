import re


def main():
    plants_next_gen = set()

    for line in open("day12.txt"):
        match = re.search("([#.]+) => ([#.])", line)
        if match[2] == "#":
            plants_next_gen.add(match[1])

    cur_state = "#.#####.##.###...#...#.####..#..#.#....##.###.##...#####.#..##.#..##..#..#.#.#.#....#.####....#..#"
    prev_gen_result = 0

    for cur_generation in range(200):
        cur_state = "...." + cur_state + "...."
        next_state = ""

        for i in range(2, len(cur_state) - 2):
            is_plant_next_gen = cur_state[i - 2:i + 3] in plants_next_gen
            next_state += "#" if is_plant_next_gen else "."

        cur_state = next_state

        result = 0

        for i in range(len(cur_state)):
            if cur_state[i] == "#":
                result += i - 2 * (cur_generation + 1)

        print(cur_generation, result, result - prev_gen_result)
        prev_gen_result = result

    # Pattern stabilizes as increasing 194 per iteration
    # Value is 38121 after the first 200 iterations
    print((50000000000 - 200) * 194 + 38121)


if __name__ == "__main__":
    main()
