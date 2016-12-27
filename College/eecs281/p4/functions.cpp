#include "functions.h"

bool comp(pair<string, vector<bool> > &pair1, pair<string, vector<bool> > &pair2)
// merge compare function
{
	return (pair1.first < pair2.first);
}

struct fileClust{
	int clustNum;
	string fileName;
	int fileNum;
};

bool compClust(fileClust &c1, fileClust &c2)
// merge compare function
{
	return (c1.fileName < c2.fileName);
}

bool compInt(pair<int, pair<string, string> > &tmp1, pair<int, pair<string, string> > &tmp2)
// merge compare function
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
	

	T* result = new T[n1 + n2]; // This was not created and unallocated memory was trying to be accessed
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
	
	return; // I added an explicit return, a good compiler should fix this on it's own
		//   however, expliciting adding it increases platform independence
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

int findDist(string file1, string file2, vector<pair<int, pair<string, string> > > &clusters)
// given two filenames looks up and returns distance between them
{
	int found = 0, dist = -1, count = 0;
	while (!found)
	{
		if (clusters.at(count).second.first == file1 && clusters.at(count).second.second == file2)
		{
			dist = clusters.at(count).first;
			found = 1;
		}
		if (clusters.at(count).second.first == file2 && clusters.at(count).second.second == file1)
		{
			dist = clusters.at(count).first;
			found = 1;
		}	
		count++;
	}
	return dist;
}

void clustMatrix(const int K, vector <pair<string, vector<bool> > > &bitVectors, vector<pair<int, pair<string, string> > > &clusters)
{

	int numFiles = bitVectors.size();
	if (K > numFiles)
	{
		cout << "\nK specified too large\n";
		return;
	}

	fileClust tmp;
	vector<vector<fileClust> > clustVec;
	vector<fileClust> tmpVec;
	
	for (int i = 0; i < numFiles; i++)
	{
		tmpVec.clear();
		tmp.clustNum = i;
		tmp.fileName = bitVectors.at(i).first;
		tmp.fileNum = i + 1;
		tmpVec.push_back(tmp);
		clustVec.push_back(tmpVec);
	}
	
	int extra = numFiles - K, count = 0;
	while (extra > 0)
	{
		string tmp1 = clusters.at(count).second.first,
		       tmp2 = clusters.at(count).second.second;
		int pos1 = -1, pos2 = -1, i = 0;
		
		while (pos1 == -1 || pos2 == -1)
		{
			for (int j = 0; j < clustVec.at(i).size(); j++)
			{
				if (clustVec.at(i).at(j).fileName == tmp1)
				{
					pos1 = clustVec.at(i).at(j).clustNum;
				}
				if (clustVec.at(i).at(j).fileName == tmp2)
				{
					pos2 = clustVec.at(i).at(j).clustNum;
				}
			}	
			i++;
		}
		if (pos1 < pos2)
		{
			while (!clustVec.at(pos2).empty())
			{
				tmp = clustVec.at(pos2).back();
				tmp.clustNum = pos1;
				clustVec.at(pos1).push_back(tmp);
				clustVec.at(pos2).pop_back();
			}
			extra--;
		}
		else if (pos2 > pos1)
		{
			while (!clustVec.at(pos1).empty())
			{
				tmp = clustVec.at(pos1).back();
				tmp.clustNum = pos2;
				clustVec.at(pos2).push_back(tmp);
				clustVec.at(pos1).pop_back();
			}
			extra--;
		}
		else
		{
			// Do nothing because files are already in same cluster
		}
		count++;
	}	
	
	for (int i = 0; i < numFiles; i++) // Sort the individual clusters
	{
		if (clustVec.at(i).size() > 1)
		{
			mergesort(clustVec.at(i).begin(), clustVec.at(i).end(), compClust);
		}
	}

	cout << "\nClustered similarity matrix:\n";	
	
	char letter = 'A';
	count = 0;
	int clust = 0;
	cout.setf(ios::left);
	cout << setw(20) << "";
	
	vector<string> files;
	vector<string> files2;
	
	while (count < numFiles)
	{
		while (clustVec.at(clust).empty())
		{
			clust++;
		}
		for (int i = 0; i < clustVec.at(clust).size(); i++)
		{
			string tmp = letter + itos(clustVec.at(clust).at(i).fileNum);
			cout << setw(6) << tmp;
			tmp += " " + clustVec.at(clust).at(i).fileName;
			files.push_back(tmp);
			tmp = clustVec.at(clust).at(i).fileName;
			files2.push_back(tmp);
		}
		count = count + clustVec.at(clust).size();
		letter++;
		clust++;
	}	
	cout << endl;
	
	for (int i = 0; i < files.size(); i++)
	{
		cout << setw(20) << files.at(i);
		for (int j = 0; j < files.size(); j++)
		{
			if (i == j)
			{
				cout << setw(6) << "-";
			}
			else
			{
				int dist = findDist(files2.at(i), files2.at(j), clusters);
				cout << setw(6) << dist;
			}
		}
		cout << endl;
	}
	
	cout.unsetf(ios::left);
	return;
}

void simMatrix(int numElts, vector <pair<string, vector<bool> > > &bitVectors, vector<pair<int, pair<string, string> > > &clusters)
{
	// This could be a problem if we were given more than 26 files (I originally mis-interpreted the spec and thought that there
	//   could only be 26 files, however, this was adequate for the bounds of our project but could be changed to handle larger cases
	int matrix[26][26];
	for (int i = 0; i < 26; i++)
	{
		for (int j = 0; j < 26; j++)
		{
			matrix[i][j] = 0;
		}
	}

	mergesort(bitVectors.begin(), bitVectors.end(), comp);

	pair<string, string> tmpFiles;
	pair<int, pair<string, string> > tmpPair;

	for (int i = 0; i < bitVectors.size(); i++)
	{
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
			matrix[i][j] = tmpPair.first;
			clusters.push_back(tmpPair);
		}
	}

	mergesort(clusters.begin(), clusters.end(), compInt);

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
		cout << setw(20) << name;
		for (int j = 0; j < bitVectors.size(); j++)
		{
			if (i < j)
			{
				cout << setw(6) << matrix[i][j];
			}
			else if (i > j)
			{
				cout << setw(6) << matrix[j][i];
			}
			else
			{
				cout << setw(6) << "-";
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
// This could be implemented more directly to save time by removing the what would be recursive elements
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
