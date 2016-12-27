#!/usr/bin/python

import re

from math import fabs

def main():
    # Files are assumed to be in the current directory, change this if the
    #  file names change
    file1 = open("./TOXORUN2_chemgauss3_scores.txt", "r")
    file2 = open("./VERTRUN2_chemgauss3_scores.txt", "r")

    # Regular expressions to match the file format
    clu_regx = re.compile("Clu\s*(\d+),\s*Mem\s*(\d+)\s*-(\S*)\s*\S*\s*\S*\s*\S*\s*\S*\s*\S*\s*(\S*)")
    head_regx = re.compile("Head\s*(\d+),\s*Size=(\d+)\s*-(\S*)\s*\S*\s*\S*\s*\S*\s*\S*\s*\S*\s*(\S*)")

    clu_map = {}
    head_map = {}
    clu_formula_map = {}
    head_formula_map = {}

    # Read the first file and make maps of all of the unique pairs
    file1 = file1.readlines()
    for line in file1:
        match = clu_regx.match(line)
        if match:
            hash_value = get_hash(match.group(1), match.group(2))
            clu_map[hash_value] = match.group(3)
            clu_formula_map[hash_value] = match.group(4)
        match = head_regx.match(line)
        if match:
            hash_value = get_hash(match.group(1), match.group(2))
            head_map[hash_value] = match.group(3)
            head_formula_map[hash_value] = match.group(4)
    
    clu_list = []
    head_list = []
    master_list = []

    # Find matches in the second file and record the differences between
    #  the two in a list
    file2 = file2.readlines()
    for line in file2:
        match = clu_regx.match(line)
        if match:
            hash_value = get_hash(match.group(1), match.group(2))
            if hash_value in clu_map:
                dif = fabs(float(clu_map[hash_value]) - float(match.group(3)))
                clu_list.append((dif, hash_value, clu_formula_map[hash_value]))
                master_list.append((dif, hash_value, clu_formula_map[hash_value], 0))
        match = head_regx.match(line)
        if match:
            hash_value = get_hash(match.group(1), match.group(2))
            if hash_value in head_map:
                dif = fabs(float(head_map[hash_value]) - float(match.group(3)))
                head_list.append((dif, hash_value, head_formula_map[hash_value]))
                master_list.append((dif, hash_value, head_formula_map[hash_value], 1))

    # Display the results
    print_clu(sorted(clu_list, reverse=True))
    print ""
    print_head(sorted(head_list, reverse=True))
    print ""
    print_master(sorted(master_list, reverse=True))

# This function makes a hash value out of the two integers by making
#  sure all values are three digits
def get_hash(val1, val2):
    val1 = int(val1)
    val2 = int(val2)

    if val1 < 10:
        total = "00" + str(val1)
    elif val1 < 100:
        total = "0" + str(val1)
    else:
        total = str(val1)

    if val2 < 10:
        total += "00" + str(val2)
    elif val2 < 100:
        total += "0" + str(val2)
    else:
        total += str(val2)

    return total

# Print the Clu list in an easily readable format
def print_clu(clu_list):
    print "Clu List:"
    for item in clu_list:
        hash_value = item[1]
        value = str(item[0])
        print "Clu " + str(int(hash_value[:3])) + ", Mem " + str(int(hash_value[3:])) + ": " + value + " " + item[2]

# Print the Head list in an easily readable format
def print_head(head_list):
    print "Head List:"
    for item in head_list:
        hash_value = item[1]
        value = str(item[0])
        print "Head " + str(int(hash_value[:3])) + ", Size=" + str(int(hash_value[3:])) + ": " + value + " " + item[2]

# Print the Master list in an easily readable format
def print_master(master_list):
    print "Master List:"
    for item in master_list:
        hash_value = item[1]
        value = str(item[0])
        if int(item[3]) == 0:
            print "Clu " + str(int(hash_value[:3])) + ", Mem " + str(int(hash_value[3:])) + ": " + value + " " + item[2]
        else:
            print "Head " + str(int(hash_value[:3])) + ", Size=" + str(int(hash_value[3:])) + ": " + value + " " + item[2]

if __name__ == "__main__":
    main()

