/* Demonstrate Ordered_array by using it to store an set of pointers to C-strings
in alphabetical order, using the standard library strcmp for the ordering function.

The point of this  demo is to illustrate using the Ordered_array container with pointers.
This demo is a sick mixture of good C++ with the Ordered_array container and
ugly C-style hacking with C-strings - std::string should be used instead in real code. 

Note that the pointed-to C-strings continue to be the same ones; the container that
is copied or assigned contains only pointers to the same C-strings allocated in memory.
*/

#include <iostream>
#include <iomanip>
#include <cstring>

#include "Ordered_array.h"

using std::strcmp;	using std::strlen;	using std::strcpy;
using std::cout;	using std::endl;	using std::cin;
using std::setw;

// A comparison function - note the reference to pointer is needed to match
// with the Ordered_array use of reference parameters for efficiency with 
// large objects
int comp_str( char * const & p1,  char * const & p2)
{
	return strcmp(p1, p2);
}

// this function takes a call-by-value argument of an Ordered_array,
// adds some elements to it, and then returns it by value.
Ordered_array<char *> add_some(Ordered_array<char *> a);

/* print a C-string */
void printit(char * ptr);

/* this function applies function f to each element to print it */
void printem(const Ordered_array<char *>& a, void(*f)(char *));

// a little function to create C-strings in allocated memory
char * create_string(char *);


int main(void)
{	
	/* cast the strcmp function to match the comparison function type */
//	int(*fp)(const char * &, const char * ) = (int(*)(const char *, const char *))strcmp;
	
	Ordered_array<char *> o_ary(comp_str);
	printem(o_ary, printit);
		
	while (true) {
		char buffer[10];		
		printem(o_ary, printit);
		cout << "\nEnter a search string, ADDEM, or STOP:";
		cin >> setw(9) >> buffer;	// should read up to 9 characters
	
		if(strcmp(buffer, "STOP") == 0)
			break;
		else if(strcmp(buffer, "ADDEM") == 0) {
			o_ary = add_some(o_ary);
			continue;
			}

		Ordered_array<char *>::Iterator index = o_ary.find(buffer);
		if(index == o_ary.end()) {
			cout << "Not found! - adding it!" << endl;
			o_ary.insert(create_string(buffer));
			}
		else {
			cout << "Found! - removing it" << endl;
			/* Free the memory for the pointed-to string before removing
			the pointer from the array. Note the array-style delete
			 - because we allocated with array new. */
			delete[] *index;
			o_ary.remove(index);
			}
		}
		
	/* Free all of the memory for the pointed-to strings before throwing away
	all of the pointers */	
	for(Ordered_array<char *>::Iterator i = o_ary.begin(); i != o_ary.end(); i++) {
		// note the array-style delete - because we allocated with array new.
		delete[] *i;
		}
	o_ary.clear();
	printem(o_ary, printit);

	cout << "Done!" << endl;
	return 0;
}

/* print an item */
void printit(char * ptr)
{
	cout << ptr;
}


/* print the contents of the Ordered_array */
void printem(const Ordered_array<char *>& a, void(*f)(char *))
{
	cout << "size, allocation are " << a.size() << ' '
		<< a.get_allocation() << endl;
	for(Ordered_array<char *>::Iterator i = a.begin(); i != a.end(); i++) {
		f(*i);
		cout << endl;
		}
}


Ordered_array<char *> add_some(Ordered_array<char *> a)
{
	Ordered_array<char *> aa(a);	// copy-construct another one
	printem(a, printit);
	printem(aa, printit);
	
	
	if(!aa.is_present("abracadabera"))
		aa.insert(create_string("abracadabera"));
	if(!aa.is_present("middle"))
		aa.insert(create_string("middle"));
	if(!aa.is_present("zounds!"))
		aa.insert(create_string("zounds!"));
	
	printem(a, printit);
	printem(aa, printit);
	
	return aa;
}
	

/* create_string copies the supplied string into a piece of allocated memory and returns a pointer to it */
char * create_string(char * buffer)
{
	char * str = new char[strlen(buffer) + 1];
	strcpy(str, buffer);
	return str;
}

/* Sample output

size, allocation are 0 3
size, allocation are 0 3

Enter a search string, ADDEM, or STOP:aardvark
Not found! - adding it!
size, allocation are 1 3
aardvark

Enter a search string, ADDEM, or STOP:ADDEM
size, allocation are 1 3
aardvark
size, allocation are 1 3
aardvark
size, allocation are 1 3
aardvark
size, allocation are 4 6
aardvark
abracadabera
middle
zounds!
size, allocation are 4 4
aardvark
abracadabera
middle
zounds!

Enter a search string, ADDEM, or STOP:yogurt
Not found! - adding it!
size, allocation are 5 8
aardvark
abracadabera
middle
yogurt
zounds!

Enter a search string, ADDEM, or STOP:yogurt
Found! - removing it
size, allocation are 4 8
aardvark
abracadabera
middle
zounds!

Enter a search string, ADDEM, or STOP:ADDEM
size, allocation are 4 4
aardvark
abracadabera
middle
zounds!
size, allocation are 4 4
aardvark
abracadabera
middle
zounds!
size, allocation are 4 4
aardvark
abracadabera
middle
zounds!
size, allocation are 4 4
aardvark
abracadabera
middle
zounds!
size, allocation are 4 4
aardvark
abracadabera
middle
zounds!

Enter a search string, ADDEM, or STOP:middle
Found! - removing it
size, allocation are 3 4
aardvark
abracadabera
zounds!

Enter a search string, ADDEM, or STOP:STOP
size, allocation are 0 3
Done!

*/
