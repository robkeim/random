#ifndef UTILITY_H
#define UTILITY_H

#include <fstream>

// a simple class for error exceptions - msg points to a C-string error message
struct Error 
{
	Error(const char * in_msg = "") :
		msg(in_msg)
		{ }
	const char * msg;
};

// define a function template named "swapem" that interchanges the values of two variables
// use in Ordered_list and String where convenient
template <typename T>
void swapem(T& item1, T& item2)
{
	T tmp = item1;
	item1 = item2;
	item2 = tmp;
	
	return;
}

// Try to read an integer from the specified stream.  If the read fails
// reset the stream and throw an Error.
int get_int(std::istream& is);

#endif

