#include <fstream>
#include <iostream>
#include <new>

#include "Collection.h"
#include "Ordered_list.h"
#include "p2_globals.h"
#include "Record.h"
#include "String.h"
#include "Utility.h"

using namespace std;

int num_Records = 0;
int num_Collections = 0;

void print_Collection_using_name(const Ordered_list<Collection> &catalog);
void find_Record_using_title(const Ordered_list<Record *> &library);
void find_Record_using_ID(const Ordered_list<Record *> &library);
void print_Library(const Ordered_list<Record *> &library);
void print_Catalog(const Ordered_list<Collection> &catalog);
void print_allocations();
void add_Record(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID);
void add_Collection(Ordered_list<Collection> &catalog);
void add_Record_to_Collection(const Ordered_list<Record *> &library, Ordered_list<Collection> &catalog);
void modify_Record_rating(Ordered_list<Record *> &library);
void delete_Record(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, const Ordered_list<Collection> &catalog);
void delete_Collection(Ordered_list<Collection> &catalog);
void remove_Record_from_Collection(const Ordered_list<Record *> &library, Ordered_list<Collection> &catalog);
void clear_Library(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, const Ordered_list<Collection> &catalog);
void clear_Catalog(Ordered_list<Collection> &catalog);
void clear_All(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog);
void save_All(const Ordered_list<Record *> &library, const Ordered_list<Collection> &catalog);
void restore_All(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog);
void quit(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog);

String get_title();

typedef Record * Record_ptr_t;
bool compare_Record_ID(const Record_ptr_t& r1, const Record_ptr_t& r2);
bool compare_Record_title(const Record_ptr_t& r1, const Record_ptr_t& r2);

void empty_record_list(Ordered_list<Record *> &list);
void clear_data(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog);

int main()
{
	Ordered_list<Record *> library_by_title(compare_Record_title);
	Ordered_list<Record *> library_by_ID(compare_Record_ID);
	Ordered_list<Collection> catalog;

	bool valid_command;

	char c1 = 0, c2 = 0;
	
	while (c1 != 'q' || c2 != 'q')
	{
		cout << "\nEnter command: ";
		
		cin >> c1 >> c2;

		valid_command = true;

		try
		{
			switch (c1)
			{
				case 'a':
					switch (c2)
					{
						case 'c':
							add_Collection(catalog);
							break;
						case 'm':
							add_Record_to_Collection(library_by_ID, catalog);
							break;
						case 'r':
							add_Record(library_by_title, library_by_ID);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 'c':
					switch (c2)
					{
						case 'A':
							clear_All(library_by_title, library_by_ID, catalog);
							break;
						case 'C':
							clear_Catalog(catalog);
							break;
						case 'L':
							clear_Library(library_by_title, library_by_ID, catalog);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 'd':
					switch (c2)
					{
						case 'c':
							delete_Collection(catalog);
							break;
						case 'm':
							remove_Record_from_Collection(library_by_ID, catalog);
							break;
						case 'r':
							delete_Record(library_by_title, library_by_ID, catalog);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 'f':
					switch (c2)
					{
						case 'r':
							find_Record_using_title(library_by_title);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 'm':
					switch (c2)
					{
						case 'r':
							modify_Record_rating(library_by_ID);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 'p':
					switch (c2)
					{
						case 'a':
							print_allocations();
							break;
						case 'c':
							print_Collection_using_name(catalog);
							break;
						case 'C':
							print_Catalog(catalog);
							break;
						case 'L':
							print_Library(library_by_title);
							break;
						case 'r':
							find_Record_using_ID(library_by_ID);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 'q':
					switch (c2)
					{
						case 'q':
							quit(library_by_title, library_by_ID, catalog);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 'r':
					switch (c2)
					{
						case 'A':
							restore_All(library_by_title, library_by_ID, catalog);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				case 's':
					switch (c2)
					{
						case 'A':
							save_All(library_by_title, catalog);
							break;
						default:
							valid_command = false;
							break;
					}
					break;
				default:
					valid_command = false;
					break;
			}
			if (!valid_command)
			{
				cout << "Unrecognized command!" << endl;
				String tmp;
				getline(cin, tmp);
			}
		}
		catch (Error& error)
		{
			// Print the error and clear the input stream
			cout << error.msg << endl;
			String tmp;
			getline(cin, tmp);
		}
		catch (bad_alloc& ba)
		{
			cout << "Failed memory allocation, program terminating!\n";
			clear_data(library_by_title, library_by_ID, catalog);
			return 0;
		}
	}

	return 0;
}

// Prints the contents of a collection with a specified name.  An error
// occurs if a collection of that name does not exist.
void print_Collection_using_name(const Ordered_list<Collection> &catalog)
{
	String name;
	cin >> name;
	
	Ordered_list<Collection>::Iterator it = catalog.begin();
	while (it != catalog.end())
	{
		if (it -> get_name() == name)
		{
			cout << *it;
			return;
		}
		
		++it;
	}
	
	throw Error("No collection with that name!");
	
	return;
}

// Find a record with a specified title and print the information about it.
// An error occurs if a record with that name does not exist.
void find_Record_using_title(const Ordered_list<Record *> &library)
{
	String title = get_title();
	Record tmp_Record(title);
	Ordered_list<Record *>::Iterator it = library.find(&tmp_Record);
	
	if (it != library.end())
	{
		cout << **it;
	}
	else
	{
		throw Error("No record with that title!");
	}
	
	return;
}

// Find a record with a speified ID, and print the information about it.
// An error occurs if an integer cannot be read, or a record with the
// specified ID does not exist.
void find_Record_using_ID(const Ordered_list<Record *> &library)
{
	int ID = get_int(cin);
	
	Record tmp_Record(ID);
	Ordered_list<Record *>::Iterator it = library.find(&tmp_Record);
	
	if (it == library.end())
	{
		throw Error("No record with that ID!");
	}

	cout << **it;
	
	return;
}

// Prints the contents of the currently library.
void print_Library(const Ordered_list<Record *> &library)
{
	int num_records = library.size();
	
	if (!num_records)
	{
		cout << "Library is empty" << endl;
		return;
	}
	
	cout << "Library contains " << num_records << " records:" << endl;
	Ordered_list<Record *>::Iterator it = library.begin();
	while (it != library.end())
	{
		cout << **it;
		++it;
	}
	
	return;
}

// Prints the contents of the current catalog.
void print_Catalog(const Ordered_list<Collection> &catalog)
{
	int num_collections = catalog.size();
	
	if (!num_collections)
	{
		cout << "Catalog is empty" << endl;
		return;
	}
	
	cout << "Catalog contains " << num_collections << " collections:" << endl;
	Ordered_list<Collection>::Iterator it = catalog.begin();
	while (it != catalog.end())
	{
		cout << *it;
		++it;
	}
	
	return;
}

// Print the current memory allocations, and also displays the number of
// collections and records currently in existance.
void print_allocations()
{
	cout << "Memory allocations:" << endl;
	cout << "Records: " << num_Records << endl;
	cout << "Collections: " << num_Collections << endl;
	cout << "Lists: " << g_Ordered_list_count << endl;
	cout << "List Nodes: " << g_Ordered_list_Node_count << endl;
	cout << "Strings: " << String::get_number() << " with " << String::get_total_allocation() << " bytes total" << endl;
	
	return;
}

// Add a record to the library.  An error occurs if the library already has
// a record with the specified name.
void add_Record(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID)
{
	String medium;
	cin >> medium;
	
	String title = get_title();
	
	Record tmp_Record(title);
	if (library_by_title.find(&tmp_Record) != library_by_title.end())
	{
		throw Error("Library already has a record with this title!");
	}
	
	Record * record = new Record(medium, title);
	
	library_by_title.insert(record);
	library_by_ID.insert(record);
	
	num_Records++;
	
	cout << "Record " << record -> get_ID() << " added" << endl;
	
	return;
}

// Add collection with a specified name.  An error occurs if the catalog
// already has a collection with the specified name.
void add_Collection(Ordered_list<Collection> &catalog)
{
	String name;
	cin >> name;
	
	Ordered_list<Collection>::Iterator it = catalog.begin();
	while (it != catalog.end())
	{
		if (it -> get_name() == name)
		{
			throw Error("Catalog already has a collection with this name!");
		}
		++it;
	}

	catalog.insert(Collection(name));
	
	num_Collections++;
	
	cout << "Collection " << name << " added" << endl;
	
	return;
}

// Add a record to a collection.  An error occurs if the collection does not
// exist, an integer cannot be read, an invalid record number is specified,
// or the specified record is already a member of the specified collection.
void add_Record_to_Collection(const Ordered_list<Record *> &library, Ordered_list<Collection> &catalog)
{
	String name;
	cin >> name;
	
	Collection tmp_Collection(name);
	Ordered_list<Collection>::Iterator collection_it = catalog.find(tmp_Collection);
	
	if (collection_it == catalog.end())
	{
		throw Error("No collection with that name!");
	}
	
	int ID = get_int(cin);
	
	Record tmp_Record(ID);
	Ordered_list<Record *>::Iterator record_it = library.find(&tmp_Record);
	
	if (record_it == library.end())
	{
		throw Error("No record with that ID!");
	}

	collection_it -> add_member(*record_it);

	cout << "Member " << ID << " " << (*record_it) -> get_title() << " added" << endl;
	
	return;
}

// Change the rating of a record.  Errors occur if an integer cannot be read,
// a record with the specified ID does not exist, or the value of the new
// rating is not in the valid rating range.
void modify_Record_rating(Ordered_list<Record *> &library)
{
	int ID = get_int(cin);
	
	Record tmp_Record(ID);
	Ordered_list<Record *>::Iterator it = library.find(&tmp_Record);
	
	if (it == library.end())
	{
		throw Error("No record with that ID!");
	}
	
	int rating = get_int(cin);
	
	(*it) -> set_rating(rating);
	
	cout << "Rating for record " << ID << " changed to " << rating << endl;
	
	return;
}

// Remove a record from the library.  An error occurs if a record with the
// specified title does not exist, or if the specified record is a member of
// one or more collections.
void delete_Record(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, const Ordered_list<Collection> &catalog)
{
	String title = get_title();
	
	Record tmp_Record(title);
	Ordered_list<Record *>::Iterator record_it = library_by_title.find(&tmp_Record);
	
	if (record_it == library_by_title.end())
	{
		throw Error("No record with that title!");
	}
	
	Record * record = *record_it;
	
	Ordered_list<Collection>::Iterator collection_it = catalog.begin();
	while (collection_it != catalog.end())
	{
		if (collection_it -> is_member_present(record))
		{
			throw Error("Cannot delete a record that is a member of a collection!");
		}
		++collection_it;
	}

	library_by_title.erase(record_it);
	
	record_it = library_by_ID.find(record);
	library_by_ID.erase(record_it);
	
	num_Records--;
	
	cout << "Record " << record -> get_ID() << " " << record -> get_title() << " deleted" << endl;

	delete record;

	return;
}

// Deletes a specified collection.  An error occurs if a collection with the
// specified name does not exist.
void delete_Collection(Ordered_list<Collection> &catalog)
{
	String name;
	cin >> name;
	
	Ordered_list<Collection>::Iterator it = catalog.begin();
	
	while (it != catalog.end())
	{
		if (it -> get_name() == name)
		{
			it -> clear();
			catalog.erase(it);

			num_Collections--;
			cout << "Collection " << name << " deleted" << endl;
			
			return;
		}
		
		++it;
	}

	throw Error("No collection with that name!");
	
	return;
}

// Remove a specified record from a specified collection.  Errors occur if
// an integer cannot be read, there is not a record with the specified ID,
// or a collection does not exist with the specified name.
void remove_Record_from_Collection(const Ordered_list<Record *> &library, Ordered_list<Collection> &catalog)
{
	String name;
	cin >> name;
	
	Ordered_list<Collection>::Iterator it = catalog.begin();
	
	while (it != catalog.end())
	{
		if (it -> get_name() == name)
		{
			int ID = get_int(cin);
			
			Record tmp_Record(ID);
			Ordered_list<Record *>::Iterator record_it = library.find(&tmp_Record);
			if (record_it == library.end())
			{
				throw Error("No record with that ID!");
			}
			
			it -> remove_member(*record_it);
			
			cout << "Member " << ID << " " << (*record_it) -> get_title() << " deleted" << endl;
			
			return;
		}
		
		++it;
	}
	
	throw Error("No collection with that name!");
	
	return;
}

// This clears the contents of the current library.  An error occurs if
// there is a collection with one or more members.
void clear_Library(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, const Ordered_list<Collection> &catalog)
{
	Ordered_list<Collection>::Iterator it = catalog.begin();
	while (it != catalog.end())
	{
		if (!it -> empty())
		{
			throw Error("Cannot clear all records unless all collections are empty!");
		}
		++it;
	}
	
	empty_record_list(library_by_title);
	// We've already called the destructors for the pointers to records
	//   so now we just need to clear the other list
	library_by_ID.clear();	
	
	num_Records = 0;
	
	cout << "All records deleted" << endl;
	
	return;
}

// This clears the contents of the catalog.
void clear_Catalog(Ordered_list<Collection> &catalog)
{
	num_Collections = 0;
	
	catalog.clear();
	
	cout << "All collections deleted" << endl;

	return;
}

// This clears both the contents of the catalog and the contents of the
// library.
void clear_All(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog)
{
	clear_data(library_by_title, library_by_ID, catalog);

	cout << "All data deleted" << endl;

	return;
}

// Save the current state of the program to a specified output file.  An
// error occurs if the specified file cannot be opened for writing.
void save_All(const Ordered_list<Record *> &library, const Ordered_list<Collection> &catalog)
{
	String filename;
	cin >> filename;
	
	ofstream os(filename.c_str());
	if (!os)
	{
		throw Error("Could not open file!");
	}
	
	os << library.size() << endl;
	
	Ordered_list<Record *>::Iterator record_it = library.begin();
	while (record_it != library.end())
	{
		(*record_it) -> save(os);
		++record_it;
	}
	
	os << catalog.size() << endl;
	
	Ordered_list<Collection>::Iterator collection_it = catalog.begin();
	while (collection_it != catalog.end())
	{
		collection_it -> save(os);
		++collection_it;
	}
	
	os.close();

	cout << "Data saved" << endl;

	return;
}

// Restore the state of the program from a previous save.  An error occurs
// if the file could not be opened, or the file contains invalid data (which
// would be the case if the specified file has not been generated by the
// program in a previous state.
void restore_All(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog)
{
	String filename;
	cin >> filename;
	
	ifstream is(filename.c_str());
	if (!is)
	{
		throw Error("Could not open file!");
	}

	try
	{
		clear_data(library_by_title, library_by_ID, catalog);
		Record::reset_ID_counter();
	
		num_Records = get_int(is);

		for (int i = 0; i < num_Records; i++)
		{
			Record *record = new Record(is);
			library_by_title.insert(record);
			library_by_ID.insert(record);
		}

		num_Collections = get_int(is);

		
		for (int i = 0; i < num_Collections; i++)
		{
			Collection collection(is, library_by_title);
			catalog.insert(collection);
		}
	}
	catch (Error &error)
	{
		clear_data(library_by_title, library_by_ID, catalog);
		Record::reset_ID_counter();
		
		throw Error("Invalid data found in file!");
	}

	is.close();
	
	cout << "Data loaded" << endl;
	
	return;
}

// Clean up the program by deleting all dynamically allocated memory, and
// then exit the program.
void quit(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog)
{
	clear_All(library_by_title, library_by_ID, catalog);

	cout << "Done" << endl;
	
	return;
}

// Get a title from standard input.  All initial and trailing whitespace is
// removed and any duplicate whitespace is also removed.
String get_title()
{
	String tmp;
	getline(cin, tmp);

	String final;
	const char *c_str = tmp.c_str();	

	int begin = 0;
	while (isspace(c_str[begin]))
	{
		begin++;
	}
	
	int end = strlen(c_str) - 1;
	while (isspace(c_str[end]))
	{
		end--;
	}
	
	for (int i = begin; i <= end; i++)
	{
		if (isspace(c_str[i]))
		{
			if (!isspace(c_str[i + 1]))
			{
				final += c_str[i];
			}
		}
		else
		{
			final += c_str[i];
		}
	}
	
	return final;
}

// A comparison function that compares records based on their ID
bool compare_Record_ID(const Record_ptr_t& r1, const Record_ptr_t& r2)
{
	return r1 -> get_ID() < r2 -> get_ID();
}

// A comparison function that compares records based on their title
bool compare_Record_title(const Record_ptr_t& r1, const Record_ptr_t& r2)
{
	return *r1 < *r2;
}

// Empty an Ordered_list<Record *> deallocating the memory for the records.
void empty_record_list(Ordered_list<Record *> &list)
{
	Ordered_list<Record *>::Iterator it = list.begin();
	while (it != list.end())
	{
		Record * record = *it;
		delete record;
		++it;
	}
	list.clear();
	
	return;
}

// Clear all of the data from the catalog and library and reset the program
// to the state when it first starts executing.
void clear_data(Ordered_list<Record *> &library_by_title, Ordered_list<Record *> &library_by_ID, Ordered_list<Collection> &catalog)
{
	num_Records = 0;
	num_Collections = 0;

	catalog.clear();
	
	Ordered_list<Record *>::Iterator it = library_by_title.begin();
	while (it != library_by_title.end())
	{
		Record * record = *it;
		delete record;
		++it;
	}
	library_by_title.clear();
	
	// We've already called the destructors for the pointers to records
	//   so now we just need to clear the other list
	library_by_ID.clear();
	
	return;
}

