#include "Record.h"

#include <fstream>
#include <iostream>

#include "Utility.h"

using namespace std;

int Record::ID_counter = 1;

// Create a Record object, giving it a unique ID number by first incrementing
// a static member variable then using its value as the ID number. The rating is set to 0.
Record::Record(const String& medium_, const String& title_) :
	ID(ID_counter++), rating(0), medium(medium_), title(title_)
{ }

// Create a Record object suitable for use as a probe containing the supplied
// title. The ID and rating are set to 0, and the medium is an empty String.
Record::Record(const String& title_) :
	ID(0), rating(0), medium(""), title(title_)
{ }

// Create a Record object suitable for use as a probe containing the supplied
// ID number - the static member variable is not modified.
// The rating is set to 0, and the medium and title are empty Strings.
Record::Record(int ID_) :
	ID(ID_), rating(0), medium(""), title("")
{ }

// Construct a Record object from a file stream in save format.
// Throw Error exception if invalid data discovered in file.
// No check made for whether the Record already exists or not.
// Where appropriate, input is read directly into member variables.
// The record number will be set from the saved data. 
// The static member variable used for new ID numbers will be set to the saved
// record ID if the saved record ID is larger than the static member variable value.
Record::Record(ifstream& is)
{
	ID = get_int(is);
	
	is >> medium >> rating;
	
	if (ID > ID_counter)
	{
		ID_counter = ID + 1;
	}
	
	is.get();
	
	String tmp;
	getline(is, tmp);
	title = tmp;
}
	
void Record::set_rating(int rating_)
{
	if (rating_ < 1 || rating_ > 5)
	{
		throw Error("Rating is out of range!");
	}
	
	rating = rating_;
	
	return;
}	
	
// Write a Record's data to a stream in save format with final endl.
// The record number is saved.

void Record::save(ostream& os) const
{
	os << ID << " " << medium << " " << rating << " " << title << endl;
	
	return;
}	

// Print a Record's data to the stream without a final endl. 
// Output order is ID number followed by a ':' then medium, rating, title, separated by one space.
// If the rating is zero, a 'u' is printed instead of the rating.
ostream& operator<< (ostream& os, const Record& record)
{
	cout << record.ID << ": " << record.medium << ' ';
	if (record.rating)
	{
		cout << record.rating;
	}
	else
	{
		cout << 'u';
	}
	cout << ' ' << record.title << endl;
	
	return os;
}
