/* Demonstrate Ordered_array by using it to store a set of integers in two
different orders. This also demonstrates the the use of the copy constructor and 
assignment operator. 
Note that the actual value returned by get_allocation_bytes() is implementation dependent -
it depends on the amount of memory allocated by the C++ implementation to hold an int object.
In the sample output, the implementation required 4 bytes to hold an int.
*/

#include <iostream>
#include <iomanip>

#include "Ordered_array.h"

using std::cout;	using std::endl;	using std::cin;

int compare_int(const int& i1, const int& i2);
int reverse_compare_int(const int& i1, const int& i2);
Ordered_array<int> reverse_it(Ordered_array<int> oa);
void printem(const Ordered_array<int>& oa);

int main(void)
{		
	Ordered_array<int> o_ary(compare_int);
		
	while (true) {
		cout << "\nEnter a search value, non-number to stop:";
		int input;
		cin >> input;
	
		if(!cin)
			break;
		Ordered_array<int>::Iterator it;
		it = o_ary.find(input);
		if(it == o_ary.end()) {
			cout << "Not found! - adding it!" << endl;
			o_ary.insert(input); 
			}
		else {
			cout << "Found! Removing it!" << endl;
			o_ary.remove(it);
			}
		// call a function that takes a call-by-value argument
		// and returns a value that gets assigned
		cout << "Normal order:" << endl;
		printem(o_ary);
		Ordered_array<int> rev_ary(reverse_compare_int);
		rev_ary = reverse_it(o_ary);	// assign result
		cout << "Reverse order:" << endl;
		printem(rev_ary);
		}
	

	cout << "Done!" << endl;
	return 0;
}

// reverse the array by filling another with a reverse comparision function
// argument is handed in by value, so is copied.
Ordered_array<int> reverse_it(Ordered_array<int> oa)
{
	Ordered_array<int> result(reverse_compare_int);
	for(Ordered_array<int>::Iterator it = oa.begin(); it != oa.end(); it++)
		result.insert(*it);
	
	return result;	// returned by value, so copied (unless compiler optimizes)
}


int compare_int(const int& i1, const int& i2)
{
	return (i1 - i2); 
}

// this puts ints in reverse order
int reverse_compare_int(const int& i1, const int& i2)
{
	return (i2 - i1); 
}

void printem(const Ordered_array<int>& oa)
{
	cout << "size, allocation, allocation bytes are " << oa.size() << ", " << oa.get_allocation() 
		<< ", " << oa.get_allocation_bytes() << endl;
	for(Ordered_array<int>::Iterator it = oa.begin(); it != oa.end(); it++)
		cout << *it << endl;
}

/* Output

Enter a search value, non-number to stop:5
Not found! - adding it!
Normal order:
size, allocation, allocation bytes are 1, 3, 12
5
Reverse order:
size, allocation, allocation bytes are 1, 3, 12
5

Enter a search value, non-number to stop:9
Not found! - adding it!
Normal order:
size, allocation, allocation bytes are 2, 3, 12
5
9
Reverse order:
size, allocation, allocation bytes are 2, 3, 12
9
5

Enter a search value, non-number to stop:3
Not found! - adding it!
Normal order:
size, allocation, allocation bytes are 3, 3, 12
3
5
9
Reverse order:
size, allocation, allocation bytes are 3, 3, 12
9
5
3

Enter a search value, non-number to stop:100
Not found! - adding it!
Normal order:
size, allocation, allocation bytes are 4, 6, 24
3
5
9
100
Reverse order:
size, allocation, allocation bytes are 4, 4, 16
100
9
5
3

Enter a search value, non-number to stop:-1
Not found! - adding it!
Normal order:
size, allocation, allocation bytes are 5, 6, 24
-1
3
5
9
100
Reverse order:
size, allocation, allocation bytes are 5, 5, 20
100
9
5
3
-1

Enter a search value, non-number to stop:5
Found! Removing it!
Normal order:
size, allocation, allocation bytes are 4, 6, 24
-1
3
9
100
Reverse order:
size, allocation, allocation bytes are 4, 4, 16
100
9
3
-1

Enter a search value, non-number to stop:xxx
Done!

*/
