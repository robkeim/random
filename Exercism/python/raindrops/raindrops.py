def convert(number):
    sounds = [(3, "Pling"), (5, "Plang"), (7, "Plong")]

    result = "".join([x[1] for x in sounds if number % x[0] == 0])

    return result if len(result) > 0 else str(number)
