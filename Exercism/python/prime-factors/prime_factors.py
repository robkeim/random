def factors(value):
    result = []

    while value != 1:
        counter = 2
        while value % counter != 0:
            counter += 1

        value /= counter
        result.append(counter)

    return result
