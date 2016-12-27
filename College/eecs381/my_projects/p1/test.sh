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

echo ""
echo "Linked List Tests:"

./test1L > rk
diff rk test1out > /dev/null
check 1 $?

./test2L > rk
diff rk test2out > /dev/null
check 2 $?

echo ""
echo "Array Tests:"

./test1A > rk
diff rk test1out > /dev/null
check 1 $?

./test2L > rk
diff rk test2out > /dev/null
check 2 $?

echo ""

echo "Record Tests:"

./test1R > rk
diff rk test1Rout > /dev/null
check 1 $?

echo ""

echo "Collection Tests:"

./test1CL > rk
diff rk test1Cout > /dev/null
check 1 $?

./test1CA > rk
diff rk test1Cout > /dev/null
check 2 $?

echo ""

echo "Normal Tests:"

./p1Aexe < normal_in.txt > rk
diff rk normalA_out.txt > /dev/null
check 1 $?

diff savefile1.txt savefile1_given.txt > /dev/null
check F $?

./p1Lexe < normal_in.txt > rk
diff rk normalL_out.txt > /dev/null
check 2 $?

diff savefile1.txt savefile1_given.txt > /dev/null
check F $?

echo ""

echo "Error Tests:"

./p1Aexe < errors_in.txt > rk
diff rk errors_out.txt > /dev/null
check 1 $?


./p1Lexe < errors_in.txt > rk
diff rk errors_out.txt > /dev/null
check 2 $?

echo ""

echo "Typeahead Tests:"

./p1Aexe < typeahead_in.txt > rk
diff rk typeahead_out.txt > /dev/null
check 1 $?


./p1Lexe < typeahead_in.txt > rk
diff rk typeahead_out.txt > /dev/null
check 2 $?

echo ""

echo "Summary:"

echo "Passed $NUM_PASSED / $NUM_TESTS"

echo ""

rm rk
