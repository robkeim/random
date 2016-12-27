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
echo "String Tests:"

./d_s1exe < d_s1.in > rk
diff rk d_s1.out > /dev/null
check 1 $?

./d_s2exe < d_s2.in > rk
diff rk d_s2.out > /dev/null
check 2 $?

./d_s3exe > rk
diff rk d_s3.out > /dev/null
check 3 $?

./d_s4exe > rk
diff rk d_s4.out > /dev/null
check 4 $?

echo ""
echo "Ordered Array Tests:"

./d_ol1exe < d_ol1.in > rk
diff rk d_ol1.out > /dev/null
check 1 $?

./d_ol2exe > rk
diff rk d_ol2.out > /dev/null
check 2 $?

echo ""
echo "Overall Tests:"

./p2exe < d_normal.in > rk
diff rk d_normal.out > /dev/null
check 1 $?

./p2exe < d_typeahead.in > rk
diff rk d_typeahead.out > /dev/null
check 2 $?

./p2exe < d_errors.in > rk
diff rk d_errors.out > /dev/null
check 3 $?

echo ""
echo "Summary: $NUM_PASSED/$NUM_TESTS"
echo ""

rm rk

