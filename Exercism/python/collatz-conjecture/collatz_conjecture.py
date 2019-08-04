def steps(number):
    if number <= 0:
        raise ValueError("Number must be positive")

    num_steps = 0

    while number != 1:
        num_steps += 1
        if number % 2 == 0:
            number /= 2
        else:
            number = 3 * number + 1

    return num_steps
