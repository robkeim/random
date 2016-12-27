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

	// Data structures
	hash_map<string, int> wordMap;
	hash_map<string, int> index;		
	vector <pair<string, vector<bool> > > bitVectors;
	vector<pair<int, pair<string,string> > > clust;
	vector<string> files;
	vector<vector<string> > clusters;
	vector<vector<int> > matrix(26, vector<int> (26, 0));
				
	int numElts = 0,
	     algorithm = atoi(argv[3]);


	// Read files in directory and create bitVectors
	readDir(atoi(argv[1]), argv[4], wordMap, numElts, bitVectors);	
	removeUnused(numElts, bitVectors);

	// Print similarity matrix
	simMatrix(numElts, bitVectors, clust, index, matrix, files);
	
	greedy(atoi(argv[2]), bitVectors, clust, index, matrix, clusters);
	
	if (algorithm == 2)
	{
		simulatedAnnealing(clusters, matrix, index);
	}
	if (algorithm == 3) // i don't consider if the solution returns less than/greater than k clusters
	{
		vector<int> diameters;
		int maxDiam = 0;
		for (int i = 0; i < clusters.size(); i++)
		{
			int diam = 0;
			for (int j = 0; j < clusters.at(i).size(); j++)
			{
				for (int k = j; k < clusters.at(i).size(); k++)
				{
					hash_map <string, int>::iterator It1 = index.find(clusters.at(i).at(j));
					hash_map <string, int>::iterator It2 = index.find(clusters.at(i).at(k));
					int tmpDist = matrix.at(It1 -> second).at(It2 -> second);
					if (tmpDist > diam)
					{
						diam = tmpDist;
					}
				}
			}
			diameters.push_back(diam);	
			if (diam > maxDiam)
			{
				maxDiam = diam;
			}	
		}	
		vector<vector<string> > empty;
		int curDiam = 0;
		branchAndBound(files, clusters, empty, maxDiam, curDiam, matrix, index, atoi(argv[2]));
	}		
	sortClustering(clusters);
	printClusteredMatrix(clusters, matrix, index);
	
	return 0;
}

