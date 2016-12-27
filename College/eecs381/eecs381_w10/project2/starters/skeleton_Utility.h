
/* Utility functions, constants, and classes used by other modules */

// a simple class for error exceptions - msg points to a C-string error message
struct Error {
	Error(const char * in_msg = "") :
		msg(in_msg)
		{}
	const char * msg;
};

// define a function template named "swapem" that interchanges the values of two variables
// use in Ordered_list and String where convenient

/* add any other functions declarations here and define in Utility.cpp */

