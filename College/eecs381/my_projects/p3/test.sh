#!/bin/sh

NUM_TESTS=0
NUM_PASSED=0

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

./p3exe < t_normal_in.txt > rk
diff rk t_normal_out.txt > /dev/null
check 1 $?

diff t_normal_savefile1.txt savefile1.txt > /dev/null
check F $?

./p3exe < t_typeahead_in.txt > rk
diff rk t_typeahead_out.txt > /dev/null
check 2 $?

./p3exe < t_errors_in.txt > rk
diff rk t_errors_out.txt > /dev/null
check 3 $?

./p3exe < t_new_commands_in.txt > rk
diff rk t_new_commands_out.txt > /dev/null
check 4 $?

echo ""
echo "Summary: $NUM_PASSED/$NUM_TESTS"
echo ""

rm rk

