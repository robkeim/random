#ifndef UTILITIES_H
#define UTILITIES_H


/* Utility functions and classes used by other modules */

// a simple class for error exceptions - msg points to a C-string error message
struct Error {
	Error(const char * in_msg = "") :
		msg(in_msg)
		{}
	const char * msg;
};

/* your other declarations here */

#endif

