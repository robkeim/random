#include <algorithm>
#include <cstring>
#include <iostream>
#include <iterator>
#include <map>
#include <tr1/functional>
#include <vector>

#include "Collection.h"
#include "Utility.h"

using std::bad_alloc;
using std::binary_function;
using std::cin;
using std::cout;
using std::endl;
using std::ifstream;
using std::list;
using std::map;
using std::ofstream;
using std::ostream_iterator;
using std::set;
using std::string;
using std::tr1::bind;
using std::tr1::placeholders::_1;
using std::tr1::ref;
using std::vector;

struct compare_Record_ID
{
	bool operator() (const Record_ptr_t &r1, const Record_ptr_t &r2) const
	{
		return r1->get_ID() < r2->get_ID();
	}
};

typedef set<Record_ptr_t, compare_Record_ID>::iterator library_ID_it_t;
typedef vector<Collection>::iterator catalog_it_t;

struct data_t
{
	set<Record_ptr_t, compare_Record_ID> library_by_ID;
	set<Record_ptr_t, compare_Record_title> library_by_title;
	vector<Collection> catalog;
};

typedef void (*fn_ptr_t)(data_t &);

void fillMap(map<string, fn_ptr_t> &commands);
void add_Collection(data_t &data);
bool collection_exists(data_t &data, string name);
void add_Record_to_Collection(data_t &data);
catalog_it_t get_Collection(data_t &data);
library_ID_it_t get_Record_by_ID(data_t &data, int ID);
void add_Record(data_t &data);
void clear_All(data_t &data);
void clear_data(data_t &data);
void combine_Collection(data_t &data);
bool Record_ptr_equal(Record_ptr_t r1, Record_ptr_t r2);
void clear_Catalog(data_t &data);
void clear_Library(data_t &data);
bool collection_not_empty(Collection collection);
void remove_Record(Record_ptr_t record);
void delete_Collection(data_t &data);
void remove_Record_from_Collection(data_t &data);
void delete_Record(data_t &data);
library_title_it get_Record_by_title(data_t &data);
void find_Record_using_title(data_t &data);
string lower_case(string in);
void find_string(data_t &data);
void list_ratings(data_t &data);
bool compare_Record_rating(const Record_ptr_t &r1, const Record_ptr_t &r2);
void modify_Record_rating(data_t &data);
void modify_title(data_t &data);
void print_allocations(data_t &data);
void print_Collection_using_name(data_t &data);
void print_Catalog(data_t &data);
void print_Library(data_t &data);
void print_Record(Record_ptr_t record);
void find_Record_using_ID(data_t &data);
void quit(data_t &data);
void restore_All(data_t &data);
void save_All(data_t &data);
void save_Record(Record_ptr_t record, ofstream &os);
void save_Collection(Collection collection, ofstream &os);
string get_title();
void clear_stream();

int main()
{
 	data_t data;
	
	map<string, fn_ptr_t> commands;
	fillMap(commands);

	char c1, c2;
	string command;
	fn_ptr_t cmd_ptr;
	
	while (command != "qq")
	{
		cout << "\nEnter command: ";
		
		cin >> c1 >> c2;

		command = c1;
		command += c2;

		try
		{
			cmd_ptr = commands[command];
			if (cmd_ptr)
			{
				cmd_ptr(data);
			}
			else
			{
				cout << "Unrecognized command!" << endl;
				clear_stream();
			}
		}
		catch (Error& error)
		{
			cout << error.msg << endl;
			clear_stream();
		}
		catch (bad_alloc& ba)
		{
			cout << "Failed memory allocation, program terminating!\n";
			clear_data(data);
			return 1;
		}
		catch (...)
		{
			cout << "Unknown error occured, program terminating!\n";
			clear_data(data);
			return 2;
		}
	}

	return 0;
}

// Add every command to the map of function pointers
void fillMap(map<string, fn_ptr_t> &commands)
{
	commands["ac"] = add_Collection;
	commands["am"] = add_Record_to_Collection;
	commands["ar"] = add_Record;
	commands["cA"] = clear_All;
	commands["cc"] = combine_Collection;
	commands["cC"] = clear_Catalog;
	commands["cL"] = clear_Library;
	commands["dc"] = delete_Collection;
	commands["dm"] = remove_Record_from_Collection;
	commands["dr"] = delete_Record;
	commands["fr"] = find_Record_using_title;
	commands["fs"] = find_string;
	commands["lr"] = list_ratings;
	commands["mr"] = modify_Record_rating;
	commands["mt"] = modify_title;
	commands["pa"] = print_allocations;
	commands["pc"] = print_Collection_using_name;
	commands["pC"] = print_Catalog;
	commands["pL"] = print_Library;
	commands["pr"] = find_Record_using_ID;
	commands["qq"] = quit;
	commands["rA"] = restore_All;
	commands["sA"] = save_All;
	
	return;
}

// Add collection with a specified name.  An error occurs if the catalog
// already has a collection with the specified name.
void add_Collection(data_t &data)
{
	string name;
	cin >> name;
	
	Collection probe(name);

	catalog_it_t it = lower_bound(data.catalog.begin(), data.catalog.end(), probe);
	if (it != data.catalog.end() && *it == probe)
	{
		throw Error("Catalog already has a collection with this name!");
	}
	
	data.catalog.insert(it, probe);
	
	cout << "Collection " << name << " added" << endl;
	
	return;
}

// Add a record to a collection.  An error occurs if the collection does not
// exist, an integer cannot be read, an invalid record number is specified,
// or the specified record is already a member of the specified collection.
void add_Record_to_Collection(data_t &data)
{
	catalog_it_t it = get_Collection(data);
	
	int ID = get_int(cin);
	
	library_ID_it_t record_it = get_Record_by_ID(data, ID);

	it -> add_member(*record_it);

	cout << "Member " << ID << " " << (*record_it) -> get_title() << " added" << endl;
	
	return;
}

// This function reads in a name and returns a pointer to the collection
// with that name.  An exception is thrown if no collection with the
// provided name exists.
catalog_it_t get_Collection(data_t &data)
{
	string name;
	cin >> name;
	
	Collection probe(name);
	
	catalog_it_t it = lower_bound(data.catalog.begin(), data.catalog.end(), probe);
	if (it == data.catalog.end() || *it != probe)
	{
		throw Error("No collection with that name!");
	}
	
	return it;
}

// This function takes in an integer and returns a pointer to the
// corresponding record.  An exception is thrown if there does not
// exist a record with the specified ID.
library_ID_it_t get_Record_by_ID(data_t &data, int ID)
{
	Record record_probe(ID);
	library_ID_it_t it = data.library_by_ID.find(&record_probe);
	
	if (it == data.library_by_ID.end())
	{
		throw Error("No record with that ID!");
	}
	
	return it;
}

// Add a record to the library.  An error occurs if the library already has
// a record with the specified name.
void add_Record(data_t &data)
{
	string medium;
	cin >> medium;
	
	string title = get_title();
	
	Record probe(title);
	
	library_title_it it = data.library_by_title.find(&probe);
	
	if (it != data.library_by_title.end())
	{
		throw Error("Library already has a record with this title!");
	}
	
	Record_ptr_t  record = new Record(medium, title);
	
	data.library_by_title.insert(record);
	data.library_by_ID.insert(record);
	
	cout << "Record " << record->get_ID() << " added" << endl;
	
	return;
}

// This clears both the contents of the catalog and the contents of the
// library.
void clear_All(data_t &data)
{
	clear_data(data);

	cout << "All data deleted" << endl;

	return;
}

// Clear all of the data from the catalog and library and reset the program
// to the state when it first starts executing.
void clear_data(data_t &data)
{
	data.catalog.clear();
	
	for_each(data.library_by_title.begin(), data.library_by_title.end(), remove_Record);

	data.library_by_title.clear();
	
	// We've already called the destructors for the pointers to records
	//   so now we just need to clear the other list
	data.library_by_ID.clear();
	
	return;
}

// This function object adds records to a collection
struct add_to_collection
{	
	add_to_collection(Collection *collection_) :
		collection(collection_)
	{ }
	
	void operator() (Record_ptr_t record)
	{
		collection->add_member(record);
	
		return;
	}
	
	Collection *collection;
};

// This takes two collections and combines them into a new collection.  The
// original two collections are uneffected.  The possible errors are no
// Collection with that name, and Catalog already has a collection with
// that name.
void combine_Collection(data_t &data)
{	
	catalog_it_t it = get_Collection(data);
	string c1_name = it -> get_name();
	list<Record_ptr_t> c1_records = it -> get_members();
	
	it = get_Collection(data);
	string c2_name = it -> get_name();	
	list<Record_ptr_t> c2_records = it -> get_members();
	
	string new_name;
	cin >> new_name;

	Collection new_collection(new_name);
	
	if (binary_search(data.catalog.begin(), data.catalog.end(), new_collection))
	{
		throw Error("Catalog already has a collection with this name!");
	}
	
	vector<Record_ptr_t > unique;
	set_union(c1_records.begin(), c1_records.end(), c2_records.begin(), c2_records.end(), back_inserter(unique));
	
	for_each(unique.begin(), unique.end(), add_to_collection(&new_collection));
	
	it = lower_bound(data.catalog.begin(), data.catalog.end(), new_collection);
	data.catalog.insert(it, new_collection);
	
	cout << "Collections " << c1_name << " and " << c2_name << " combined into new collection " << new_name << endl;
	
	return;
}

// Compare the title of record pointers
bool Record_ptr_equal(Record_ptr_t r1, Record_ptr_t r2)
{
	return r1->get_title() == r2->get_title();
}

// This clears the contents of the catalog.
void clear_Catalog(data_t &data)
{
	data.catalog.clear();
	
	cout << "All collections deleted" << endl;

	return;
}

// This clears the contents of the current library.  An error occurs if
// there is a collection with one or more members.
void clear_Library(data_t &data)
{
	catalog_it_t it = find_if(data.catalog.begin(), data.catalog.end(), collection_not_empty);
	
	if (it != data.catalog.end())
	{
		throw Error("Cannot clear all records unless all collections are empty!");
	}

	for_each(data.library_by_title.begin(), data.library_by_title.end(), remove_Record);
	data.library_by_title.clear();
	// We've already called the destructors for the pointers to records
	//   so now we just need to clear the other list
	data.library_by_ID.clear();	
	
	cout << "All records deleted" << endl;
	
	return;
}

// Return true if the Collection contains members, false otherwise
bool collection_not_empty(Collection collection)
{
	return !collection.empty();
}

// Delete the contents of a Record
void remove_Record(Record_ptr_t record)
{
	delete record;

	return;
}

// Deletes a specified collection.  An error occurs if a collection with the
// specified name does not exist.
void delete_Collection(data_t &data)
{	
	catalog_it_t it = get_Collection(data);
	string name = it->get_name();
	
	it -> clear();
	data.catalog.erase(it);

	cout << "Collection " << name << " deleted" << endl;
	
	return;
}

// Remove a specified record from a specified collection.  Errors occur if
// an integer cannot be read, there is not a record with the specified ID,
// or a collection does not exist with the specified name.
void remove_Record_from_Collection(data_t &data)
{
	catalog_it_t it = get_Collection(data);
	
	int ID = get_int(cin);
	
	library_ID_it_t record_it = get_Record_by_ID(data, ID);
	
	it -> remove_member(*record_it);
	
	cout << "Member " << ID << " " << (*record_it) -> get_title() << " deleted" << endl;
	
	return;
}

// This function object returns true if the specified record is a member of
// the specified Collection, false otherwise
struct member_present : public binary_function<Collection, Record_ptr_t, bool>
{
	bool operator() (Collection collection, Record_ptr_t record) const
	{
		return collection.is_member_present(record);
	}
};

// Remove a record from the library.  An error occurs if a record with the
// specified title does not exist, or if the specified record is a member of
// one or more collections.
void delete_Record(data_t &data)
{
	library_title_it record_it = get_Record_by_title(data);
	
	Record_ptr_t record = *record_it;
	
	catalog_it_t collection_it = find_if(data.catalog.begin(), data.catalog.end(), bind2nd(member_present(), record));
	
	if (collection_it != data.catalog.end())
	{
		throw Error("Cannot delete a record that is a member of a collection!");
	}

	data.library_by_title.erase(record_it);
	
	record_it = data.library_by_ID.find(record);
	data.library_by_ID.erase(record_it);
	
	cout << "Record " << record -> get_ID() << " " << record -> get_title() << " deleted" << endl;

	delete record;

	return;
}

// This function reads a title and returns a record to the iterator of the
// Record pointer with that title.  An exception is thrown if a Record with
// the specified title doesn't exist.
library_title_it get_Record_by_title(data_t &data)
{
	string title = get_title();
	
	Record probe(title);
	library_title_it it = data.library_by_title.find(&probe);
	
	if (it == data.library_by_title.end())
	{
		throw Error("No record with that title!");
	}
	
	return it;
}

// Find a record with a specified title and print the information about it.
// An error occurs if a record with that name does not exist.
void find_Record_using_title(data_t &data)
{
	library_title_it it = get_Record_by_title(data);
	
	cout << **it;
	
	return;
}

// This function object searches for a matching record, given a supplied
// string, using a case insensitive comparison
struct match_records
{	
	match_records(string word_)
	{ 
		word = lower_case(word_);
	}
	
	bool operator() (Record_ptr_t record)
	{
		string title = lower_case(record->get_title());
		if (title.find(word) != string::npos)
		{
			return true;
		}
	
		return false;
	}
	
	string word;
};

// Return a version of the input string converted to lower case
string lower_case(string in)
{
	string final;

	transform(in.begin(), in.end(), back_inserter(final), tolower);
	
	return final;
}

// This function takes a string and searches the library for titles that
// contain the supplied string as part of the title using a case
// insensitive search. Errors are no records contain that string.
void find_string(data_t &data)
{
	string word;
	cin >> word;
	
	vector<Record_ptr_t> records;
	
	copy_if(data.library_by_title.begin(), data.library_by_title.end(), back_inserter(records), match_records(word));
	
	if (!records.size())
	{
		throw Error("No records contain that string!");
	}
	
	for_each(records.begin(), records.end(), print_Record);
	
	return;
}

// Sort and print all of the Records in descending order of rating.  For
// Records with equal rating, they are sorted alphabetically by title.
void list_ratings(data_t &data)
{
	vector<Record_ptr_t> records(data.library_by_title.size());
	
	copy(data.library_by_title.begin(), data.library_by_title.end(), records.begin());
	sort(records.begin(), records.end(), compare_Record_rating);
	
	if (!records.size())
	{
		cout << "Library is empty" << endl;
		return;
	}

	for_each(records.begin(), records.end(), print_Record);	

	return;
}

// Compare two records first based on their ID, and then for Records with
// the same ID, they are compared by title.
bool compare_Record_rating(const Record_ptr_t &r1, const Record_ptr_t &r2)
{
	if (r1->get_rating() == r2->get_rating())
	{
		return r1->get_title() < r2->get_title();
	}
	
	return r1->get_rating() > r2->get_rating();
}

// Change the rating of a record.  Errors occur if an integer cannot be read,
// a record with the specified ID does not exist, or the value of the new
// rating is not in the valid rating range.
void modify_Record_rating(data_t &data)
{
	int ID = get_int(cin);
	
	library_ID_it_t it = get_Record_by_ID(data, ID);
	
	int rating = get_int(cin);
	
	(*it) -> set_rating(rating);
	
	cout << "Rating for record " << ID << " changed to " << rating << endl;
	
	return;
}

// This function object takes an old and new Record and replaces the old
// Record with the new Record if the old Record is present in a Collection
struct replace_record
{
	replace_record(Record_ptr_t old_Record_, Record_ptr_t new_Record_) :
		old_Record(old_Record_), new_Record(new_Record_)
	{ }
	
	void operator() (Collection &collection)
	{
		if(collection.is_member_present(old_Record))
		{
			collection.remove_member(old_Record);
			collection.add_member(new_Record);
		}
	
		return;
	}

	Record_ptr_t old_Record;
	Record_ptr_t new_Record;
};

// This takes in a record number and modifies the title of that record.  An 
// error can occur if there is no record with the specified number, or 
// there already exists a record with the new title.
void modify_title(data_t &data)
{
	int ID = get_int(cin);

	library_ID_it_t ID_it = get_Record_by_ID(data, ID);

	string title = get_title();
	
	Record title_probe(title);
	library_title_it title_it = data.library_by_title.find(&title_probe);
	
	if (title_it != data.library_by_title.end())
	{
		throw Error("Library already has a record with this title!");
	}
	
	Record_ptr_t record = *ID_it;
	
	// Remove the record from the libraries
	data.library_by_ID.erase(ID_it);
	title_it = data.library_by_title.find(record);
	data.library_by_title.erase(title_it);
	
	Record_ptr_t new_Record = new Record(*record);
	new_Record->set_title(title);
	
	for_each(data.catalog.begin(), data.catalog.end(), replace_record(record, new_Record));
	
	// Insert the new record into the libraries
	data.library_by_ID.insert(new_Record);
	data.library_by_title.insert(new_Record);
	
	delete record;
	
	cout << "Title for record " << ID << " changed to " << title << endl; 
	
	return;
}

// Print the current memory allocations, and also displays the number of
// collections and records currently in existance.
void print_allocations(data_t &data)
{
	cout << "Memory allocations:" << endl;
	cout << "Records: " << data.library_by_title.size() << endl;
	cout << "Collections: " << data.catalog.size() << endl;
	
	return;
}

// Prints the contents of a collection with a specified name.  An error
// occurs if a collection of that name does not exist.
void print_Collection_using_name(data_t &data)
{	
	catalog_it_t it = get_Collection(data);
	
	cout << *it;
	
	return;
}

// Prints the contents of the current catalog.
void print_Catalog(data_t &data)
{
	int num_Collections = data.catalog.size();
	
	if (!num_Collections)
	{
		cout << "Catalog is empty" << endl;
		return;
	}
	
	cout << "Catalog contains " << num_Collections << " collections:" << endl;
	copy(data.catalog.begin(), data.catalog.end(), ostream_iterator<Collection>(cout));
	
	return;
}

// Prints the contents of the currently library.
void print_Library(data_t &data)
{
	int num_records = data.library_by_title.size();
	
	if (!num_records)
	{
		cout << "Library is empty" << endl;
		return;
	}
	
	cout << "Library contains " << num_records << " records:" << endl;
	
	for_each(data.library_by_title.begin(), data.library_by_title.end(), print_Record);
	
	return;
}

// Print the contents of a record pointer to standard out
void print_Record(Record_ptr_t record)
{
	cout << *record;
	
	return;
}

// Find a record with a speified ID, and print the information about it.
// An error occurs if an integer cannot be read, or a record with the
// specified ID does not exist.
void find_Record_using_ID(data_t &data)
{
	int ID = get_int(cin);

	library_ID_it_t it = get_Record_by_ID(data, ID);

	cout << **it;
	
	return;
}

// Clean up the program by deleting all dynamically allocated memory, and
// then exit the program.
void quit(data_t &data)
{
	clear_All(data);

	cout << "Done" << endl;
	
	return;
}

// Restore the state of the program from a previous save.  An error occurs
// if the file could not be opened, or the file contains invalid data (which
// would be the case if the specified file has not been generated by the
// program in a previous state.
void restore_All(data_t &data)
{
	string filename;
	cin >> filename;
	
	ifstream is(filename.c_str());
	if (!is)
	{
		throw Error("Could not open file!");
	}

	try
	{
		clear_data(data);
		Record::reset_ID_counter();
	
		int num_Records = get_int(is);

		for (int i = 0; i < num_Records; i++)
		{
			Record_ptr_t record = new Record(is);
			data.library_by_title.insert(record);
			data.library_by_ID.insert(record);
		}

		int num_Collections = get_int(is);

		for (int i = 0; i < num_Collections; i++)
		{
			Collection collection(is, data.library_by_title);
			data.catalog.push_back(collection);
		}
		sort(data.catalog.begin(), data.catalog.end());
	}
	catch (Error &error)
	{
		clear_data(data);
		Record::reset_ID_counter();
		
		throw Error("Invalid data found in file!");
	}

	is.close();
	
	cout << "Data loaded" << endl;
	
	return;
}

// Save the current state of the program to a specified output file.  An
// error occurs if the specified file cannot be opened for writing.
void save_All(data_t &data)
{
	string filename;
	cin >> filename;
	
	ofstream os(filename.c_str());
	if (!os)
	{
		throw Error("Could not open file!");
	}
	
	os << data.library_by_title.size() << endl;
	
	for_each(data.library_by_title.begin(), data.library_by_title.end(), bind(&save_Record, _1, ref(os)));
	
	os << data.catalog.size() << endl;
	
	for_each(data.catalog.begin(), data.catalog.end(), bind(&save_Collection, _1, ref(os)));
	
	os.close();

	cout << "Data saved" << endl;

	return;
}

// Print to the contents of a record to the specified stream
void save_Record(Record_ptr_t record, ofstream &os)
{
	record->save(os);
}

// Print the contents of a collection to the specified stream
void save_Collection(Collection collection, ofstream &os)
{
	collection.save(os);
}

// This function object removes duplicate whitespace in the middle of a title
struct parse_title
{
	parse_title(string * str_) :
		str(str_), space(false)
	{ }
	
	void operator() (char c)
	{
		// Add the character to the string if its not a space, or if its
		//  a space and the last character added was not a space
		if (!isspace(c))
		{
			*str += c;
			space = false;
		}
		else 
		{
			if (!space)
			{
				*str += ' ';
				space = true;
			}
		}
	}	
	
	string * str;
	bool space;
};

// Get a title from standard input.  All initial and trailing whitespace is
// removed and any duplicate whitespace is also removed.
string get_title()
{
	char c;
	cin.get(c);
	
	// Clear the whitespace at the beginning
	while (isspace(c))
	{
		cin.get(c);
	}
	cin.unget();

	string tmp;
	getline(cin, tmp);
	cin.unget();

	string final;
	
	// Copy each character elmininating duplicate spaces
	for_each(tmp.begin(), tmp.end(), parse_title(&final)); 
	
	// Remove the last space at the end
	if(isspace(final[final.size() - 1]))
	{
		final = final.substr(0, final.size() - 1);
	}
	
	return final;
}

// Clear stdin until a newline is reached
void clear_stream()
{
	char c;
	
	cin.get(c);
	while (c != '\n')
	{
		cin.get(c);
	}
	
	return;
}

