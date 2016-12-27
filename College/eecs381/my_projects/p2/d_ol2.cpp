/*
 This contains a demo of the Ordered_list template, showing how it is constructed and searched
 for a list of ints, char * pointers, and Thing objects.
 
 Use a simple program like this as a "test harness" to systematically test your Ordered_list
 class functions, starting with the simplest and most basic.
 */
#include "Ordered_list.h"

#include <iostream>
#include <string>
#include <cstring>

using namespace std;

// Things have an ID number used to compare them.
class Thing {
public:
	Thing(int i_in) : i(i_in) {}
	int get_ID() const 
		{return i;}
	bool operator< (const Thing& rhs) const
		{return i < rhs.i;}
	void increment()
		{i++;}
	friend ostream& operator<< (ostream& os, const Thing& t);
private:
	int i;
};

/* function prototypes */
ostream& operator<< (ostream& os, const Thing& t);

void demo_with_const_int();
void demo_with_int_ptr();
void demo_with_cstr_ptr();
void demo_with_Things_default_comparison();
void demo_with_Things_custom_comparison();
void test_apply_if_with_Things();

template<typename T>
void print(Ordered_list<T>& in_list); 
void print_int(const int& i);
void print_int_char(const int& i, char c);
template<typename T>
void print_ptr(Ordered_list<T>& in_list); 
// for clarity, use a typedef putting the const "inside"
typedef int * int_ptr_t;
bool compare_int_ptrs(const int_ptr_t& data_ptr1, const int_ptr_t& data_ptr2);
// for clarity, use a typedef putting the const "inside"
typedef char const * char_ptr_t;
bool compare_c_string_ptrs(const char_ptr_t& data_ptr1, const char_ptr_t& data_ptr2);
bool compare_Things_rev(const Thing& t1, const Thing& t2);
bool match_Thing2(const Thing& t);
bool match_Thing_int(const Thing& t, int i);


int main(void)
{
	demo_with_const_int();
	cout << "Done!" << endl;
	
	return 0;
}

void demo_with_const_int()
{	
	cout << "\ndemo_with_const_int" << endl;
	Ordered_list<const int> int_list;
	
	int_list.insert(3); 
	cout << "int_list size is " << int_list.size() << endl;
	print(int_list);
	
	int_list.insert(1); 
	cout << "int_list size is " << int_list.size() << endl;
	print(int_list);
	
	int_list.insert(2); 
	cout << "int_list size is " << int_list.size() << endl;
	print(int_list);
	
	int_list.apply(print_int);
	cout << endl;
	
	Ordered_list<const int> int_list2(int_list);
	
	cout << "Copied the list\n";
	
	int_list2.apply(print_int);
	cout << endl;
	
	int_list.clear();
	cout << "int_list size is " << int_list.size() << endl;

	int_list2.apply(print_int);
	cout << endl;
	
	int_list2.clear();
	cout << "int_list2 size is " << int_list2.size() << endl;
}	
	


// these are required to have const int& to match the template declarations
void print_int(const int& i)
{
	cout << i << endl;
}

void print_int_char(const int& i, char c)
{
	cout << i << c;
}

// Print the contents of the list on one line, separated by spaces.
// This requires that operator<< be defined for the type of item in the list.
template<typename T>
void print(Ordered_list<T>& in_list)
{
	for(typename Ordered_list<T>::Iterator it = in_list.begin(); it != in_list.end(); it++) {
		if(it != in_list.begin())	// output a leading space after the first one
			cout << ' ';
		cout << *it;
	}
	cout << endl;
}

template<typename T>
void print_ptr(Ordered_list<T>& in_list)
{
	for(typename Ordered_list<T>::Iterator it = in_list.begin(); it != in_list.end(); it++) {
		if(it != in_list.begin())	// output a leading space after the first one
			cout << ' ';
		cout << *(*it);
	}
	cout << endl;
}

bool compare_int_ptrs(const int_ptr_t& data_ptr1, const int_ptr_t& data_ptr2)
{
	return (*data_ptr1 < *data_ptr2);
}

bool compare_Things_rev(const Thing& t1, const Thing& t2)
{
	// compare in reverse order
	return t2 < t1;
}

// this function is defined to return true only if the first C-string
// compares less-than (comes before) the second.
bool compare_c_string_ptrs(const char_ptr_t& data_ptr1, const char_ptr_t& data_ptr2)
{
	return (strcmp(data_ptr1, data_ptr2) < 0);
}

ostream& operator<< (ostream& os, const Thing& t)
{
	os << t.i;
	return os;
}

/* Sample output
demo_with_const_int
int_list size is 1
3
int_list size is 2
1 3
int_list size is 3
1 2 3
1
2
3
1:2:3:
Enter an int:3
Found
Enter an int:5
Not found
Enter an int:0
Not found
int_list size is 0

demo_with_int_ptr
int_ptr_list size is 3
1 2 3
Enter an int:2
Found
1 42 3
did you really want to do that?
Enter an int:3
Not found
Enter an int:1
Found
42 42 3
did you really want to do that?

demo_with_cstr_ptr
cp_list size is 3
s1 s2 s3
Enter a string:xx
Not found
Enter a string:s1
Found
Enter a string:s3
Found
cp_list size is 0

demo_with_Things_default_comparison
thing_list size is 3
1 2 3
Enter an int:2
Found - will remove
thing_list size is 2
1 3
Enter an int:5
Not found
Enter an int:1
Found - will remove
thing_list size is 1
3
incremented thing_list
4
thing_list size is 0

demo_with_Things_custom_comparison
thing_list size is 0
thing_list size is 1
3
thing_list size is 2
3 1
thing_list size is 3
3 2 1
Enter an int:5
Not found
Enter an int:0
Not found
Enter an int:
2
Found - will remove
thing_list size is 2
3 1
incremented thing_list
4 2
thing_list size is 0

test_apply_if_with_Things
thing_list size is 3
1 2 3
there is a match for Thing2
Enter an int:3
there is a match for the value 3
Done!
 */

