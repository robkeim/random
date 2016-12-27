#include <cstring>
#include <iostream>

#include "String.h"
#include "Utility.h"

using namespace std;

int String::number = 0;
int String::total_allocation = 0;
bool String::messages_wanted = false;

// Default initialization is to contain an empty string; if a non-empty
// C-string is supplied, this String gets minimum allocation.
String::String(const char * in_cstr)
{
	if (messages_wanted)
	{
		cout << "Ctor: \"" << in_cstr << "\"" << endl;
	}

	str = new char[strlen(in_cstr) + 1];
	strcpy(str, in_cstr);
	str_size = strlen(in_cstr);
	str_allocation = str_size + 1;

	number++;
	total_allocation += str_allocation;
}

// This String gets a copy of original's data, and gets minimum allocation.
String::String(const String& original)
{
	if (messages_wanted)
	{
		cout << "Copy ctor: \"" << original.c_str() << "\"" << endl;
	}

	str = new char[strlen(original.str) + 1];
	strcpy(str, original.str);
	str_size = strlen(original.str);
	str_allocation = str_size + 1;

	number++;
	total_allocation += str_allocation;

	return;
}

// deallocate C-string memory
String::~String()
{
	if (messages_wanted)
	{
		cout << "Dtor: \"" << str << "\"" << endl;
	}
	number--;
	total_allocation -= str_allocation;
	
	delete[] str;
	str = 0;
	
	str_size = 0;
	str_allocation = 0;	
	
	return;
}

// Left-hand side gets a copy of rhs data and gets minimum allocation.
// This operator use the copy-swap idiom for assignment.
String& String::operator= (const String& rhs)
{
	if (messages_wanted)
	{
		cout << "Assign from String:  \"" << rhs.c_str() << "\"" << endl;
	}

	String tmp(rhs);
	swap(tmp);

	return *this;
}

// This operator creates a temporary String object from the rhs C-string, and swaps the contents
String& String::operator= (const char * rhs)
{
	if (messages_wanted)
	{
		cout << "Assign from C-string:  \"" << rhs << "\"" << endl;
	}
	
	String tmp(rhs);
	swap(tmp);
	
	return *this;
}

// Return a reference to character i in the string.
// Throw exception if 0 <= i < size is false.
char& String::operator[] (int i)
{
	if (!(0 <= i && i < size()))
	{
		throw String_exception("Subscript out of range");
	}
	
	return str[i];
}

// const version for const Strings
const char& String::operator[] (int i) const
{
	if (!(0 <= i && i < size()))
	{
		throw String_exception("Subscript out of range");
	}
	
	return str[i];	
}

// Return a String starting with i and extending for len characters
// The substring must be contained within the string.
// Values of i and len for valid input are as follows:
// i >= 0 && len >= 0 && i <= size && (i + len) <= size.
// If both i = size and len = 0, the input is valid and the result is an empty string.
// Throw exception if the input is invalid.
String String::substring(int i, int len) const
{
	if (!(i >= 0 && len >= 0 && i <= size() && (i + len) <= size()))
	{
		throw String_exception("Substring bounds invalid");
	}
	
	char *substr = new char[len + 1];
	for (int j = 0; j < len; j++)
	{
		substr[j] = str[i + j];
	}
	substr[len] = 0;

	String final = String(substr);
	delete[] substr;

	return final;
}	

// Set to an empty string with minimum allocation by create/swap with an empty string.
void String::clear()
{
	String tmp("");
	swap(tmp);
	
	return;
}

// Remove the len characters starting at i; allocation is unchanged.
// The removed characters must be contained within the String.
// Valid values for i and len are the same as for substring.
void String::remove(int i, int len)
{
	if (!(i >= 0 && len >= 0 && i <= size() && (i + len) <= size()))
	{
		throw String_exception("Remove bounds invalid");
	}

	for (int j = i; j < str_size - len; j++)
	{
		str[j] = str[j + len];
	}
	str[str_size - len] = 0;

	str_size -= len;
	
	return;	
}

// Insert the supplied source String before character i
// Pushing the rest of the contents back, reallocating as needed.
// If i == size, the inserted string is added to the end of this String.
// This String retains the final allocation.
// Throw exception if 0 <= i <= size is false
void String::insert_before(int i, const String& src)
{
	if (!(0 <= i && i <= size()))
	{
		throw String_exception("Insertion point out of range");
	}
	
	int len_to_add = src.size();
	
	if (str_size + len_to_add >= str_allocation)
	{
		total_allocation -= str_allocation;

		str_allocation = 2 * (str_size + len_to_add + 1);
		char *tmp = new char[str_allocation];

		total_allocation += str_allocation;

		strcpy(tmp, str);
		delete[] str;
		str = tmp;
	}
	
	for (int j = str_size; j >= i; j--)
	{
		str[j + len_to_add] = str[j]; 
	}
	str[str_size + len_to_add] = 0;
	
	for (int j = 0; j < len_to_add; j++)
	{
		str[i + j] = src[j];
	}
	
	str_size += len_to_add;
	
	return;
}	

// These concatenation operators reallocate memory for their left-hand operand
// as needed, and leave the String with the final allocation
String& String::operator += (char rhs)
{
	if (str_allocation <= str_size + 1)
	{
		total_allocation -= str_allocation;

		str_allocation = 2 * (str_size + 1 + 1);
		char *tmp = new char[str_allocation];
		
		total_allocation += str_allocation;
		
		strcpy(tmp, str);
		delete[] str;
		str = tmp;
	}
	
	str[str_size] = rhs;
	str_size++;
	str[str_size] = 0;
	
	return *this;
}

// These concatenation operators reallocate memory for their left-hand operand
// as needed, and leave the String with the final allocation
String& String::operator += (const char * rhs)
{
	int rhs_len = strlen(rhs);
	if (str_allocation <= str_size + rhs_len)
	{
		total_allocation -= str_allocation;

		str_allocation = 2 * (str_size + rhs_len + 1);
		char *tmp = new char[str_allocation];
		
		total_allocation += str_allocation;

		strcpy(tmp, str);
		delete[] str;
		str = tmp;
	}
	
	strcpy(str + strlen(str), rhs);
	str_size += rhs_len;
	str[str_size] = 0;
	
	return *this;
}

// These concatenation operators reallocate memory for their left-hand operand
// as needed, and leave the String with the final allocation
String& String::operator += (const String& rhs)
{
	int rhs_len = rhs.size();
	if (str_allocation <= str_size + rhs_len)
	{
		total_allocation -= str_allocation;

		str_allocation = 2 * (str_size + rhs_len + 1);
		char *tmp = new char[str_allocation];
		
		total_allocation += str_allocation;
		
		strcpy(tmp, str);
		delete[] str;
		str = tmp;
	}
	
	strcpy(str + strlen(str), rhs.str);
	str_size += rhs_len;
	str[str_size] = 0;

	return *this;
}

// Swap the contents of this string with another one.
// The member variable values are interchanged, along with the
// pointers to the allocated C-strings, but the two C-strings
// are neither copied nor modified. No memory allocation/deallocation is done.
void String::swap(String& other)
{
	swapem(str, other.str);
	swapem(str_size, other.str_size);
	swapem(str_allocation, other.str_allocation);

	return;
}

// compare lhs and rhs strings; constructor will convert a C-string literal to a String.
// comparison is based on std::strcmp result compared to 0
bool operator== (const String& lhs, const String& rhs)
{
	if (strcmp(lhs.c_str(), rhs.c_str()))
	{
		return false;	
	}
	
	return true;
}

// compare lhs and rhs strings; constructor will convert a C-string literal to a String.
// comparison is based on std::strcmp result compared to 0
bool operator!= (const String& lhs, const String& rhs)
{
	if (!strcmp(lhs.c_str(), rhs.c_str()))
	{
		return false;
	}
	
	return true;
}

// compare lhs and rhs strings; constructor will convert a C-string literal to a String.
// comparison is based on std::strcmp result compared to 0
bool operator< (const String& lhs, const String& rhs)
{
	if (strcmp(lhs.c_str(), rhs.c_str()) >= 0)
	{
		return false;
	}
	
	return true;
}

// compare lhs and rhs strings; constructor will convert a C-string literal to a String.
// comparison is based on std::strcmp result compared to 0
bool operator> (const String& lhs, const String& rhs)
{
	if (strcmp(lhs.c_str(), rhs.c_str()) <= 0)
	{
		return false;
	}
	
	return true;
}

// Concatenate a String with another String.
// If one of the arguments is a C-string, the String constructor will automatically create
// 	a temporary String for it to match this function (inefficient, but instructive).
// This automatic behavior would be disabled if the String constructor was declared "explicit".
// This function constructs a copy of the lhs, then concatenates the rhs to it with operator +=, and returns it.
String operator+ (const String& lhs, const String& rhs)
{
	String tmp(lhs);
	tmp += rhs;

	return tmp;
}

// input and output operators and functions
// The output operator writes the contents of str to the stream
std::ostream& operator<< (std::ostream& os, const String& str)
{
	os << str.c_str();
	return os;
}

// The input operator empties the supplied string, then starts reading the stream.
// It skips initial whitespace, then copies characters into
// the supplied str until whitespace is encountered again. The terminating
// whitespace remains in the input stream, analogous to how input normally works.
// str is expanded as needed, and retains the final allocation.  
// If the input stream fails, str contains whatever characters were read.
std::istream& operator>> (std::istream& is, String& str)
{
	char c;
	
	str = "";
	
	is >> c;
	str += c;
	
	while (!isspace(is.peek()) && is.peek() != EOF)
	{
		is >> c;
		str += c;
	}

	return is;
}

// getline sets str to an empty string, then reads characters into str until it finds a '\n', 
// which is copied into str.
// str's allocation is expanded as needed, and it retains the final allocation.
// If the input stream fails, str contains whatever characters were read.
std::istream& getline(std::istream& is, String& str)
{
	str = "";
	
	while (is.peek() != '\n' && is.peek() != EOF)
	{
		str += is.get();
	}

	return is;
}

