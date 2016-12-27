#ifndef RECORD_H
#define RECORD_H

/* A Record ontains a unique ID number, assigned when the record is created, a rating, 
and a title and medium name as Strings. Once created, only the rating be modified. */

#include "String.h"

class Record
{
	public:
	// Create a Record object, giving it a unique ID number by first incrementing
	// a static member variable then using its value as the ID number. The rating is set to 0.
	Record(const String& medium_, const String& title_);

	// Create a Record object suitable for use as a probe containing the supplied
	// title. The ID and rating are set to 0, and the medium is an empty String.
	Record(const String& title_);

	// Create a Record object suitable for use as a probe containing the supplied
	// ID number - the static member variable is not modified.
	// The rating is set to 0, and the medium and title are empty Strings.
	Record(int ID_);

	// Construct a Record object from a file stream in save format.
	// Throw Error exception if invalid data discovered in file.
	// No check made for whether the Record already exists or not.
	// Where appropriate, input is read directly into member variables.
	// The record number will be set from the saved data. 
	// The static member variable used for new ID numbers will be set to the saved
	// record ID if the saved record ID is larger than the static member variable value.
	Record(std::ifstream& is);
	
	// Accessors
	int get_ID() const
	{
		return ID;
	}
	
	String get_title() const
	{
		return title;
	}
	
	// reset the ID counter
	static void reset_ID_counter() 
	{
		ID_counter = 1;
		return;
	}
	
	// if the rating is not between 1 and 5 inclusive, an exception is thrown
	void set_rating(int rating_);
	
	// Write a Record's data to a stream in save format with final endl.
	// The record number is saved.
	void save(std::ostream& os) const;

	// This operator defines the order relation between Records, based just on the last title
	bool operator< (const Record& rhs) const
	{
		return title < rhs.title;
	}

	friend std::ostream& operator<< (std::ostream& os, const Record& record);

private:
	static int ID_counter;

	int ID;
	int rating;
	String medium;
	String title;

	// These declarations help ensure that Record objects are unique
	Record(const Record&);	// disallow copy
	Record& operator= (const Record&); // disallow assignment
};

// Print a Record's data to the stream without a final endl. 
// Output order is ID number followed by a ':' then medium, rating, title, separated by one space.
// If the rating is zero, a 'u' is printed instead of the rating.
std::ostream& operator<< (std::ostream& os, const Record& record);

#endif

