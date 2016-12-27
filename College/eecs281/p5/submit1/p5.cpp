// Rob Keim
// EECS 281 - Project 5: More clustering!

#include "functions.h"



int main(int argc, char* argv[])
{
	if (argc != 5)
	{	
		cout << "\t./p5exe N K A directory\n";
		exit(1);
	}

	hash_map<string, int> wordMap;
	int numElts = 0;

	vector <pair<string, vector<bool> > > bitVectors;

	readDir(atoi(argv[1]), argv[4], wordMap, numElts, bitVectors);	

	removeUnused(numElts, bitVectors);

	vector<pair<int, pair<string,string> > > clust;

	hash_map<string, int> index;	
	
	vector<vector<int> > matrix(26, vector<int> (26, 0));
	
	simMatrix(numElts, bitVectors, clust, index, matrix);
	
	greedyAlg(atoi(argv[2]), bitVectors, clust, index, matrix);

	return 0;
}

