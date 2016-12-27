#include "Collection.h"

#include <fstream>
#include <iostream>

#include "Ordered_list.h"
#include "Record.h"
#include "Utility.h"

using namespace std;

// This is the ordering function for Record *
typedef Record * Record_ptr_t;
bool cmp_Record_title(const Record_ptr_t& r1, const Record_ptr_t& r2)
{
	return *r1 < *r2;
}

// Create a collection using the user specified name
Collection::Collection(const String& name_) :
	name(name_)
{
	Ordered_list<Record *> tmp(cmp_Record_title);
	records = tmp;
	
	return;
}

// Construct a Collection from an input file stream in save format, using the record list,
// restoring all the Record information. 
// Record list is needed to resolve references to record members.
// No check made for whether the Collection already exists or not.
// Throw Error exception if invalid data discovered in file.
// Where appropriate, input is read directly into member variables.
Collection::Collection(ifstream& is, const Ordered_list<Record *>& library)
{
	Ordered_list<Record *> tmp(cmp_Record_title);
	records = tmp;
	
	is >> name;
		
	int num_members = get_int(is);
	
	for (int i = 0; i < num_members; i++)
	{
		is.get();
		
		String name;
		getline(is, name);
		
		Record tmp(name);
		Ordered_list<Record *>::Iterator it = library.find(&tmp);
		
		if (it == library.end())
		{
			throw Error("Invalid data found in file!");
		}
		
		add_member(*it);	
	}
	
	return;
}

// Add the Record, throw exception if there is already a Record with the same title.
void Collection::add_member(Record * record_ptr)
{
	if (is_member_present(record_ptr))
	{
		throw Error("Record is already a member in the collection!");
	}
	
	records.insert(record_ptr);
	
	return;
}

// Return true if the record is present, false if not.
bool Collection::is_member_present(Record * record_ptr) const
{
	return records.find(record_ptr) != records.end();
}

// Remove the specified Record, throw exception if the record was not found.
void Collection::remove_member(Record * record_ptr)
{
	if (!is_member_present(record_ptr))
	{
		throw Error("Record is not a member in the collection!");
	}	

	records.erase(records.find(record_ptr));

	return;
}	

// Write a Collections's data to a stream in save format, with endl as specified.
void Collection::save(std::ostream& os) const
{
	os << name << " " << records.size() << endl;
	
	Ordered_list<Record *>::Iterator it = records.begin();
	while (it != records.end())
	{
		os << (*it) -> get_title() << endl;
		++it;
	}
	
	return;
}

// Print the Collection data
ostream& operator<< (ostream& os, const Collection &collection)
{
	cout << "Collection " << collection.name << " contains:";
	if (collection.records.empty())
	{
		cout << " None" << endl;
		return os;
	}
	else
	{
		cout << endl;
	}
	
	Ordered_list<Record *>::Iterator cur = collection.records.begin();
	while (cur != collection.records.end())
	{
		cout << **cur;
		++cur;
	}
	
	return os;
}

