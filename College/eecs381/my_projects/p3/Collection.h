#ifndef COLLECTION_H
#define COLLECTION_H

#include <list>
#include <set>

#include "Record.h"
#include "Utility.h"

// Collections contain a name and a container of members,
// represented as pointers to Records.
// Collection objects manage their own Record container. 
class Collection
{
	public:
	// Construct a collection with the specified name and no members
	Collection(const std::string& name_);
	
	// Construct a Collection from an input file stream in save format, using the record list,
	// restoring all the Record information. 
	// Record list is needed to resolve references to record members.
	// No check made for whether the Collection already exists or not.
	// Throw Error exception if invalid data discovered in file.
	// Where appropriate, input is read directly into member variables.
	 Collection(std::ifstream& is, std::set<Record_ptr_t, compare_Record_title> &library_by_title);
	
	// Accessors
	std::string get_name() const
	{
		return name;
	}
		
	// Add the Record, throw exception if there is already a Record with the same title.
	void add_member(Record_ptr_t record_ptr);
	
	// Return true if there are no members; false otherwise
	bool empty() const
	{
		return records.empty();
	}
	
	// Return true if the record is present, false if not.
	bool is_member_present(Record_ptr_t record_ptr) const;
	
	// Remove the specified Record, throw exception if the record was not found.
	void remove_member(Record_ptr_t record_ptr);
	
	// discard all members
	void clear()
	{
		records.clear();
		return;
	}

	// Write a Collections's data to a stream in save format, with endl as specified.
	void save(std::ostream& os) const;

	// This operator defines the order relation between Collections, based just on the name
	bool operator< (const Collection& rhs) const
	{
		return name < rhs.name;
	}
	
	// This operator defines the equality between Collections, based just
	// on the name
	bool operator== (const Collection& rhs) const
	{
		return name == rhs.name;
	}
	
	// This operator defines the inequality between Collections, based just
	// on the name
	bool operator!= (const Collection& rhs) const
	{
		return name != rhs.name;
	}
	
	// Return a list of all of the members in the collection
	std::list<Record_ptr_t> get_members();
	
	friend std::ostream& operator<< (std::ostream& os, const Collection &collection);
		
private:
	std::string name;
	std::set<Record_ptr_t, compare_Record_title> records;
};

// Print the Collection data
std::ostream& operator<< (std::ostream& os, const Collection &collection);

#endif

