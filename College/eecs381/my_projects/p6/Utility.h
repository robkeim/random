#ifndef UTILITY_H
#define UTILITY_H

// This is an error handling mechanism.  Instances of the Error struct are
// used as exceptions.
struct Error 
{
	Error(const char * in_msg) : 
		msg(in_msg) 
	{ }
	
	const char * msg;
};

#endif
