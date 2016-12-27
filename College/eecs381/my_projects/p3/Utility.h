#ifndef UTILITY_H
#define UTILITY_H

#include <fstream>
#include <set>

// a simple class for error exceptions - msg points to a C-string error message
struct Error 
{
	Error(const char * in_msg = "") :
		msg(in_msg)
		{ }
	const char * msg;
};

// Try to read an integer from the specified stream.  If the read fails
// reset the stream and throw an Error.
int get_int(std::istream& is);

// Iterate through a container copying the objects into the new destination
//  that evaluate true with respect to the predicate
template <class In, class Out, class Pred>
Out copy_if(In first, In last, Out res, Pred p)
{
	for ( ; first != last; first++)
	{
		if (p(*first))
		{
			*res++ = *first;
		}
	}
	return res;
}

class Record;

typedef Record * Record_ptr_t;

// Compare records based on their title
struct compare_Record_title
{
	bool operator() (const Record_ptr_t &r1, const Record_ptr_t &r2) const;
};

typedef std::set<Record_ptr_t, compare_Record_title>::iterator library_title_it;

#endif

