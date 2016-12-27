#ifndef __FUNCTIONS_H__
#define __FUNCTIONS_H__

#include <cmath>
#include <dirent.h>
#include <ext/hash_map>
#include <fstream>
#include <iomanip>
#include <iostream>
#include <sstream>
#include <string>
#include <sys/stat.h>

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

using namespace std;
using namespace __gnu_cxx;

string itos(int i);

void printClusteredMatrix(const vector<vector<string> > &clusters, const vector<vector<int> > &matrix, hash_map<string, int> &index);

void sortClustering(vector<vector<string> > &clusters);

void branchAndBound(vector<string> files, vector<vector<string> > &bestClust, vector<vector<string> > &curClust, int &minDiam, int &curDiam, vector<vector<int> > &matrix, hash_map<string, int> &index, const int K);

void simulatedAnnealing(vector<vector<string> > &clusters, vector<vector<int> > &matrix, hash_map<string, int> &index);

void greedy(const int K, vector <pair<string, vector<bool> > > &bitVectors, vector<pair<int, pair<string, string> > > &clustersOld, hash_map<string, int> &index, vector<vector<int> > &matrix, vector<vector<string> > &clusters);

void simMatrix(int numElts, vector <pair<string, vector<bool> > > &bitVectors, vector<pair<int, pair<string, string> > > &clusters, hash_map<string, int> &index, vector<vector<int> > &matrix, vector<string> &files);

void removeUnused(int &numElts, vector <pair<string, vector<bool> > > &bitVectors);

void readDir(const unsigned int N, string dirName, hash_map<string, int> &wordMap, int &numElts, vector <pair<string, vector<bool> > > &bitVectors);

void evalFile(const unsigned int N, string filePath, string fileName, hash_map<string, int> &wordMap, int &numElts, vector <pair<string, vector<bool> > > &bitVectors);

#endif
