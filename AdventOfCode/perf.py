import sys
import os
import time
import importlib.util


def main():
    if len(sys.argv) < 2:
        print("Usage: python perf.py <year>")
        sys.exit(1)

    year = sys.argv[1]

    for day in range(1, 26):
        day_str = f"{day:02d}"
        print(f"Day {day_str}")

        script_path = os.path.join(year, f"day{day_str}.py")
        if not os.path.isfile(script_path):
            print("    Part 1: N/A")
            print("    Part 2: N/A\n")
            continue

        try:
            module = import_from_path(day_str, script_path)

            # Temporarily change to the <year> directory
            original_dir = os.getcwd()
            os.chdir(year)

            try:
                # Measure Part 1
                part1_time = measure_execution_time(module.part1)

                if part1_time:
                    print(f"    Part 1: {part1_time} sec ({round(part1_time * 1000, 1)} ms)")
                else:
                    print("    Part 1: N/A")

                # Measure Part 2
                part2_time = measure_execution_time(module.part2)

                if part1_time and day != 25:
                    print(f"    Part 2: {part2_time} sec ({round(part2_time * 1000, 1)} ms)")
                else:
                    print("    Part 2: N/A")
            finally:
                # Change back to original directory
                os.chdir(original_dir)

        except Exception as e:
            print(f"    Error running day {day_str}: {e}")
            print("    Part 1: N/A")
            print("    Part 2: N/A")

        print()


def import_from_path(day_str, script_path):
    spec = importlib.util.spec_from_file_location(day_str, script_path)
    module = importlib.util.module_from_spec(spec)
    spec.loader.exec_module(module)
    return module


def measure_execution_time(func):
    original_stdout = sys.stdout

    try:
        with open(os.devnull, "w") as null_out:
            sys.stdout = null_out
            start = time.time()
            func()
            elapsed = time.time() - start



        return round(elapsed, 4)
    except Exception:
        return None
    finally:
        sys.stdout = original_stdout


if __name__ == "__main__":
    main()
