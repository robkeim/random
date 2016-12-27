/* Demonstrate how an Ordered_array can be quickly loaded using the insert_at_end function
when the data are available already in order.
Note that the actual value returned by get_allocation_bytes() is implementation dependent -
it depends on the amount of memory allocated by the C++ implementation to hold an int object.
In the sample output, the implementation required 4 bytes to hold an int.
*/

#include <iostream>

#include "Ordered_array.h"

using std::cout;	using std::endl;	using std::cin;

int compare_int(const int& i1, const int& i2);
void printem(const Ordered_array<int>& oa);

int main(void)
{		
	Ordered_array<int> o_ary(compare_int);
	
	// be sure to do these in order! Results undefined if not!
	o_ary.insert_at_end(1);
	o_ary.insert_at_end(3);
	o_ary.insert_at_end(5);
	o_ary.insert_at_end(7);
	o_ary.insert_at_end(9);
		
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
		cout << "Contents:" << endl;
		printem(o_ary);
		}

	cout << "Done!" << endl;
	return 0;
}

int compare_int(const int& i1, const int& i2)
{
	return (i1 - i2); 
}

void printem(const Ordered_array<int>& oa)
{
	cout << "size, allocation, allocation bytes are " << oa.size() << ", " << oa.get_allocation() 
		<< ", " << oa.get_allocation_bytes() << endl;
	for(Ordered_array<int>::Iterator it = oa.begin(); it != oa.end(); it++)
		cout << *it << endl;
}

/* Output

Enter a search value, non-number to stop:20
Not found! - adding it!
Contents:
size, allocation, allocation bytes are 6, 6, 24
1
3
5
7
9
20

Enter a search value, non-number to stop:21
Not found! - adding it!
Contents:
size, allocation, allocation bytes are 7, 12, 48
1
3
5
7
9
20
21

Enter a search value, non-number to stop:3
Found! Removing it!
Contents:
size, allocation, allocation bytes are 6, 12, 48
1
5
7
9
20
21

Enter a search value, non-number to stop:q
Done!

*/
