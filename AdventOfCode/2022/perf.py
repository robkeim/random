import os
import sys
import time


def main():
    for day in range(1, 26):
        day_str = str(day) if day > 9 else "0" + str(day)

        try:
            module = __import__("day" + day_str)

            print("Day " + day_str)

            part1 = measure_execution_time(module.part1)
            print("    Part 1: {} sec ({} ms)".format(part1, round(part1 * 1000, 1)))

            part2 = measure_execution_time(module.part2)
            print("    Part 2: {} sec ({} ms)".format(part2, round(part2 * 1000, 1)))

            print()

        except ModuleNotFoundError:
            break


def measure_execution_time(function):
    sys.stdout = open(os.devnull, "w")  # Prevent printing

    start_time = time.time()
    function()
    elapsed_time = time.time() - start_time

    sys.stdout = sys.__stdout__  # Restore printing

    return round(elapsed_time, 4)


if __name__ == "__main__":
    main()
