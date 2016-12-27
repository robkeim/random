#include "Ordered_list.h"

#include <iostream>

using namespace std;

void demo_with_const_int();

template<typename T>
void print(Ordered_list<T>& in_list); 

void print_int(const int& i);

int main(void)
{
	demo_with_const_int();
	cout << "Done!" << endl;
	
	return 0;
}

void demo_with_const_int()
{	
	Ordered_list<const int> a;
	Ordered_list<const int> b;
	
	for (int i = 0; i < 5; i++)
	{
		a.insert(i);
	}
	
	for (int i = 0; i < 10; i++)
	{
		b.insert(i);
	}
	
	b = a;
	
	a.insert(-1);
	cout << "List A =\n";
	print(a);
	cout << "List B =\n";
	print(b);
	
	/*Ordered_list<const int> int_list;
	
	cout << "\nDelete with end( )\n";
	int_list.erase(int_list.end());
	
	Ordered_list<const int>::Iterator it = int_list.find(-1);
	
	if (it != int_list.end())
	{
		cout << "ERROR: Found element in empty list\n";
	}
	
	int_list.insert(3);
	print(int_list);
	int_list.erase(int_list.find(3));
	
	for (int i = 0; i < 10; i++)
	{
		int_list.insert(i);
	}
	print(int_list);
	Ordered_list<const int> junk(int_list);
	print(junk);
	int_list.clear();
	cout << "Printing copied list when original was cleared\n";
	print(junk);
	cout << "Printing original list\n";
	print(int_list);
	
	for (int i = 0; i < 5; i++)
	{
		int_list.insert(i);
	}
	cout << "Swapping the lists\n";
	int_list.swap(junk);
	print(int_list);
	print(junk);
	
	cout << "Using the assignment operator\n";
	junk = int_list;
	print(int_list);
	print(junk);
	int_list.clear();
	print(junk);*/
	
	/*int_list.insert(3); 
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
	*/
	return;
}	

// these are required to have const int& to match the template declarations
void print_int(const int& i)
{
	cout << i << endl;
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

