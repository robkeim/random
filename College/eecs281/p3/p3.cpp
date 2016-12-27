// Rob Keim
// EECS 281 - Project 3

#include <stdlib.h>
#include <fstream>
#include <iostream>
#include <list>
#include <string>

#include "functions.h"

using namespace std;

using namespace __gnu_cxx;

int main(int argc, char* argv[])
{
	if (argc < 3)
		error();
	
	hash_map<string, list<string> > index;
	hash_map<string, hash_map<string, int> > hashMap;
		
	for (int ii = 2; ii < argc; ii++)
	{
		readDir(atoi(argv[1]), argv[ii], index, hashMap);
	}
	
	query(atoi(argv[1]), index, hashMap);
	
	return 0;
}

