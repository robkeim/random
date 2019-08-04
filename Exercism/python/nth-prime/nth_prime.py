def prime(number):
    if number <= 0:
        raise ValueError("To find the Nth prime, N needs to be positive")

    primes = []

    counter = 2

    while number > 0:
        while not is_prime(primes, counter):
            counter += 1

        primes.append(counter)
        number -= 1

    return primes[-1]


def is_prime(primes, number):
    for prime in primes:
        if number % prime == 0:
            return False

    return True
