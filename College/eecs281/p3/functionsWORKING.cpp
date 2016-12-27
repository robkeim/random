#include <dirent.h>
#include <fstream>
#include <cstdlib>
#include <iostream>
#include <string>
#include <sys/stat.h>
#include <algorithm>

#include "functions.h"

using namespace std;

using namespace __gnu_cxx;

struct relevance{
	string name;
	unsigned long int rel;
};

void error()
{
	cout << "Confused, aborting\n";
	exit(1);
}

void lowerCase(string &str)
{
	for (int i = 0; i < str.length(); i++)
	{
		str[i] = tolower(str[i]);
	}
	return;
}

void parseLine(string &word, string &inputStr)
{
	int i = 0;
	while((i<inputStr.size()) && !isalpha(inputStr.at(i)))
	{
		i++;
	}

	int j = i;
	while((j < inputStr.size()) && (isalpha(inputStr.at(j)) || (inputStr.at(j) == '\'')))
	{
		j++;
	}

	word = inputStr.substr(i, j-i);
	inputStr = inputStr.substr(j, inputStr.length() - j);
	if(word.empty())
	{
		inputStr = word;
		return;
	}
	
	while (word.at(word.length()-1) == '\'') // remove apostrophes at end
	{
		word = word.substr(0,word.length()-1);
	}
	return;
}

void openAndHash(const unsigned int N, string filePath, string fileName, hash_map<string, list<string> > &index, hash_map<string, hash_map<string, int> > &hashMap)
{
	ifstream inFile(filePath.c_str());
	if (!inFile)
		error();
		
	hashMap[fileName];

	string tmp, word;
	
	while (getline(inFile, tmp))
	{
		while (!tmp.empty())
		{
			parseLine(word, tmp);
			lowerCase(word); // watch for segfault here
			if (!word.empty())
			{
				hash_map <string, hash_map<string, int> >::iterator hashIt = hashMap.find(fileName);
				if (hashIt == hashMap.end()) // filename should always be found
					error();

				hash_map <string, int>::iterator fileIt = (hashIt -> second).find(word);	
				if (fileIt == (hashIt -> second).end())
				{
					(hashIt -> second)[word] = 1;
					
					hash_map <string, list<string> >::iterator indexIt = index.find(word);
					
					if (indexIt == index.end())
					{
						index[word];
						hash_map <string, list<string> >::iterator indexIt = index.find(word);
						(indexIt -> second).push_back(fileName);
					}
					else
					{
						(indexIt -> second).push_back(fileName);
					}
				}
				else
				{
					if ((fileIt -> second) < N)
					{
						(fileIt -> second)++;
					}	
				}			
			}
		}
	}	
	inFile.close();
	return;
}

void readDir(const unsigned int N, string localDir, hash_map<string, list<string> > &index, hash_map<string, hash_map<string, int> > &hashMap)
{
	DIR *dir = NULL;
	struct dirent *file = NULL;
	
	dir = opendir(localDir.c_str());
	if (dir == NULL)
		error();
	
	file = readdir(dir);
	
	string filePath = "", fileName = "";
	
	while (file != NULL)
	{
		fileName = file -> d_name;
		
		if (localDir.at(localDir.length() - 1) != '/')
		{
			filePath = localDir + "/" + fileName;
		}	
		else
		{
			filePath = localDir + fileName;
		}
		
		struct stat dirInfo;
		
		if (stat(filePath.c_str(), &dirInfo) == 0)
		{
			if (dirInfo.st_mode & S_IFDIR && (fileName.at(0) != '.'))
			{
				readDir(N, filePath, index, hashMap);
			}
			if (dirInfo.st_mode & S_IFREG)
			{
				int dot = fileName.find(".");
				if ((dot != -1) && (fileName.substr(dot, fileName.length() - dot) == ".txt"))
				{
					openAndHash(N, filePath, fileName, index, hashMap);
				}			
			}
		}	
		file = readdir(dir);	
	}	
	return;
}

void sort(list<relevance> &rel)
{
		list<relevance>::iterator elt1;
		list<relevance>::iterator elt2;
		
		elt1 = rel.begin();
		elt2 = elt1++;
		
		while (elt1 != rel.end()) // check here for issue
		{
			if ((elt1 -> rel) > (elt2 -> rel))
			{
				string tmp;
				int tmp2;
				tmp = (elt1 -> name);
				tmp2 = (elt1 -> rel);
				(elt1 -> name) = (elt2 -> name);
				(elt1 -> rel) = (elt2 -> rel);
				(elt2 -> name) = tmp;
				(elt2 -> rel) = tmp2;
				sort(rel);
				return;
			}
			if ((elt1 -> rel) == (elt2 -> rel))
			{
				if ((elt1 -> name) < (elt2 -> name))
				{
					string tmp = (elt1 -> name);
					(elt1 -> name) = (elt2 -> name);
					(elt2 -> name) = tmp;
					sort(rel);
					return;					
				}
			}
			elt1++;
			elt2++;
		}
	return;
}

void print(list<relevance> rel)
{
	int count = 1;
	
	list<relevance>::iterator it;
	for (it = rel.begin(); it != rel.end(); it++)
	{
		cout << count << " " << it -> name << endl;
		count++;
	}
	return;
}

string firstWord(string &word)
{
	string tmp = "";

	int space = word.find(" ");
	if (space == -1)
	{
		tmp = word;
		string empty;
		word = empty;
	}
	else
	{
		tmp = word.substr(0, space);
		word = word.substr(space + 1, word.length() - space);
	}
	return tmp;
}

void query(const unsigned int N, hash_map<string, list<string> > &index, hash_map<string, hash_map<string, int> > &hashMap)
{
	string searchStr = "", word = "";
	int space;
	bool noResults;
	cout << "Search words: ";
	getline(cin, searchStr);
	
	list<relevance> rel;
	
	while (searchStr != ":quit")
	{
		rel.clear();

		string copyStr = searchStr;
		string first = firstWord(copyStr);
		
		lowerCase(first);

		hash_map <string, list<string> >::iterator indexIt = index.find(first);
		if (indexIt != index.end())
		{
			int size = (indexIt -> second).size();

			list<string>::iterator it = (indexIt -> second).begin();

			for (int i = 0; i < size; i++)
			{
				int theRelevance = 1;
				copyStr = searchStr;
				while ((!copyStr.empty()) && (theRelevance != 0))
				{
					first = firstWord(copyStr);
					if((hashMap.find(*it) -> second).count(first))
					{
						int eltRel = (hashMap.find(*it) -> second).find(first) -> second;
						if (eltRel < N)
						{
							theRelevance *= eltRel;
						}
						else
						{
							theRelevance = 0;
						}
					}
					else
					{
						theRelevance = 0;
					}
				}

				if (theRelevance != 0)
				{
					relevance tmp;
					tmp.name = *it;
					tmp.rel = theRelevance;
					rel.push_back(tmp);					
				}

				it++;
			}

			if(rel.size() == 0)
			{
				cout << "No results\n";
			}else{			
			sort(rel);
			print(rel);
			}
		}
		else
		{
			cout << "No results\n";
		}

		cout << "\nSearch words: ";
		getline(cin, searchStr);
	}
	cout << "Quitting\n";
	return;
}
