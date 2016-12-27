/* 
Demonstrate Ordered_array by using it to store and retrieve a set of simple objects.
Note that the actual value returned by get_allocation_bytes() is implementation dependent -
it depends on the amount of memory allocated by the C++ implementation to hold a Thing object,
which depends on e.g. the size of an integer, alignment issues, etc. In the sample output,
the implementation required 8 bytes to hold a Thing object.
*/

#include <iostream>
#include "Ordered_array.h"

using std::ostream;	using std::cout;	using std::endl;	using std::cin;

// a simple class to stuff into the container
struct Thing {
	Thing (char in_code = 'x', int in_number = 0) :
		code(in_code), number(in_number)
		{}
	char code;
	int number;
};

int compare_Thing(const Thing& t1, const Thing& t2);
ostream& operator<< (ostream& os, const Thing& t);
void printem(const Ordered_array<Thing>& oa);


int main(void)
{	
	int counter = 0;
	Ordered_array<Thing> o_ary(compare_Thing);
		
	while (true) {
		
		printem(o_ary);
		cout << "\nEnter a search character, or ! to stop: ";
		char c;
		cin >> c;
	
		if(c == '!')
			break;

		Ordered_array<Thing>::Iterator it;
		it = o_ary.find(c);
		if(it == o_ary.end()) {
			cout << "Not found! - adding one!" << endl;
			Thing t(c, ++counter);
			o_ary.insert(t);
			}
		else {
			cout << "Found! The other member is " << it->number << endl;
			cout << "Removing it!" << endl;
			o_ary.remove(it);
			}
		}
		
	o_ary.clear();
	printem(o_ary);

	cout << "Done!" << endl;
}

// Two Things are compared by comparing the character members
int compare_Thing(const Thing& t1, const Thing& t2)
{
	return (t1.code - t2.code); 
}

ostream& operator<< (ostream& os, const Thing& t)
{
	os << t.code << t.number;
	return os;
}

void printem(const Ordered_array<Thing>& oa)
{
	cout << "size, allocation, allocation bytes are " << oa.size() << ", " << oa.get_allocation() 
		<< ", " << oa.get_allocation_bytes() << endl;

	for(Ordered_array<Thing>::Iterator it = oa.begin(); it != oa.end(); it++)
		cout << *it << endl;
}

/* Sample output

size, allocation, allocation bytes are 0, 3, 24

Enter a search character, or ! to stop: c
Not found! - adding one!
size, allocation, allocation bytes are 1, 3, 24
c1

Enter a search character, or ! to stop: m
Not found! - adding one!
size, allocation, allocation bytes are 2, 3, 24
c1
m2

Enter a search character, or ! to stop: r
Not found! - adding one!
size, allocation, allocation bytes are 3, 3, 24
c1
m2
r3

Enter a search character, or ! to stop: d
Not found! - adding one!
size, allocation, allocation bytes are 4, 6, 48
c1
d4
m2
r3

Enter a search character, or ! to stop: e
Not found! - adding one!
size, allocation, allocation bytes are 5, 6, 48
c1
d4
e5
m2
r3

Enter a search character, or ! to stop: c
Found! The other member is 1
Removing it!
size, allocation, allocation bytes are 4, 6, 48
d4
e5
m2
r3

Enter a search character, or ! to stop: r
Found! The other member is 3
Removing it!
size, allocation, allocation bytes are 3, 6, 48
d4
e5
m2

Enter a search character, or ! to stop: z
Not found! - adding one!
size, allocation, allocation bytes are 4, 6, 48
d4
e5
m2
z6

Enter a search character, or ! to stop: !
size, allocation, allocation bytes are 0, 3, 24
Done!

*/
