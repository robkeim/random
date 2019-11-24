import collections


raw_input = "sialva"
input_counter = collections.Counter(raw_input)

with open("dictionary.txt", "r") as dictionary:
    for line in dictionary:
        line = line.strip()

        if len(line) > len(raw_input):
            continue

        line_counter = collections.Counter(line)
        match = True
        for element in line_counter:
            value = line_counter[element]

            if element not in input_counter or value > input_counter[element]:
                match = False
                break

        if match:
            print(line)
