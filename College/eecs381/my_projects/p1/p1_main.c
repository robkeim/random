#include <ctype.h>
#include <stdio.h>
#include <string.h>

#include "Collection.h"
#include "Ordered_container.h"
#include "p1_globals.h"
#include "Record.h"

#define MAX_FILENAME_LENGTH 31
#define FILENAME_STRING_FORMAT "%31s"

#define MAX_MEDIUM_LENGTH 7
#define MEDIUM_STRING_FORMAT "%7s"

#define MAX_NAME_LENGTH 15
#define NAME_STRING_FORMAT "%15s"

#define MAX_TITLE_LENGTH 63

int num_records = 0;
int num_collections = 0;

void find_Record_using_title(struct Ordered_container * library);
void find_Record_using_ID(struct Ordered_container * library);
void print_Collection_using_name(struct Ordered_container * catalog);
void print_Library(struct Ordered_container * library);
void print_Catalog(struct Ordered_container * catalog);
void print_allocations(void);
void add_Record(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID);
void add_Collection(struct Ordered_container * catalog);
void add_Record_to_Collection(struct Ordered_container * library, struct Ordered_container * catalog);
void modify_Record_rating(struct Ordered_container * library);
void delete_Record(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog);
void delete_Collection(struct Ordered_container * catalog);
void remove_Record_to_Collection(struct Ordered_container * library, struct Ordered_container * catalog);
void clear_Library(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog);
void clear_Catalog(struct Ordered_container * catalog);
void clear_All(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog);
void save_All(struct Ordered_container * library, struct Ordered_container * catalog);
void restore_All(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog);
void quit(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog);
void clear_all_data(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog);
void clear_stream(void);
int Collection_has_members(struct Collection * collection);
int is_Record_in_Collection(struct Record * record, struct Collection * collection);
struct Record * find_Record_by_title(struct Ordered_container * library, char * title);
int compare_Record_title(struct Record * r1, struct Record * r2);
int compare_title(char * title, struct Record * record);
struct Record * find_Record_by_ID(struct Ordered_container * library, int ID);
int compare_Record_ID(struct Record * r1, struct Record * r2);
int compare_ID(int * ID, struct Record * record);
struct Collection * find_Collection_by_name(struct Ordered_container * collection, char * name);
int compare_Collection(struct Collection * c1, struct Collection * c2);
int compare_name(char * name, struct Collection * collection);
int get_integer(int * tmp);
void get_title(char * buf);

int main()
{
	struct Ordered_container * library_by_title = OC_create_container((int (*) (void *, void *))compare_Record_title);
	struct Ordered_container * library_by_ID = OC_create_container((int (*) (void *, void *))compare_Record_ID);
	struct Ordered_container * catalog = OC_create_container((int (*) (void *, void *))compare_Collection);
	
	int valid_command = 0;
	char c1 = 0, c2 = 0;
	
	while (c1 != 'q' && c2 != 'q')
	{
		printf("\nEnter command: ");
		
		scanf(" %c %c", &c1, &c2);

		valid_command = 1;

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
						valid_command = 0;
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
						valid_command = 0;
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
						remove_Record_to_Collection(library_by_ID, catalog);
						break;
					case 'r':
						delete_Record(library_by_title, library_by_ID, catalog);
						break;
					default:
						valid_command = 0;
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
						valid_command = 0;
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
						valid_command = 0;
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
						valid_command = 0;
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
						valid_command = 0;
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
						valid_command = 0;
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
						valid_command = 0;
						break;
				}
				break;
			default:
				valid_command = 0;
				break;
		}
		
		if (!valid_command)
		{
			printf("Unrecognized command!\n");
			clear_stream();
		}
	}

	return 0;
}

/* The user specifies a title, and the record is found and printed.  An error
     message is displayed if there is no record with such title. */
void find_Record_using_title(struct Ordered_container * library)
{
	struct Record * record;
	
	char buf[MAX_TITLE_LENGTH + 1];
	get_title(buf);
	
	record = find_Record_by_title(library, buf);
	
	if (!record)
	{
		printf("No record with that title!\n");	
	}
	else
	{
		print_Record(record);
	}
	
	return;
}

/* The user specifies an ID, and the corresponding record is printed.  An error
     is displayed if no record with the specified ID exists. */
void find_Record_using_ID(struct Ordered_container * library)
{
	struct Record * record;
	
	int ID;
	if (get_integer(&ID))
	{
		return;
	}
	
	record = find_Record_by_ID(library, ID);
	
	if (!record)
	{
		printf("No record with that ID!\n");
		clear_stream();
	}
	else
	{
		print_Record(record);
	}
	
	return;
}

/* The user specifies the name of a collection, and it's contents are printed.
     An error message is displayed if no collection with such name exists. */
void print_Collection_using_name(struct Ordered_container * catalog)
{
	struct Collection * collection;
	
	char name[MAX_NAME_LENGTH + 1];
	scanf(NAME_STRING_FORMAT, name);
	
	collection = find_Collection_by_name(catalog, name);

	if (!collection)
	{
		printf("No collection with that name!\n");
		clear_stream();
	}
	else
	{
		print_Collection(collection);
	}

	return;
}

/* Prints the contents of the library */
void print_Library(struct Ordered_container * library)
{
	int num_records = OC_get_size(library);
	if (!num_records)
	{
		printf("Library is empty\n");
		return;
	}
	
	printf("Library contains %d records:\n", num_records);
	OC_apply(library, (void (*) (void *))print_Record);
	
	return;
}

/* Prints all of the collections and for each collection what records are a part
     of that collection */
void print_Catalog(struct Ordered_container * catalog)
{
	int num_collections = OC_get_size(catalog);
	
	if (!num_collections)
	{
		printf("Catalog is empty\n");
		return;
	}
	
	printf("Catalog contains %d collections:\n", num_collections);
	OC_apply(catalog, (void (*) (void *))print_Collection);
	
	return;
}

/* Print the current memory allocations from the program */
void print_allocations(void)
{
	printf("Memory allocations:\n");
	printf("Records: %d\n", num_records);
	printf("Collections: %d\n", num_collections);
	printf("Containers: %d\n", g_Container_count);
	printf("Container items in use: %d\n", g_Container_items_in_use);
	printf("Container items allocated: %d\n", g_Container_items_allocated);
	printf("C-strings: %d bytes total\n", g_string_memory);

	return;
}

/* The user specifies a medium and title, and the record is added to the library.
     An error occurs if there already exists a record with the specified title. */
void add_Record(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID)
{
	struct Record * ptr;
	
	char medium[MAX_MEDIUM_LENGTH + 1];
	char title[MAX_TITLE_LENGTH + 1];
	
	scanf(MEDIUM_STRING_FORMAT, medium);
	get_title(title);
		
	if (find_Record_by_title(library_by_title, title))
	{
		printf("Library already has a record with this title!\n");
		return;
	}
	
	ptr = create_Record(medium, title);
	
	num_records ++;
	OC_insert(library_by_title, ptr);
	OC_insert(library_by_ID, ptr);
	
	printf("Record %d added\n", get_Record_ID(ptr));
	
	return;
}

/* A collection is created from a specified name.  An error occurs if there already
     exists a collection of that name */
void add_Collection(struct Ordered_container * catalog)
{
	struct Collection * collection;
	
	char name[MAX_NAME_LENGTH + 1];
	scanf(NAME_STRING_FORMAT, name);
	
	if (find_Collection_by_name(catalog, name))
	{
		printf("Catalog already has a collection with this name!\n");
		clear_stream();
		return;
	}
	
	collection = create_Collection(name);
	OC_insert(catalog, collection);
	
	printf("Collection %s added\n", name);
	
	return;
}

/* A record is added to a collection.  Errors can occur if there is no collection
     with such a name, or the user specifies an invalid ID for the record */
void add_Record_to_Collection(struct Ordered_container * library, struct Ordered_container * catalog)
{
	struct Collection * collection;
	struct Record * record;
	
	int ID;
	char name[MAX_NAME_LENGTH + 1];
	scanf(NAME_STRING_FORMAT, name);
	
	collection = find_Collection_by_name(catalog, name);
	
	if (!collection)
	{
		printf("No collection with that name!\n");
		clear_stream();
		return;
	}
	
	if (get_integer(&ID))
	{
		return;
	}
	
	record = find_Record_by_ID(library, ID);
	
	if (!record)
	{
		printf("No record with that ID!\n");
		clear_stream();
		return;
	}
	
	if (is_Collection_member_present(collection, record))
	{
		printf("Record is already a member in the collection!\n");
		clear_stream();
		return;
	}
	
	printf("Member %d %s added\n", ID, get_Record_title(record));
	add_Collection_member(collection, record);
	
	return;
}

/* Allows the user to modify the rating of a record.  Errors occur if the user
     specifies and invalid ID or an invalid rating. */
void modify_Record_rating(struct Ordered_container * library)
{
	struct Record * record;
	
	int rating;
	int ID;
	
	if (get_integer(&ID))
	{
		return;
	}
	
	record = find_Record_by_ID(library, ID);
	
	if (!record)
	{
		printf("No record with that ID!\n");
		clear_stream();
		return;
	}
	
	if (get_integer(&rating))
	{
		return;
	}
	
	if (rating <= 0 || rating > 5)
	{
		printf("Rating is out of range!\n");
		clear_stream();
		return;
	}
	
	set_Record_rating(record, rating);
	printf("Rating for record %d changed to %d\n", ID, rating);
	
	return;
}

/* This removes a record from the library.  An error occurs if no record of the
     specified title exists, or the record is a member of a collection */
void delete_Record(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog)
{
	struct Record * record;

	char title[MAX_TITLE_LENGTH + 1];
	get_title(title);
	
	record = find_Record_by_title(library_by_title, title);
	
	if (!record)
	{
		printf("No record with that title!\n");	
		return;
	}
	
	if (OC_find_item_arg(catalog, record, (int (*) (void *, void *))is_Record_in_Collection))
	{
		printf("Cannot delete a record that is a member of a collection!\n");
		return;
	}
	
	OC_delete_item(library_by_title, OC_find_item(library_by_title, record));
	OC_delete_item(library_by_ID, OC_find_item(library_by_ID, record));
	
	num_records--;
	
	printf("Record %d %s deleted\n", get_Record_ID(record), title);
	
	destroy_Record(record);
	
	return;
}

/* This removes a collection.  An error occurs if there is no collection with the
     specified name */
void delete_Collection(struct Ordered_container * catalog)
{
	struct Collection * collection;
	
	char name[MAX_NAME_LENGTH + 1];
	scanf(NAME_STRING_FORMAT, name);
	
	collection = find_Collection_by_name(catalog, name);
	
	if (!collection)
	{
		printf("No collection with that name!\n");
		clear_stream();
		return;
	}	

	OC_delete_item(catalog, OC_find_item_arg(catalog, name, (int (*)(void *, void *))compare_name));
	destroy_Collection(collection);
	
	num_collections--;
	
	printf("Collection %s deleted\n", name);
	
	return;
}

/* This removes a record from a given collection.  Errors occur if an invalid name
     is specified for the collection, and invalid ID is specified for the record,
     or if the record is not a member of the specified collection. */
void remove_Record_to_Collection(struct Ordered_container * library, struct Ordered_container * catalog)
{
	struct Collection * collection;
	struct Record * record;
	
	int ID;
	char name[MAX_NAME_LENGTH + 1];
	scanf(NAME_STRING_FORMAT, name);
	
	collection = find_Collection_by_name(catalog, name);
	
	if (!collection)
	{
		printf("No collection with that name!\n");
		clear_stream();
		return;
	}	

	if (get_integer(&ID))
	{
		return;
	}	

	record = find_Record_by_ID(library, ID);
	
	if (!record)
	{
		printf("No record with that ID!\n");
		clear_stream();
		return;
	}
	
	if (!is_Collection_member_present(collection, record))
	{
		printf("Record is not a member in the collection!\n");
		clear_stream();
		return;
	}

	remove_Collection_member(collection, record);
	printf("Member %d %s deleted\n", ID, get_Record_title(record));

	return;
}

/* This clears all of the contents of the library */
void clear_Library(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog)
{
	if (OC_apply_if(catalog, (int (*) (void *))Collection_has_members))
	{
		printf("Cannot clear all records unless all collections are empty!\n");
		clear_stream();
		return;
	}
	
	num_records = 0;
	
	OC_apply(library_by_title, (void (*) (void *))destroy_Record);
	OC_clear(library_by_title);
	OC_clear(library_by_ID);
	
	printf("All records deleted\n");
	
	return;
}

/* This clears all of the contents of the catalog. */
void clear_Catalog(struct Ordered_container * catalog)
{
	num_collections = 0;
	
	OC_apply(catalog, (void (*) (void *))destroy_Collection);
	OC_clear(catalog);
	
	printf("All collections deleted\n");

	return;
}

/* This clears all the data in the program.  It's effect is the same as sequential
     calls to clear_Library and clear_Catalog. */
void clear_All(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog)
{
	clear_all_data(library_by_title, library_by_ID, catalog);
	
	printf("All data deleted\n");
	
	return;
}

/* Save the state of the program to a specified file, so the state can be restored
     later.  An error occurs if the specified file cannot be opened. */
void save_All(struct Ordered_container * library, struct Ordered_container * catalog)
{
	FILE * file;

	char filename[MAX_FILENAME_LENGTH + 1];
	scanf(FILENAME_STRING_FORMAT, filename);

	file = fopen(filename, "w");
	
	if (!file)
	{
		printf("Could not open file!\n");
		clear_stream();
		return;
	}
	
	fprintf(file, "%d\n", OC_get_size(library));
	OC_apply_arg(library, (void (*) (void *, void *))save_Record, file);
	
	fprintf(file, "%d\n", OC_get_size(catalog));
	OC_apply_arg(catalog, (void (*) (void *, void *))save_Collection, file);
	
	fclose(file);

	printf("Data saved\n");

	return;
}

/* Restore the state of the program from a file that was saved by the program
     previously.  Errors occur if the specified file cannot be opened, or there
     is invalid data in the file (indicating it was not saved by the program). */
void restore_All(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog)
{
	FILE * file;
	int i;
	int num_items;
	struct Record * record;
	struct Collection * collection;

	char filename[MAX_FILENAME_LENGTH + 1];
	scanf(FILENAME_STRING_FORMAT, filename);

	file = fopen(filename, "r");
	
	if (!file)
	{
		printf("Could not open file!\n");
		clear_stream();
		return;
	}
	
	clear_all_data(library_by_title, library_by_ID, catalog);
	
	if (fscanf(file, "%d", &num_items) != 1)
	{
		printf("Invalid data found in file!\n");
		fclose(file);
		clear_stream();
		return;
	}
	
	getc(file);
	
	for (i = 0; i < num_items; i++)
	{
		record = load_Record(file);
		if (!record)
		{
			printf("Invalid data found in file!\n");
			clear_all_data(library_by_title, library_by_ID, catalog);
			fclose(file);
			clear_stream();
			return;	
		}
		
		OC_insert(library_by_title, record);
		OC_insert(library_by_ID, record);
	}
	
	if (!fscanf(file, "%d \n", &num_items))
	{
		printf("Invalid data found in file!\n");
		clear_all_data(library_by_title, library_by_ID, catalog);
		fclose(file);
		clear_stream();
		return;
	}
	
	for (i = 0; i < num_items; i++)
	{
		collection = load_Collection(file, library_by_title);
		if (!collection)
		{
			printf("Invalid data found in file!\n");
			clear_all_data(library_by_title, library_by_ID, catalog);
			fclose(file);
			clear_stream();
			return;	
		}
		
		OC_insert(catalog, collection);
	}

	fclose(file);
	
	num_records = OC_get_size(library_by_title);
	num_collections = OC_get_size(catalog);
	
	printf("Data loaded\n");
	
	return;
}

/* Free all memory and exit the program */
void quit(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog)
{
	clear_All(library_by_title, library_by_ID, catalog);
	OC_destroy_container(library_by_title);
	OC_destroy_container(library_by_ID);
	OC_destroy_container(catalog);

	printf("Done\n");
	
	return;
}

/* Clear all of the data from the title sorted and ID sorted library, as well as
     the catalog. */
void clear_all_data(struct Ordered_container * library_by_title, struct Ordered_container * library_by_ID, struct Ordered_container * catalog)
{
	num_records = 0;
	num_collections = 0;

	OC_apply(catalog, (void (*) (void *))destroy_Collection);
	OC_clear(catalog);
	
	OC_apply(library_by_title, (void (*) (void *))destroy_Record);
	OC_clear(library_by_title);
	OC_clear(library_by_ID);	
	
	return;
}

/* Clear the stdin input stream until a newline is found.  This is used when an
     error is discovered for error recovery. */
void clear_stream(void)
{
	while (getchar() != '\n')
	{
	}
	
	return;
}

/* Return true if the specified Collection has members false otherwise. */
int Collection_has_members(struct Collection * collection)
{
	return !Collection_empty(collection);
}

/* Return true if a Record exists in a given collection, false otherwise. */
int is_Record_in_Collection(struct Record * record, struct Collection * collection)
{
	if (is_Collection_member_present(collection, record))
	{
		return 0;
	}
	
	return -1;
}

/* Search a library for a record with a specified title and return a pointer to 
     it.  Return NULL if no record is found. */
struct Record * find_Record_by_title(struct Ordered_container * library, char * title)
{
	void * ptr;
	ptr	= OC_find_item_arg(library, title, (int (*)(void *, void *))compare_title);
	
	if (!ptr)
	{
		return NULL;
	}
	
	return OC_get_data_ptr(ptr);
}

/* Compare two record titles */
int compare_Record_title(struct Record * r1, struct Record * r2)
{
	return strcmp(get_Record_title(r1), get_Record_title(r2));
}

/* Compare a record title with an user specified title. */
int compare_title(char * title, struct Record * record)
{
	return strcmp(get_Record_title(record), title);
}

/* Search a library for a given record of the specified ID.  Return a pointer to
     the record, and NULL if no such record exists. */
struct Record * find_Record_by_ID(struct Ordered_container * library, int ID)
{
	void * ptr = OC_find_item_arg(library, &ID, (int (*)(void *, void *))compare_ID);
	
	if (!ptr)
	{
		return NULL;
	}
	
	return OC_get_data_ptr(ptr);
}

/* Compare the value of two record IDs */
int compare_Record_ID(struct Record * r1, struct Record * r2)
{
	return get_Record_ID(r1) - get_Record_ID(r2);
}

/* Compare the value of a record ID with a specified ID */
int compare_ID(int * ID, struct Record * record)
{
	return get_Record_ID(record) - *ID;
}

/* Find a collection from a specified name.  Return a pointer to the collection,
     and NULL if no collection is found. */
struct Collection * find_Collection_by_name(struct Ordered_container * collection, char * name)
{
	void * ptr = OC_find_item_arg(collection, name, (int (*)(void *, void *))compare_name);
	
	if (!ptr)
	{
		return NULL;
	}
	
	return OC_get_data_ptr(ptr);
}

/* Compare the names of two Collections */
int compare_Collection(struct Collection * c1, struct Collection * c2)
{
	return strcmp(get_Collection_name(c1), get_Collection_name(c2));
}

/* Compare the name of a collection with a specified name */
int compare_name(char * name, struct Collection * collection)
{
	return strcmp(get_Collection_name(collection), name);
}

/* Get an integer from stdin, and if this fails print the appropriate error
     message and clear the stream until a newline is found. */
int get_integer(int * tmp)
{
	if (!scanf("%d", tmp))
	{
		printf("Could not read an integer value!\n");
		clear_stream();
		return 1;
	}
	
	return 0;
}

/* Get a title from stdin, and remove leading, trailing, and multiple whitespace
     that separates words from the title */
void get_title(char * buf)
{
	int i, j;
	int extra_space;
	
	fgets(buf, MAX_TITLE_LENGTH + 1, stdin);

	/* Remove whitespace at beginning */
	i = 0;
	while (isspace(buf[i]))
	{
		i++;
	}
	
	for (j = 0; j < strlen(buf); j++)
	{
		buf[j] = buf[j + i];
	}
	
	/* Remove extra whitespace in the title */
	extra_space = 1;
	while (extra_space)
	{
		extra_space = 0;
		i = 0;

		while (strlen(buf) && i < strlen(buf) - 1 && !extra_space)
		{
			if (isspace(buf[i]) && isspace(buf[i + 1]))
			{
				for (j = i; j < strlen(buf); j++)
				{
					buf[j] = buf[j + 1];
				}
				extra_space = 1;
			}
			i++;
		}
	}
	
	/* Remove whitespace at end */
	while (isspace(buf[strlen(buf) - 1]))
	{
		buf[strlen(buf) - 1] = 0;
	}
	
	return;
}

