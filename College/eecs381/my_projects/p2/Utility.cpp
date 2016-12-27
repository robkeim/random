#include "Utility.h"

#include <fstream>

using namespace std;

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

