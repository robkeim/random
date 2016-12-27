#include "Collection.h"

#include <assert.h>
#include <stdlib.h>
#include <string.h>

#include "Ordered_container.h"
#include "p1_globals.h"
#include "Record.h"
#include "Utility.h"

#define MAX_COLLECTION_LENGTH 15
#define COLLECTION_STRING_FORMAT "%15s"

#define MAX_TITLE_LENGTH 63

static int compare_title(char * title, struct Record * record);
static void save_Record_title(struct Record * record_ptr, FILE * outfile);
static int Record_compare(struct Record * r1, struct Record * r2);

/* a Collection contains a pointer to a C-string name and a container
that holds pointers to Records - the members. */
struct Collection {
	char * name;
	struct Ordered_container * members;
};

/* Create a Collection object.
This is the only function that allocates memory for a Collection
and the contained data. */
struct Collection * create_Collection(char * name)
{
	struct Collection * ptr = safe_malloc(sizeof(struct Collection));
	ptr -> name = str_copy(name);
	ptr -> members = OC_create_container((int (*)(void *, void *))Record_compare);

	g_string_memory += strlen(name) + 1;

	return ptr;
}

/* Destroy a Collection object.
This is the only function that frees the memory for a Collection
and the contained data. It discards the member list,
but of course does not delete the Records themselves. */
void destroy_Collection(struct Collection * collection_ptr)
{
	assert(collection_ptr);

	g_string_memory -= (strlen(collection_ptr -> name) + 1);

	free(collection_ptr -> name);
	OC_destroy_container(collection_ptr -> members);
	free(collection_ptr);

	return;
}

/* Return the collection name. */
char * get_Collection_name(struct Collection * collection_ptr)
{
	assert(collection_ptr);

	return collection_ptr -> name;
}

/* return non-zero if there are no members, 0 if there are members */
int Collection_empty(struct Collection * collection_ptr)
{
	assert(collection_ptr);

	return OC_empty(collection_ptr -> members);
}

/* Add a member; return non-zero and do nothing if already present. */
int add_Collection_member(struct Collection * collection_ptr, struct Record * record_ptr)
{
	assert(collection_ptr);

	/* Check to make sure member isn't already a part of the collection */
	if (is_Collection_member_present(collection_ptr, record_ptr))
	{
		return 1;
	}

	OC_insert(collection_ptr -> members, (void *) record_ptr);

	return 0;
}

/* Return non-zero if the record is a member, zero if not. */
int is_Collection_member_present(struct Collection * collection_ptr, struct Record * record_ptr)
{
	assert(collection_ptr);
	
	if (OC_find_item(collection_ptr -> members, (void *) record_ptr))
	{
		return 1;
	}
	
	return 0;
}

/* Remove a member; return non-zero if not present, zero if was present. */
int remove_Collection_member(struct Collection * collection_ptr, struct Record * record_ptr)
{
	assert(collection_ptr);
	
	if (!is_Collection_member_present(collection_ptr, record_ptr))
	{
		return 1;
	}

	OC_delete_item(collection_ptr -> members, OC_find_item(collection_ptr -> members, record_ptr));

	return 0;
}

/* Print the data in a Collection. */
void print_Collection(struct Collection * collection_ptr)
{
	assert(collection_ptr);

	if (Collection_empty(collection_ptr))
	{
		printf("Collection %s contains: None\n", collection_ptr -> name);
		return;
	}
	
	printf("Collection %s contains:\n", collection_ptr -> name);
	OC_apply(collection_ptr -> members, (void (*)(void *))print_Record);

	return;
}

/* Write the data in a Collection to a file. */
void save_Collection(struct Collection * collection_ptr, FILE * outfile)
{	
	assert(collection_ptr);
	
	fprintf(outfile, "%s %i\n", collection_ptr -> name , OC_get_size(collection_ptr -> members));
	OC_apply_arg(collection_ptr -> members, (void (*)(void *, void *))save_Record_title, outfile);
	
	return;
}

/* Read a Collection's data from a file stream, create the data object and 
return a pointer to it, NULL if invalid data discovered in file.
No check made for whether the Collection already exists or not. */
struct Collection * load_Collection(FILE * input_file, struct Ordered_container * records)
{
	int i;
	int collection_size;
	struct Collection * ptr;
	struct Record * record;
	void * void_ptr;
	char name[MAX_COLLECTION_LENGTH + 1];
	/* We use MAX_TITLE_LENGTH + 2 to ensure that we can pick up a newline to
	     verify if we have a valid title */
	char title[MAX_TITLE_LENGTH + 2];
	
	if (fscanf(input_file, COLLECTION_STRING_FORMAT, name) != 1)
	{
		return NULL;
	}

	if (fscanf(input_file, "%i", &collection_size) != 1)
	{
		return NULL;
	}

	if (getc(input_file) != '\n')
	{
		return NULL;
	}

	ptr = safe_malloc(sizeof(struct Collection));
	ptr -> name = str_copy(name);
	ptr -> members = OC_create_container((OC_comp_fp_t)Record_compare);

	for (i = 0; i < collection_size; i++)
	{		
		fgets(title, MAX_TITLE_LENGTH + 2, input_file);
		
		/* Remove the newline */
		if (strlen(title) && title[strlen(title) - 1] == '\n')
		{
			title[strlen(title) - 1] = 0;
		}
		else
		{
			destroy_Collection(ptr);
			return NULL;
		}

		void_ptr = OC_find_item_arg(records, title, (int (*)(void *, void *))compare_title);
	
		if (!void_ptr)
		{
			destroy_Collection(ptr);
			return NULL;
		}
	
		record = OC_get_data_ptr(void_ptr);

		add_Collection_member(ptr, record);
	}

	/* Check collection size to verify data was read in correctly */
	if (OC_get_size(ptr -> members) != collection_size)
	{
		destroy_Collection(ptr);
		return NULL;
	}
	
	g_string_memory += strlen(name) + 1;
	
	return ptr;
}

/* This is the record comparison function and is used by the ordered array */
static int Record_compare(struct Record * r1, struct Record * r2)
{
	return strcmp(get_Record_title(r1), get_Record_title(r2));
}

/* This compares a char * with a record title */
static int compare_title(char * title, struct Record * record)
{
	return strcmp(get_Record_title(record), title);
}

/* Takes the record and saves it to the given filestream */
static void save_Record_title(struct Record * record_ptr, FILE * outfile)
{
	assert(record_ptr);

	fprintf(outfile, "%s\n", get_Record_title(record_ptr));

	return;
}

