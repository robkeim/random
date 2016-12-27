#ifndef __FUNCTIONS_H__
#define __FUNCTIONS_H__

#include <string>
#include <list>
#include <stdlib.h>
#include <fstream>
#include <iostream>
#include <ext/hash_map>

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

using namespace __gnu_cxx;

void error();

void readDir(const unsigned int N, string dirName, hash_map<string, list<string> > &index, hash_map<string, hash_map<string, int> > &hashMap);

void query(const unsigned int N, hash_map<string, list<string> > &index, hash_map<string, hash_map<string, int> > &hashMap);

#endif /* __FUNCTIONS_H__ */
