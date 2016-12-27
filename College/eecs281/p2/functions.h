#ifndef __FUNCTIONS_H__
#define __FUNCTIONS_H__

#include <fstream>
#include <iostream>
#include <ext/hash_map>
#include <cstdlib>
#include <string>

using namespace std;

namespace __gnu_cxx
{
	template<> struct hash< std::string >
	{
		size_t operator()( const std::string& x ) const
		{
			return hash< const char* >()( x.c_str() );
		}
	};
}

struct details
{
	string appear;
	bool unique;
	unsigned int wordNum;
	unsigned int appearances;
};

void readFile(__gnu_cxx::hash_map<string, details> &hashMap, const string &inFileName, const string &outFileName);
// REQ: valid input/output file
// MOD: hash map
// EFF: reads/parses the file and builds the hash map

void queryMode(__gnu_cxx::hash_map<string, details> &hashMap); 
// REQ: valid filled hash map
// MOD: none
// EFF: enters and input mode and lets the user query about data located in hash map

#endif /* __FUNCTIONS_H__ */

