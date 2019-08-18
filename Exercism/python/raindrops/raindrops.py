def convert(number):
    sounds = [(3, "Pling"), (5, "Plang"), (7, "Plong")]

    result = "".join([sound for num, sound in sounds if number % num == 0])

    return result if len(result) > 0 else str(number)
