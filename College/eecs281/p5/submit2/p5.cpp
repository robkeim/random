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
	
	int algorithm = atoi(argv[3]);

	vector <pair<string, vector<bool> > > bitVectors;

	readDir(atoi(argv[1]), argv[4], wordMap, numElts, bitVectors);	

	removeUnused(numElts, bitVectors);

	vector<pair<int, pair<string,string> > > clust;

	hash_map<string, int> index;	
	
	vector<vector<int> > matrix(26, vector<int> (26, 0));
	
	vector<string> files;
	
	simMatrix(numElts, bitVectors, clust, index, matrix, files);
	
	vector<vector<string> > clusters;
	
	greedyAlg(atoi(argv[2]), bitVectors, clust, index, matrix, clusters, algorithm);
	
	if (algorithm == 2)
	{
		//simAnneal(clusters, files, matrix, index);
	}
	
	if (algorithm == 3)
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
		bnb(files, clusters, empty, maxDiam, 0, matrix, index);

vector<pair<string,string> > names;
		cout << "\nClustered similarity matrix:\n";	
	
		char letter = 'A';
		int clust = 0;
		cout.setf(ios::left);
		cout << setw(20) << "";
		for (int i = 0; i < clusters.size(); i++)
		{
			for (int j = 0; j < clusters.at(i).size(); j++)
			{
				hash_map <string, int>::iterator It = index.find(clusters.at(i).at(j));
			
				string tmp = letter + itos((It -> second) + 1);
				cout << setw(6) << tmp;
				tmp += " " + clusters.at(i).at(j);
				pair<string, string> tmpPair;
				tmpPair.first = tmp;
				tmpPair.second = clusters.at(i).at(j);
				names.push_back(tmpPair);
			}
			letter++;
		}
		cout << endl;
		int ctr = 0;
		for (int i = 0; i < names.size(); i++)
		{
			cout << setw(20) << names.at(i).first;
			for (int j = 0; j < names.size(); j++)
			{
				if (i == j)
				{
					cout << setw(6) << "-";
				}
				else
				{
					hash_map <string, int>::iterator It1 = index.find(names.at(i).second);
					hash_map <string, int>::iterator It2 = index.find(names.at(j).second);
					cout << setw(6) << matrix.at(It1->second).at(It2->second);
				}
			}
			cout << endl;
		}
		cout.unsetf(ios::left);	
	}	
	return 0;
}

