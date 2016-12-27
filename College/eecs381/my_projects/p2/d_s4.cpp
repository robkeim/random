#include <iostream>

#include "String.h"

using namespace std;

void print(String str);

int main()
{
	String x = "abcdefg";
	
	// [ ] operator
	cout << "\nTesting the [ ] operator\n";
	cout << "|";
	for (int i = 0; i < x.size(); i++)
	{
		cout << x[i];
	}
	cout << "|\n";
	
	// substring
	cout << "\nTesting substring\n";
	for (int i = 0; i <= x.size(); i++)
	{
		for (int len = 0; i + len <= x.size(); len++)
		{
			print(x.substring(i, len));
		}
	}
	
	// remove
	cout << "\nTesting remove\n";
	for (int i = 0; i <= x.size(); i++)
	{
		for (int len = 0; i + len <= x.size(); len++)
		{
			String x = "abcdefg";
			x.remove(i, len);
			print(x);
		}
	}
	
	// insert
	cout << "\n Testing insert_before\n";
	const String xxx = "---";
	for (int i = 0; i <= x.size(); i++)
	{
		String x = "abcdefg";
		x.insert_before(i, xxx);
		print(x);
	}
	
	return 0;
}

void print(String str)
{
	cout << "|" << str << "|\n";

	return;
}

