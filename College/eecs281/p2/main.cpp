// Rob Keim
// EECS 281
// Project 2: Coding function, fast lookup, gprof

#include <fstream>
#include <iostream>
#include <ext/hash_map>
#include <cstdlib>
#include <string>

#include "functions.h"

using namespace std;

int main(int argc, char* argv[])
{
	__gnu_cxx::hash_map<string, details> hashMap;
	
	if (argc == 3)
	{	
		const string inFile(argv[1]),
				     outFile(argv[2]);
		readFile(hashMap, inFile, outFile);
	}
	if (argc == 4)
	{
		const string inFile(argv[2]),
				     outFile(argv[3]);	
		readFile(hashMap, inFile, outFile);
		queryMode(hashMap);
	}

	return 0;
}

