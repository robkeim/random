#include "Collection.h"

#include <algorithm>

using std::endl;
using std::ifstream;
using std::list;
using std::ostream;
using std::set;
using std::string;

// Create a collection using the user specified name
Collection::Collection(const string& name_) :
	name(name_)
{ }

// Construct a Collection from an input file stream in save format, using the record list,
// restoring all the Record information. 
// Record list is needed to resolve references to record members.
// No check made for whether the Collection already exists or not.
// Throw Error exception if invalid data discovered in file.
// Where appropriate, input is read directly into member variables.
Collection::Collection(ifstream& is, set<Record_ptr_t, compare_Record_title> &library_by_title)
{	
	is >> name;
		
	int num_members = get_int(is);
	
	is.get();
	
	for (int i = 0; i < num_members; i++)
	{	
		string name;
		getline(is, name);
		
		Record probe(name);
		library_title_it it = library_by_title.find(&probe);
		
		if (it == library_by_title.end())
		{
			throw Error("Invalid data found in file!");
		}
		
		add_member(*it);	
	}
	
	return;
}

// Add the Record, throw exception if there is already a Record with the same title.
void Collection::add_member(Record_ptr_t record_ptr)
{
	if (is_member_present(record_ptr))
	{
		throw Error("Record is already a member in the collection!");
	}
	
	records.insert(record_ptr);
	
	return;
}

// Return true if the record is present, false if not.
bool Collection::is_member_present(Record_ptr_t record_ptr) const
{
	return records.find(record_ptr) != records.end();
}

// Remove the specified Record, throw exception if the record was not found.
void Collection::remove_member(Record_ptr_t record_ptr)
{
	if (!is_member_present(record_ptr))
	{
		throw Error("Record is not a member in the collection!");
	}	

	records.erase(records.find(record_ptr));

	return;
}	

// This function object is used by Collection::save(ostream& os)
struct print_Record_title
{
	print_Record_title(ostream &os_) :
		os(os_)
	{ }
	
	void operator() (const Record_ptr_t &r1) const
	{
		os << r1->get_title() << endl;
		return;
	}
	
	private:
		ostream &os;
};

// Write a Collections's data to a stream in save format, with endl as specified.
void Collection::save(ostream& os) const
{
	os << name << " " << records.size() << endl;

	for_each(records.begin(), records.end(), print_Record_title(os));
	
	return;
}

// This function object is used by Collection::get_members()
struct add_to_list
{
	add_to_list(list<Record_ptr_t> *record_list_) :
		record_list(record_list_)
	{ }
	
	void operator() (Record_ptr_t record)
	{
		record_list->push_back(record);
		
		return;
	}
	
	list<Record_ptr_t> *record_list;
};

// This returns a list of the Records in the specified collection
list<Record_ptr_t> Collection::get_members()
{
	list<Record_ptr_t> record_list;

	for_each(records.begin(), records.end(), add_to_list(&record_list));

	return record_list;
}

// This function object is used by operator<<
struct print_collection
{
	print_collection(ostream &os_) :
		os(os_)
	{ }
	void operator() (Record_ptr_t record)
    {
    	os << *record;
    
    	return;
    }
	
	private:
		ostream &os;
};

// Print the Collection data
ostream& operator<< (ostream& os, const Collection &collection)
{
	os << "Collection " << collection.name << " contains:";
	if (collection.records.empty())
	{
		os << " None" << endl;
		return os;
	}
	else
	{
		os << endl;
	}
	
	for_each(collection.records.begin(), collection.records.end(), print_collection(os));
	
	return os;
}

