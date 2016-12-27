#include "Utility.h"

#include "Record.h"

using std::istream;

// Try to read an integer from the specified stream.  If the read fails
// reset the stream and throw an Error.
int get_int(istream& is)
{
	int tmp;
	if(!(is >> tmp))
	{
		is.clear();
		throw Error("Could not read an integer value!");	
	}
	
	return tmp;
}

// Compare records based on their title
bool compare_Record_title::operator() (const Record_ptr_t &r1, const Record_ptr_t &r2) const
{
	return r1->get_title() < r2->get_title();
}

