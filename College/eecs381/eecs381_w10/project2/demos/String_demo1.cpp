// demonstrate some of the features of the String class

#include "String.h"
#include <iostream>
using namespace std;

void print_info(const String& s);

String test_fn(String s);


int main ()
{
	// in case there is an exception thrown
	try {

	// constructors
	String s0, s1 ("Tom"), s2 ("Dick"), s3 ("Harry");
	String s4(s3);

	// access internal info
	print_info(s0);
	print_info(s1);
	print_info(s2);
	print_info(s3);
	print_info(s4);
	
	// output operator |'s show where each output starts and stops
	cout << '|' << s0 << '|' << s1 << '|' << s2 << '|' << s3 << '|' << s4 << '|' << endl;

	// internal info and assignment to other String
	print_info(s4);
	s4 = s2;
	print_info(s4);
	
	// assignment to c-string
	s4 = "howdy there, pardner!";
	print_info(s4);
	
	// copy ctor
	cout << "call test_fn with : " << s3 << endl;
	s4 = test_fn(s3);
	s3 = "s3's new data";
	cout << "result from test_fn: " << s4 << endl;

	// comparison operators
	
	if (s1 == s2)
		cout << s1 << " == " << s2 << endl;
	else
		cout << s1 << " != " << s2 << endl;

	if (s1 < s2)
		cout << s1 << " < " << s2 << endl;
	else
		cout << s1 << " >= " << s2 << endl;
		
	if (s1 < "Dick")
		cout << s1 << " < \"Dick\"" << endl;
	else
		cout << s1 << " >=  \"Dick\"" << endl;
		
	if ("Tom" > "Dick")
		cout << "\"Tom\" is greater than \"Dick\"" << endl;
	else 
		cout << "\"Tom\" is not greater than \"Dick\"" << endl;

	// concatenation functions
	String target;
	print_info(target);
	target = target + "abc";
	print_info(target);
	target += 'd';
	print_info(target);
	target += "efg";
	print_info(target);
	target += "hijklmnop";
	print_info(target);
	target += "qrs";
	print_info(target);
	target = target + String("tuv");
	print_info(target);
	target = target + "wxyz";
	print_info(target);
	target = String("456") + target;
	print_info(target);
	target = "123" + target;
	print_info(target);
	cout << "Concatenation results: " << target << endl;
	
	// insert_before
	String str("0123456789");
	print_info(str);
	String ins("abc");
	str.insert_before(2, ins);
	print_info(str);
	str.insert_before(str.size(), ins);	
	print_info(str);

	// remove
	str = "0123456789";
	print_info(str);
	str.remove(0, 5);
	print_info(str);
	str = "0123456789";
	str.remove(9, 1);
	print_info(str);
	
	// substring
	str = "abcdefghij";
	String sub;
	sub = str.substring(0,3);
	print_info(sub);
	sub = str.substring(6, 4);
	print_info(sub);
	
	// subscripting
	str = "0123456789";
	print_info(str);
	str[3] = str[9];
	print_info(str);


	String input;
	
	// input operator
	cout << "String input: ";
	print_info(input);
	cout << "enter three strings on the same or different lines:" << endl;	
	/* enter a short, medium, and long string to see different allocations at work */
	cin >> input;
	cout << "String input: ";
	print_info(input);

	cin >> input;
	cout << "String input: ";
	print_info(input);

	cin >> input;
	cout << "String input: ";
	print_info(input);
	
	// assign input results to another string
	s4 = input;
	print_info(s4);
	
	} // end of try block
	
	catch(String_exception& x) {
		cout << "String exception caught: " << x.msg << endl;
		}
	catch(...) {
		cout << "Unknown exception caught! " << endl;
		}


	cout << "Finished" << endl;
	
	return 0;
}


String test_fn(String s)
{
	cout << "in test_fn:" << endl;
	String ss(s);
	ss += " was inside test_fn";
	cout << "test_fn got " << s << " and is returning: " << ss << endl;
	return ss;
}

void print_info(const String& s)
{
		cout << "String contains \""  << s.c_str() << "\", length is " << s.size() << ", allocation is " << s.get_allocation() << endl;
}

/* output of the above program with some sample inputs

String contains "", length is 0, allocation is 1
String contains "Tom", length is 3, allocation is 4
String contains "Dick", length is 4, allocation is 5
String contains "Harry", length is 5, allocation is 6
String contains "Harry", length is 5, allocation is 6
||Tom|Dick|Harry|Harry|
String contains "Harry", length is 5, allocation is 6
String contains "Dick", length is 4, allocation is 5
String contains "howdy there, pardner!", length is 21, allocation is 22
call test_fn with : Harry
in test_fn:
test_fn got Harry and is returning: Harry was inside test_fn
result from test_fn: Harry was inside test_fn
Tom != Dick
Tom >= Dick
Tom >=  "Dick"
"Tom" is not greater than "Dick"
String contains "", length is 0, allocation is 1
String contains "abc", length is 3, allocation is 4
String contains "abcd", length is 4, allocation is 10
String contains "abcdefg", length is 7, allocation is 10
String contains "abcdefghijklmnop", length is 16, allocation is 34
String contains "abcdefghijklmnopqrs", length is 19, allocation is 34
String contains "abcdefghijklmnopqrstuv", length is 22, allocation is 23
String contains "abcdefghijklmnopqrstuvwxyz", length is 26, allocation is 27
String contains "456abcdefghijklmnopqrstuvwxyz", length is 29, allocation is 30
String contains "123456abcdefghijklmnopqrstuvwxyz", length is 32, allocation is 33
Concatenation results: 123456abcdefghijklmnopqrstuvwxyz
String contains "0123456789", length is 10, allocation is 11
String contains "01abc23456789", length is 13, allocation is 28
String contains "01abc23456789abc", length is 16, allocation is 28
String contains "0123456789", length is 10, allocation is 11
String contains "56789", length is 5, allocation is 11
String contains "012345678", length is 9, allocation is 11
String contains "abc", length is 3, allocation is 4
String contains "ghij", length is 4, allocation is 5
String contains "0123456789", length is 10, allocation is 11
String contains "0129456789", length is 10, allocation is 11
String input: String contains "", length is 0, allocation is 1
enter three strings on the same or different lines:
short
String input: String contains "short", length is 5, allocation is 10
medium_length_string
String input: String contains "medium_length_string", length is 20, allocation is 22
pretty_long_string_but_I_can_think_of_ones_that_would_be_a_lot_longer
String input: String contains "pretty_long_string_but_I_can_think_of_ones_that_would_be_a_lot_longer", length is 69, allocation is 94
String contains "pretty_long_string_but_I_can_think_of_ones_that_would_be_a_lot_longer", length is 69, allocation is 70
Finished

*/