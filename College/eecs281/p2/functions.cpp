#include <cctype>
#include <fstream>
#include <iostream>
#include <ext/hash_map>
#include <cstdlib>
#include <string>
#include <cctype>

#include "functions.h"

using namespace std;

void parseQuery(string &inputStr)
// REQ: non-empty string
// MOD: the input string
// EFF: removes all characters at beginning/end of string, and takes the substr
//        until the first space
{
	while (!isalpha(inputStr[0])) // remove non-alpha characters at beginning of string
	{
		inputStr = inputStr.substr(1, inputStr.length() - 1);
	}
	
	while (!isalpha(inputStr[inputStr.length() - 1])) // remove non-alpha characters at end of string
	{
		inputStr = inputStr.substr(0, inputStr.length() - 1);
	}
	
	for (int ii = 0; ii < inputStr.length(); ii++) // delimits input at first whitespace
	{
		if ((inputStr[ii] == ' ') || (inputStr[ii] == '\t'))
		{
			inputStr = inputStr.substr(0, ii);
		}
	}
	
	return;
}

string lowerCase(string mixedCase)
// REQ: none
// MOD: none
// EFF: returns a lower case case equivalent string to the one that's passed into the function
{
	string result;
	for (int i = 0; i < mixedCase.length(); i++)
	{
		mixedCase[i] = tolower(mixedCase[i]);
	}
	return mixedCase;
}

void evaluateWord(__gnu_cxx::hash_map<string, details> &hashMap, ostream &outFile, string &word, int &numWords)
// REQ: valid output file
// MOD: hash map
// EFF: given a word and a hash map, indexes the word into the hash map, otherwise updates information about
//       the number of appearances for a given word
{
 	while (word.substr(0, 1) == "'") // removes all ' at the beginning of the string
 	{
 		word = word.substr(1, word.length() - 1);
 	}
 	while (word.substr(word.length() - 1, 1) == "'") // removes all ' at the end of the string
 	{
 		word = word.substr(0, word.length() - 1);
 	}
 	
 	string lower = lowerCase(word);
	
	bool found = hashMap.count(lower);

	if (found == 0)  // place word in hash map
	{
		details tmp;
			tmp.appear = word;
			tmp.unique = true;
			tmp.wordNum = numWords;
			tmp.appearances = 1;
		hashMap[lower] = tmp;
		outFile << numWords << " ";
		numWords++;
	}
	else  // updates information about entry in hash map
	{
		if (hashMap[lower].unique && (hashMap[lower].appear != word))
		{
			hashMap[lower].unique = false;
		}
		hashMap[lower].appearances++;
		outFile << hashMap[lower].wordNum << " ";
	}	
	
	word = "";
	return;
}

void readFile(__gnu_cxx::hash_map<string, details> &hashMap, const string &inFileName, const string &outFileName)
// REQ: valid input/output file
// MOD: hash map
// EFF: reads/parses the file and builds the hash map
{
	ifstream inFile(inFileName.c_str());
	ofstream outFile(outFileName.c_str());

	string word = "";
	char nextChar;
	bool newWord = true, newLine = true;
	int numWords = 0;
	
	inFile.get(nextChar);

	while (!inFile.eof()) 
	{
		if (nextChar == '.')
		{
			if (word != "")
			{
				evaluateWord(hashMap, outFile, word, numWords);
				outFile << endl;
			}
			else
			{
				outFile << endl;
			}
		}
		else if (nextChar == ' ' || nextChar == '\t' || nextChar == '\n')
		{
			if (word != "")
			{
				evaluateWord(hashMap, outFile, word, numWords);
			}
		}
		else if (isdigit(nextChar))
		{
			if (word != "")
			{
				evaluateWord(hashMap, outFile, word, numWords);
			}
		}
		else if (isalpha(nextChar) || (nextChar == '\''))
		{
			word = word + nextChar;
		}
		else
		{
			if (word != "")
			{
				evaluateWord(hashMap, outFile, word, numWords);
			}
		}
		
		inFile.get(nextChar);
	}	
	
	inFile.close();
	outFile.close();
	
	return;
}

void queryMode(__gnu_cxx::hash_map<string, details> &hashMap)
// REQ: valid filled hash map
// MOD: none
// EFF: enters and input mode and lets the user query about data located in hash map
{
	string input, lower;
	bool found = false;
	
	cout << "Enter word: ";
	getline(cin, input);
	
	while (input != ":quit")
	{
		if (input == "")
		{
			cout << "Appeared 0 times\n";
		}
		else
		{
			parseQuery(input);	
			lower = lowerCase(input);
			found = hashMap.count(lower);
		
			if (found == 0)
			{
				cout << "Appeared 0 times\n";
			}
			else
			{
				if (hashMap[lower].unique == true && (input != hashMap[lower].appear))
				{
					cout << "Appeared " << hashMap[lower].appearances << " times as \"" 
						 << hashMap[lower].appear << "\"\n";
				}
				else
				{
					cout << "Appeared " << hashMap[lower].appearances << " times\n";
				}
			}
		}	
		cout << "Enter word: ";
		getline(cin, input);
	}
	cout << "Quitting\n";
	return;
}

