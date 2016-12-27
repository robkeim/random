#!/usr/bin/python

import re

from math import fabs

def main():
    # Files are assumed to be in the current directory, change this if the
    #  file names change
    file1 = open("./CHEMSCREEN_chemgauss3_scores.txt", "r")
    file2 = open("./CHEMSCREENVERT_chemgauss3_scores.txt", "r")

    # Regular expressions to match the file format
    regx = re.compile("(\d+)\s*-(\S*).*?(\S*)")

    the_map = {}
    the_formula_map = {}

    # Read the first file and make maps of all of the unique pairs
    file1 = file1.readlines()
    for line in file1:
        match = regx.match(line)
        if match:
            the_map[match.group(1)] = match.group(2)
            the_formula_map[match.group(1)] = match.group(3)
    
    the_list = []

    # Find matches in the second file and record the differences between
    #  the two in a list
    file2 = file2.readlines()
    for line in file2:
        match = regx.match(line)
        if match:
            hash_value = match.group(1)
            if hash_value in the_map:
                dif = fabs(float(the_map[hash_value]) - float(match.group(2)))
                the_list.append((dif, hash_value, the_formula_map[hash_value], 0))

    # Display the results
    print_list(sorted(the_list, reverse=True))

# Print the list in an easily readable format
def print_list(the_list):
    for item in the_list:
        value = str(item[0])
        name = str(item[1])
        print name + ": " + value + " " + item[2]

if __name__ == "__main__":
    main()

