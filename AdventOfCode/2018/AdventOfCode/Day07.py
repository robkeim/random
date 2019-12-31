import re

from collections import defaultdict


def main():
    blocked_by = defaultdict(set)
    steps = set()

    for line in open("Day07.txt"):
        match = re.search("Step ([A-Z]) must be finished before step ([A-Z]) can begin.", line)
        blocked_by[match[2]].add(match[1])
        steps.add(match[1])
        steps.add(match[2])

    cur_tick = 0
    cur_processing = list()
    max_concurrent_processes = 5
    base_step_execution_time = 60

    while len(steps) > 0 or len(cur_processing) > 0:
        finished_items = list(filter(lambda x: x[1] <= cur_tick, cur_processing))

        for finished_item in finished_items:
            for item in blocked_by:
                if finished_item[0] in blocked_by[item]:
                    blocked_by[item].remove(finished_item[0])

        cur_processing = list(filter(lambda x: x[1] > cur_tick, cur_processing))

        available_items = sorted(list(filter(lambda x: len(blocked_by[x]) == 0, steps)))

        for available_item in available_items:
            if len(cur_processing) == max_concurrent_processes:
                break

            duration = ord(available_item) - ord("A") + 1
            cur_processing.append((available_item, cur_tick + base_step_execution_time + duration))
            steps.remove(available_item)

        cur_tick += 1

    print(cur_tick - 1)


if __name__ == "__main__":
    main()
