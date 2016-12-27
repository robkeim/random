#include "functions.h"

bool compare(pair<string, vector<bool> > &pair1, pair<string, vector<bool> > &pair2)
{
	return (pair1.first < pair2.first);
}

struct fileClust{
	int clustNum;
	string fileName;
	int fileNum;
};

bool compare(string &str1, string &str2)
{
	return (str1 < str2);
}

bool compare(vector<string> &vec1, vector<string> &vec2)
{
	return (vec1.at(0) < vec2.at(0));
}

bool compare(fileClust &c1, fileClust &c2)
{
	return (c1.fileName < c2.fileName);
}

bool compare(pair<int, pair<string, string> > &tmp1, pair<int, pair<string, string> > &tmp2)
{
	if (tmp1.first < tmp2.first)
	{
		return 1;
	}
	else if (tmp1.first > tmp2.first)
	{
		return 0;
	}
	else
	{
		 if (tmp1.second.first < tmp2.second.first)
		{
			return 1;	
		}
		else
		{
			return 0;
		}
	}	
}

template <typename I>
void merge(I b, I mid, I e, bool (*compare)(typename I::value_type &x, typename I::value_type &y)) {

	//Get the lengths of the left and the right part.
	int n1 = mid-b;
	int n2 = e - mid;

	typedef typename I::value_type T;
	T* left = new T[n1];
	T* right = new T[n2];
	int i;

	//populate left and right arrays
	i = 0;
	for(I p = b; i < n1; p++) {
		left[i] = *p;
		i++;
	}
	i = 0;
	for(I p = (mid);i < n2; p++) {
		right[i] = *p;
		i++;
	}
	

	T* result = new T[n1 + n2];
	i = 0;
	int leftctr = 0;
	int rightctr = 0;

	//Merge the two sorted arrays 
	for(; i < n1+n2;i++) {
		if(leftctr < n1 && rightctr < n2) {
			if(compare (left[leftctr], right[rightctr])) {
				result[i] = left[leftctr++];
			}
			else result[i] = right[rightctr++];
		}
		else if (leftctr <n1) result[i] = left[leftctr++];
		else result[i] = right[rightctr++];
	}
	i = 0;

	//Copy the sorted array back into the original array.
	for(I p = b; p != e; p++)
	{

		*p = result[i++];
	}
	
	delete [] left;
	delete [] right;
	delete [] result;
	
	return;
}			

template<typename I>
void mergesort(I b, I e, bool (*compare)(typename I::value_type &x, typename I::value_type &y)) {

	int len = e-b;

	if(len>1) {
		I mid = b + len/2;
		
		//Sort both the halves
		mergesort(b, mid, compare);
		mergesort(mid, e, compare);
		merge(b, mid, e, compare);
	}
	return;
} 

string itos(int i)	
// convert int to string
{
	stringstream s;
	s << i;
	return s.str();
}

void lowerCase(string &str)
// convert string to all lower case
{
	for (int i = 0; i < str.length(); i++)
	{
		str[i] = tolower(str[i]);
	}
	return;
}

void parseLine(string &word, string &inputStr)
// given a line of input find the first word and keep the rest of the string
{
	int i = 0;
	while((i<inputStr.size()) && !isalpha(inputStr.at(i))) // find first alpha character
	{
		i++;
	}

	int j = i;
	while((j < inputStr.size()) && (isalpha(inputStr.at(j)) || (inputStr.at(j) == '\''))) // find first non-alpha and non apostrophe
	{
		j++;
	}

	word = inputStr.substr(i, j - i);
	inputStr = inputStr.substr(j, inputStr.length() - j);
	if(word.empty())
	{
		inputStr = word;
		return;
	}
	
	while (word.at(word.length() - 1) == '\'') // remove apostrophes at end
	{
		word = word.substr(0, word.length() - 1);
	}

	lowerCase(word);

	return;
}

void printClusteredMatrix(const vector<vector<string> > &clusters, const vector<vector<int> > &matrix, hash_map<string, int> &index)
{
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
	return;
}

void sortClustering(vector<vector<string> > &clusters)
{
	// Sort files within cluster
	for (int i = 0; i < clusters.size(); i++)
	{
		mergesort(clusters.at(i).begin(), clusters.at(i).end(), compare);
	}	
	// Sort clusters
	mergesort(clusters.begin(), clusters.end(), compare);
	return;
}

int maxDiameter(vector<vector<string> > &clusters, vector<vector<int> > &matrix, hash_map<string, int> &index)
{
	int maxDiam = INT_MIN;
	for (int i = 0; i < clusters.size(); i++)
	{
		if (clusters.at(i).size() > 1)
		{
			for (int j = 0; j < clusters.at(i).size(); j++)
			{
				for (int k = j; k < clusters.at(i).size(); k++)
				{
					hash_map <string, int>::iterator It1 = index.find(clusters.at(i).at(j));
					hash_map <string, int>::iterator It2 = index.find(clusters.at(i).at(k));
					int tmpDist = matrix.at(It1 -> second).at(It2 -> second);
					if (tmpDist > maxDiam)
					{
						maxDiam = tmpDist;
					}
				}
			}
		}	
	}
	return maxDiam;
}	

void branchAndBound(vector<string> files, vector<vector<string> > &bestClust, vector<vector<string> > &curClust, int &minDiam, int &curDiam, vector<vector<int> > &matrix, hash_map<string, int> &index, const int K)
{
	if (files.size() == 0)
	{
		if ((curDiam < minDiam));// && (curClust.size() == (K + 1))); // not sure if this is the correct way the autograder does it, but it's worth a shot
		{
			sortClustering(curClust);
			bestClust = curClust;
			minDiam = curDiam;
		}
		return;
	}
	else
	{
		string fileName = files.at(0);
							files.erase(files.begin());
		for (int i = 0; i < curClust.size(); i++)
		{
			curClust.at(i).push_back(fileName);
			int checkDiam = 0;
			for (int j = 0; j < curClust.at(i).size(); j++)
			{
				for (int k = j; k < curClust.at(i).size(); k++)
				{
					hash_map <string, int>::iterator It1 = index.find(curClust.at(i).at(j));
					hash_map <string, int>::iterator It2 = index.find(curClust.at(i).at(k));
					int tmp = matrix.at(It1 -> second).at(It2 -> second);
					if (tmp > checkDiam)
					{
						checkDiam = tmp;
					}
				}
			}
			if (checkDiam > curDiam)
			{
				curDiam = checkDiam;
			}			

			if (curDiam <= minDiam)  // only line i modified since the second submission should only help time required
			{
				branchAndBound(files, bestClust, curClust, minDiam, curDiam, matrix, index, K);
			}
			curClust.at(i).pop_back(); // does this need to be here?
			return;
		}

		if (1)
		{
			vector<string> tmp;
			//cout << "problem\n";
			tmp.push_back(fileName);
			curClust.push_back(tmp);
			//files.erase(files.begin());
			branchAndBound(files, bestClust, curClust, minDiam, curDiam, matrix, index, K);
			curClust.pop_back();
		}			
	}	
	return;
}

void simulatedAnnealing(vector<vector<string> > &clusters, vector<vector<int> > &matrix, hash_map<string, int> &index)
{
	vector<string> files;
	for (int i = 0; i < clusters.size(); i++)
	{
		for (int j = 0; j < clusters.at(i).size(); j++)
		{
			files.push_back(clusters.at(i).at(j));
		}	
	}			
	
	for (int iterations = 0; iterations < 1000; iterations++)
	{
		int randClust = rand() % clusters.size();
		int randFile = rand() % files.size();
		     
		string file = files.at(randClust);
		int clust = INT_MIN, col = INT_MIN, potentialDiam = INT_MIN;
		for (int i = 0; i < clusters.size(); i++)
		{
			for (int j = 0; j < clusters.at(i).size(); j++)
			{
				if (clusters.at(i).at(j) == file)
				{
					clust = i;
					col = j;
				}
			}	
		} 	
		
		if (clust == randClust)
		{
			continue; // the file is in the cluster already
		}
		if (clusters.at(clust).size() == 1)
		{
			continue; // there is only one file in the cluster
		}  

		hash_map <string, int>::iterator It1 = index.find(clusters.at(clust).at(col));
		for (int i = 0; i < clusters.at(randClust).size(); i++)
		{
			hash_map <string, int>::iterator It2 = index.find(clusters.at(randClust).at(i));
			int tmp = matrix.at(It1 -> second).at(It2 -> second);
			if (tmp > potentialDiam)
			{
				potentialDiam = tmp;
			}
		}
		int maxDiam = maxDiameter(clusters, matrix, index);	
		
		if (potentialDiam > maxDiam)  // check with probability of reassigning
		{
			double T = static_cast<double>(maxDiam) / 5.0;
			double I = static_cast<double>(potentialDiam) - static_cast<double>(maxDiam);
			double probability = exp(-I/T) * 100;
			double randProb = rand() % 101;
			
			if (randProb < probability)
			{ 
				clusters.at(randClust).push_back(file);
				clusters.at(clust).erase(clusters.at(clust).begin() + col);
				sortClustering(clusters);
			}
		}
		else // automatically reassign it
		{
			clusters.at(randClust).push_back(file);
			clusters.at(clust).erase(clusters.at(clust).begin() + col);
			sortClustering(clusters);
		}		
	}	
	return;
}

void greedy(const int K, vector <pair<string, vector<bool> > > &bitVectors, vector<pair<int, pair<string, string> > > &clustersOld, hash_map<string, int> &index, const vector<vector<int> > &matrix, vector<vector<string> > &clusters)
{
	int numFiles = bitVectors.size(),
	     numClusters = numFiles,
	     minClustNum = INT_MAX,
	     minDist = INT_MAX,
	     tmpMax = INT_MIN;
	vector <string> tmp;
	for (int i = 0; i < bitVectors.size(); i++)
	{
		tmp.clear();
		tmp.push_back(bitVectors.at(i).first);
		clusters.push_back(tmp);
	}	
	while (numClusters > K)
	{
		minClustNum = INT_MAX, minDist = INT_MAX, tmpMax = INT_MIN;
		for (int i = 1; i < clusters.size(); i++)
		{
			tmpMax = INT_MIN;
			for (int j = 0; j < clusters.at(0).size(); j++)
			{
				hash_map <string, int>::iterator It1 = index.find(clusters.at(0).at(j));
				for (int k = 0; k < clusters.at(i).size(); k++)
				{
					hash_map <string, int>::iterator It2 = index.find(clusters.at(i).at(k));
					int tmpDist = matrix.at(It1 -> second).at(It2 -> second);
					if (tmpDist > tmpMax)
					{
						tmpMax = tmpDist;
					}	
				}
			}
			if (tmpMax < minDist)
			{
				minDist = tmpMax;
				minClustNum = i;
			}
			if (tmpMax == minDist)
			{
				mergesort(clusters.at(i).begin(), clusters.at(i).end(), compare);			
				if (clusters.at(i).at(0) < clusters.at(minClustNum).at(0))
				{
					minClustNum = i;
				}
			}			
		}
	
		while (clusters.at(0).size() != 0)
		{
			clusters.at(minClustNum).push_back(clusters.at(0).back());
			clusters.at(0).pop_back();
		}
		clusters.erase(clusters.begin());
		numClusters--;	
	}

	return;
}

void simMatrix(int numElts, vector <pair<string, vector<bool> > > &bitVectors, vector<pair<int, pair<string, string> > > &clusters, hash_map<string, int> &index, vector<vector<int> > &matrix, vector<string> &files)
{	mergesort(bitVectors.begin(), bitVectors.end(), compare);

	pair<string, string> tmpFiles;
	pair<int, pair<string, string> > tmpPair;

	for (int i = 0; i < bitVectors.size(); i++)
	{
		index[bitVectors.at(i).first] = i;
		hash_map <string, int>::iterator wordIt = index.find(bitVectors.at(i).first);
		for (int j = i + 1; j < bitVectors.size(); j++)
		{
			tmpFiles.first = bitVectors.at(i).first;
			tmpFiles.second = bitVectors.at(j).first;
			tmpPair.second = tmpFiles;
			int dist = 0, size = 0;

			if ((bitVectors.at(i).second).size() < (bitVectors.at(j).second).size())
			{
				size = (bitVectors.at(i).second).size();
			}
			else
			{
				size = (bitVectors.at(j).second).size();
			}
			for (int k = 0; k < size; k++)
			{
				if ((bitVectors.at(i).second).at(k) == 1 && (bitVectors.at(j).second).at(k) == 1)
				{
					dist++;
				}
			}
			tmpPair.first = numElts - dist;
			matrix[i][j] = matrix[j][i] = tmpPair.first;
			clusters.push_back(tmpPair);
		}
	}

	mergesort(clusters.begin(), clusters.end(), compare);

	cout.setf(ios::left);
	cout << "Similarity matrix:\n";
	cout << setw(20) << "";
	for (int i = 0; i < bitVectors.size(); i++)
	{
		cout << setw(6) << (i + 1);
	}
	cout << endl;
	for (int i = 0; i < bitVectors.size(); i++)
	{
		string name = itos(i + 1);
		name = name + " " + bitVectors.at(i).first;
		files.push_back(bitVectors.at(i).first);
		cout << setw(20) << name;
		for (int j = 0; j < bitVectors.size(); j++)
		{
			if (i == j)
			{
				cout << setw(6) << "-";
			}
			else
			{
				cout << setw(6) << matrix[i][j];
			}
		}
		cout << endl;
	}
	cout.unsetf(ios::left);

	return;
}

void removeUnused(int &numElts, vector <pair<string, vector<bool> > > &bitVectors)
{
	vector<bool> notFound;
	for (int i = 0; i < bitVectors.size(); i++)
	{
		while (notFound.size() < (bitVectors.at(i).second).size())
		{
			notFound.push_back(0);
		}		
		for (int j = 0; j < (bitVectors.at(i).second).size(); j++)
		{
			if (notFound.at(j) == 0 && (bitVectors.at(i).second).at(j) == 1)
			{
				notFound.at(j) = 1;
			}
		}
	}
	int wordsNotFound = 0;
	for (int i = 0; i < notFound.size(); i++)
	{
		if (notFound.at(i) == 0)
		{
			wordsNotFound++;
		}
	}
	numElts = numElts - wordsNotFound;
	return;
}

void evalFile(const unsigned int N, string filePath, string fileName, hash_map<string, int> &wordMap, int &numElts, vector <pair<string, vector<bool> > > &bitVectors)
{
	ifstream inFile(filePath.c_str());
	if (!inFile)
	{
		cout << "Cannot read file\n";
		exit(1);
	}

	hash_map<string, int> fileMap;
	vector<bool> fileVector;
	string tmp, word;
	
	while (getline(inFile, tmp))
	{
		while (!tmp.empty())
		{
			parseLine(word, tmp);
			lowerCase(word);
			if (!word.empty())
			{
				hash_map <string, int>::iterator wordIt = wordMap.find(word);
				if (wordIt == wordMap.end())
				{
					wordMap[word] = numElts;
					fileMap[word] = 1;
					while (fileVector.size() < numElts)
					{
						fileVector.push_back(0);
					}
					fileVector.push_back(1);
					numElts++;
				}
				else
				{
					hash_map <string, int>::iterator fileIt = fileMap.find(word);
					if (fileIt == fileMap.end())
					{
						fileMap[word] = 1;
						while (fileVector.size() <= (wordIt -> second))
						{
							fileVector.push_back(0);
						}
						fileVector.at(wordIt -> second) = 1;
					}
					else
					{
						if ((fileIt -> second) < (N - 1))
						{
							(fileIt -> second)++;
						}
						else
						{
							fileVector.at((wordIt -> second)) = 0;
						}	
					}
				}
			}
		}
	}	
	inFile.close();

	pair<string, vector<bool> > tmpPair;
	tmpPair.first = fileName;
	tmpPair.second = fileVector;
	bitVectors.push_back(tmpPair);

	return;
}

void readDir(const unsigned int N, string dirName, hash_map<string, int> &wordMap, int &numElts, vector <pair<string, vector<bool> > > &bitVectors)
{
	DIR *dir = NULL;
	struct dirent *file = NULL;
	
	dir = opendir(dirName.c_str());
	if (dir == NULL)
	{
		cout << "Invalid Directory\n";
		exit(1);
	}
	
	file = readdir(dir);
	
	string filePath = "", fileName = "";
	
	while (file != NULL)
	{
		fileName = file -> d_name;
		
		if (dirName.at(dirName.length() - 1) != '/')
		{
			filePath = dirName + "/" + fileName;
		}	
		else
		{
			filePath = dirName + fileName;
		}
		
		struct stat dirInfo;
		
		if (stat(filePath.c_str(), &dirInfo) == 0)
		{
			if (dirInfo.st_mode & S_IFDIR && (fileName.at(0) != '.'))
			{
				// This is used for searching sub-directories recursively
				// readDir(N, filePath, index, hashMap);
			}
			if (dirInfo.st_mode & S_IFREG)
			{
				int dot = fileName.find(".");
				if ((dot != -1) && (fileName.substr(dot, fileName.length() - dot) == ".txt"))
				{
					evalFile(N, filePath, fileName, wordMap, numElts, bitVectors);
				}			
			}
		}	
		file = readdir(dir);	
	}	
	closedir(dir);
	return;
}
