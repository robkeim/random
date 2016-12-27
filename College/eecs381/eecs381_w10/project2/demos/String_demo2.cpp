// demonstrate exception handling
#include "String.h"
#include <iostream>
using namespace std;


// an  exception class
class User_screwed_up {
};

void print_info(const String& s);

String test_fn(String s);

// this little testing harness can be expanded to other operations that use
// indices, like substring, insert, etc.
int main ()
{
	
	while(true) {
	try {
	
		String str, code;
		int i, j;
	
		str = "0123456789";
		cout << "Enter quit, or a test name and two integers, i and j:";
		cin >> code;
		if(code == "quit")
			break;
		cin >> i >> j;
		if (!cin) {
			throw User_screwed_up();
			}
	
		if(code == "[]") {
			cout << "Before: " << str << endl;
			str[i] = str[j];
			cout << "After:  " << str << endl;
			}
		else if(code == "sub") {
			cout << "Source: " << str << endl;
			String sub = str.substring(i, j);
			cout << "Result: " << sub << endl;
			}
		else if(code == "rem") {
			cout << "Before: " << str << endl;
			str.remove(i, j);
			cout << "After:  " << str << endl;
			}
		else if(code == "ins") {
			cout << "Before: " << str << endl;
			String ins("abc");
			for(int k = 0; k < j; k++)
				str.insert_before(i, ins);
			cout << "After:  " << str << endl;
			}
		else
			throw User_screwed_up();
		
		} // end of try block
		catch(User_screwed_up& x) {
			cout << "Excuse me, but you entered something wrong. Please try again." << endl;
			cout << "In the meantime, I will clean up the place a bit." << endl;
			cin.clear();
			while(cin.get() != '\n');
			}
		catch(String_exception& x) {
			cout << x.msg << endl;
			}
			
		// go around again
		}
	cout << "And thank you, for trying out String's exception throwing!" << endl;
}

/* Typical program behavior:

Enter quit, or a test name and two integers, i and j:[] 5 9
Before: 0123456789
After:  0123496789
Enter quit, or a test name and two integers, i and j:[] 5 14
Before: 0123456789
Subscript out of range
Enter quit, or a test name and two integers, i and j:sub 2 3
Source: 0123456789
Result: 234
Enter quit, or a test name and two integers, i and j:sub -5 3
Source: 0123456789
Substring bounds invalid
Enter quit, or a test name and two integers, i and j:sub 7 5
Source: 0123456789
Substring bounds invalid
Enter quit, or a test name and two integers, i and j:rem 0 5
Before: 0123456789
After:  56789
Enter quit, or a test name and two integers, i and j:rem 8 3
Before: 0123456789
Remove bounds invalid
Enter quit, or a test name and two integers, i and j:ins 3 x
Excuse me, but you entered something wrong. Please try again.
In the meantime, I will clean up the place a bit.
Enter quit, or a test name and two integers, i and j:ins 3 3
Before: 0123456789
After:  012abcabcabc3456789
Enter quit, or a test name and two integers, i and j:ins 14 1
Before: 0123456789
Insertion point out of range
Enter quit, or a test name and two integers, i and j:prang 14 23
Excuse me, but you entered something wrong. Please try again.
In the meantime, I will clean up the place a bit.
Enter quit, or a test name and two integers, i and j:quit
And thank you, for trying out String's exception throwing!
*/
