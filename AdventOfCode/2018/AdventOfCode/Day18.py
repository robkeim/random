def get_hash(grid):
    result = ""
    for y in range(grid_size):
        for x in range(grid_size):
            result += grid[x][y]

    return result


cur_iteration = [list(line.strip()) for line in open("day18.txt").readlines()]
grid_size = len(cur_iteration)

seen = dict()
iteration = 0
seen[get_hash(cur_iteration)] = iteration

while True:
    next_iteration = [line[:] for line in cur_iteration]

    for y in range(grid_size):
        for x in range(grid_size):
            num_adj_trees = 0
            num_adj_lumberyards = 0

            for dy in range(-1, 2):
                for dx in range(-1, 2):
                    if (dx == 0 and dy == 0) or x + dx < 0 or y + dy < 0 or x + dx >= grid_size or y + dy >= grid_size:
                        continue

                    value = cur_iteration[x + dx][y + dy]
                    if value == "|":
                        num_adj_trees += 1
                    elif value == "#":
                        num_adj_lumberyards += 1

            if cur_iteration[x][y] == ".":
                next_iteration[x][y] = "|" if num_adj_trees >= 3 else "."
            elif cur_iteration[x][y] == "|":
                next_iteration[x][y] = "#" if num_adj_lumberyards >= 3 else "|"
            elif cur_iteration[x][y] == "#":
                next_iteration[x][y] = "#" if num_adj_lumberyards >= 1 and num_adj_trees >= 1 else "."
            else:
                raise Exception("Invalid current square value: " + cur_iteration[x][y])

    if get_hash(next_iteration) in seen:
        break

    seen[get_hash(next_iteration)] = iteration

    cur_iteration = next_iteration
    iteration += 1

prev = seen[get_hash(next_iteration)]

num_remaining = (1000000000 - prev) % (iteration - prev)

for _ in range(num_remaining):
    next_iteration = [line[:] for line in cur_iteration]

    for y in range(grid_size):
        for x in range(grid_size):
            num_adj_trees = 0
            num_adj_lumberyards = 0

            for dy in range(-1, 2):
                for dx in range(-1, 2):
                    if (dx == 0 and dy == 0) or x + dx < 0 or y + dy < 0 or x + dx >= grid_size or y + dy >= grid_size:
                        continue

                    value = cur_iteration[x + dx][y + dy]
                    if value == "|":
                        num_adj_trees += 1
                    elif value == "#":
                        num_adj_lumberyards += 1

            if cur_iteration[x][y] == ".":
                next_iteration[x][y] = "|" if num_adj_trees >= 3 else "."
            elif cur_iteration[x][y] == "|":
                next_iteration[x][y] = "#" if num_adj_lumberyards >= 3 else "|"
            elif cur_iteration[x][y] == "#":
                next_iteration[x][y] = "#" if num_adj_lumberyards >= 1 and num_adj_trees >= 1 else "."
            else:
                raise Exception("Invalid current square value: " + cur_iteration[x][y])

    cur_iteration = next_iteration

num_trees = 0
num_lumberyards = 0

for y in range(grid_size):
    for x in range(grid_size):
        if cur_iteration[x][y] == "|":
            num_trees += 1
        elif cur_iteration[x][y] == "#":
            num_lumberyards += 1

print(num_trees * num_lumberyards)
