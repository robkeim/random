// Demonstrate monitoring functions for String class
// NOTICE: Compile and run this with the g++ compiler option -fno-elide-constructors
// This will prevent g++ from optimizing away some of the calls to the constructors
// and thus make the result conform to the normal "official" ideas of when constructors
// are called.
// Example:
// 	g++ -ansi -pedantic -fno-elide-constructors String_demo3.cpp String.cpp
// 	./a.out

#include "String.h"

#include <iostream>
using namespace std;

// this function shows the information about one string
void print_info(const String& s);
// this function outputs the number and memory usage of all strings
void print_all_info();

void test_fn1();
String test_fn2(String s);


int main ()
{
	String::set_messages_wanted(true);

	print_all_info();
	// call a function that will create and destroy a couple of strings
	cout << "\nwatch them go in and out of scope" << endl;
	test_fn1();
	print_all_info();
	
	cout << "\ninitialize and copy-construct" << endl;
	// Initialize with '='
	String arg = "Hello";
	// copy construct
	String str(arg);
	
	String result;
	cout << "\nassign from a C-string" << endl;
	// assign from a C-string
	result = "this was a C-string";
	cout << "\nassign from another String" << endl;
	str = result;
	cout << "done with assignments: " << str << ' ' << result << endl;
	print_all_info();
	
	cout << "\ncall a function with an argument and returned value" << endl;
	// now call a function with a returned value
	result = test_fn2(arg);
	cout << "function call result: " << result << endl;
	
	cout << "\nOK, that's enough" << endl;
	print_all_info();
}


void test_fn1()
{
	cout << "in test_fn1:" << endl;
	String s1("Good morning!");
	cout << s1 << endl;
	String s2("Good night!");
	cout << s2 << endl;
	// check number of strings now existing
	print_all_info();
}

String test_fn2(String s)
{
	cout << "in test_fn2:" << endl;
	// check number of strings now existing
	print_all_info();
	String ss("Your results may vary!");
	cout << "test_fn got " << s << " and is returning: " << ss << endl;
	print_all_info();
	return ss;
}

void print_info(const String& s)
{
		cout << "String contains \""<< s.c_str() << "\", length is " << s.size() << ", allocation is " << s.get_allocation() << endl;
}

void print_all_info()
{
	cout << String::get_number() << " Strings using " << String::get_total_allocation() << " bytes"  << endl;
}

