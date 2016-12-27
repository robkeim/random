#!/bin/bash

NUM_TESTS=0
NUM_PASSED=0

# This function checks the return code from the previous test, outputting
# PASSED or FAILED appropriately.  It increments the count to keep track of the
# total number of tests passed
check()
{
	NUM_TESTS=$((NUM_TESTS+1))
	if [ $2 -ne 0 ]
	then
		echo "$1: FAILED"
	else
		echo "$1: PASSED"
		NUM_PASSED=$((NUM_PASSED+1))
	fi
}

# Insert the names of the tests here
tests=( cruise status views )

# Execute all of the tests in the array above where the input for the test is in
# the file t_TEST_in.txt and the output to compare is in t_TEST_out.txt
for test in ${tests[@]}
do
    ./p6exe < t_${test}_in.txt > rk
	diff rk t_${test}_out.txt > /dev/null
	check $NUM_TESTS $?
done

echo ""
echo "Summary: $NUM_PASSED/$NUM_TESTS"
echo ""

rm rk

