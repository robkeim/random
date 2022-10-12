from datetime import datetime

guesses_file_path = "template.txt"
date_format = "%Y-%m-%d"
real_date = datetime.strptime("2022-10-23", date_format)
real_sex = "M"
real_weight = 7.1
real_height = 20


def calculate_closest_guess():
    guesses = parse_guesses()

    guesses_diff = []

    for person, date, sex, weight, height in guesses:
        date_diff = abs((date - real_date).days)
        sex_diff = 1 if sex == real_sex else 0
        weight_diff = abs(weight - real_weight)
        height_diff = abs(height - real_height)

        guesses_diff.append([person, date_diff, sex_diff, weight_diff, height_diff])

    guesses_diff.sort(key=lambda x: (x[1], -1 * x[2], x[3], x[4]))

    print([guess[0] for guess in guesses_diff])


def parse_guesses():
    guesses = []

    with open(guesses_file_path, "r") as file:
        for line in file.readlines()[1:]:
            person, date, sex, weight, height = line.split(",")

            date = datetime.strptime(date, date_format)
            weight = float(weight)
            height = float(height)

            if sex != "M" and sex != "F":
                raise Exception("Invalid sex in line: " + line)

            guesses.append((person, date, sex, weight, height))

    return guesses


if __name__ == "__main__":
    calculate_closest_guess()
