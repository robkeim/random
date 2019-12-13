import sys

from random import randint


def main():
    num_cases = int(sys.stdin.readline())

    for case in range(num_cases):
        n = int(sys.stdin.readline())

        while True:
            rand = randint(1, n)

            if "4" not in str(rand) and "4" not in str(n - rand):
                sys.stdout.write("Case #%i: %i %i\n" % (case + 1, rand, n - rand))
                break


if __name__ == "__main__":
    main()
