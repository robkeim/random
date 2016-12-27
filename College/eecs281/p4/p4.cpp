// Rob Keim
// EECS 281 - Project 4: Bit vectors for file similarity

#include "functions.h"

int main(int argc, char* argv[])
{
	if (argc != 4)
	{	
		cout << "\t./p4exe N K directory\n";
		exit(1);
	}

	hash_map<string, int> wordMap;
	int numElts = 0;

	vector <pair<string, vector<bool> > > bitVectors;

	readDir(atoi(argv[1]), argv[3], wordMap, numElts, bitVectors);	

	removeUnused(numElts, bitVectors);

	vector<pair<int, pair<string,string> > > clust;

	simMatrix(numElts, bitVectors, clust);

	if (atoi(argv[2]) > 0)
	{
		clustMatrix(atoi(argv[2]), bitVectors, clust);
	}

	return 0;
}

