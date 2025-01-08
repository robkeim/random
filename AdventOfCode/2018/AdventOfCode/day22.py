import heapq
from functools import lru_cache

depth = target_x = target_y = None


def part1():
    global depth, target_x, target_y
    depth, target = open("day22.txt").readlines()
    depth = int(depth.split(":")[1])
    target_x, target_y = [int(value) for value in target.split(":")[1].split(",")]

    risk_level = 0

    for x in range(target_x + 1):
        for y in range(target_y + 1):
            risk_level += erosion_level(x, y) % 3

    print(risk_level)


@lru_cache(1_000_000)
def geologic_index(x, y):
    if x == 0 and y == 0:
        return 0

    if x == target_x and y == target_y:
        return 0

    if y == 0:
        return x * 16807

    if x == 0:
        return y * 48271

    return erosion_level(x - 1, y) * erosion_level(x, y - 1)


@lru_cache(1_000_000)
def erosion_level(x, y):
    return (geologic_index(x, y) + depth) % 20183


def part2():
    global depth, target_x, target_y
    depth, target = open("day22.txt").readlines()
    depth = int(depth.split(":")[1])
    target_x, target_y = [int(value) for value in target.split(":")[1].split(",")]

    dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)]
    seen = set()
    to_process = [(0, 0, 0, "torch")]

    while len(to_process) > 0:
        minutes, x, y, tool = heapq.heappop(to_process)
        key = (x, y, tool)

        if key in seen:
            continue

        seen.add(key)

        if x == target_x and y == target_y and tool == "torch":
            print(minutes)
            break

        for next_tool in ["climbing gear", "none", "torch"]:
            if tool == next_tool:
                continue

            if is_valid_land_type_and_tool(erosion_level(x, y) % 3, next_tool):
                heapq.heappush(to_process, (minutes + 7, x, y, next_tool))

        for dx, dy in dirs:
            next_x = x + dx
            next_y = y + dy

            if next_x < 0 or next_y < 0:
                continue

            if is_valid_land_type_and_tool(erosion_level(next_x, next_y) % 3, tool):
                heapq.heappush(to_process, (minutes + 1, next_x, next_y, tool))


def is_valid_land_type_and_tool(land_type, tool):
    return ((land_type == 0 and tool != "none") or
            (land_type == 1 and tool != "torch") or
            (land_type == 2 and tool != "climbing gear"))


def main():
    part1()
    part2()


if __name__ == "__main__":
    main()
