#include "Record.h"

#include <assert.h>
#include <stdlib.h>
#include <string.h>

#include "p1_globals.h"
#include "Utility.h"

#define MAX_MEDIUM_LENGTH 7
#define MEDIUM_STRING_FORMAT "%7s"

#define MAX_TITLE_LENGTH 63

#define ID_INITIAL_VALUE 1

#define MIN_RATING_VALUE 0
#define MAX_RATING_VALUE 5

/* a Record contains an int ID, rating, and pointers to C-strings for the title and medium */
struct Record {
	int ID;
	char * title;
	char * medium;
	int rating;
};

/* Static local variable that gives each record a unique ID */
static int ID_counter = ID_INITIAL_VALUE;

/* Create a Record object, giving it a unique ID number using a static
local variable. This is the only function that allocates dynamic memory 
for a Record and the contained data. The rating is set to 0. */
struct Record * create_Record(char * medium, char * title)
{
	struct Record * ptr = safe_malloc(sizeof(struct Record));
	
	ptr -> ID = ID_counter++;
	ptr -> title = str_copy(title);
	ptr -> medium = str_copy(medium);
	ptr -> rating = 0;

	g_string_memory += strlen(medium) + strlen(title) + 2;

	return ptr;
}

/* Destroy a Record object
This is the only function that frees the memory for a Record
and the contained data. */
void destroy_Record(struct Record * record_ptr)
{
	assert(record_ptr);

	g_string_memory -= (strlen(record_ptr -> title) + strlen(record_ptr -> medium) + 2);

	free(record_ptr -> title);
	free(record_ptr -> medium);
	free(record_ptr);

	return;
}

/* Return the ID number. */
int get_Record_ID(struct Record * record_ptr)
{
	assert(record_ptr);

	return record_ptr -> ID;
}

/* Get the title pointer. */
char * get_Record_title(struct Record * record_ptr)
{
	assert(record_ptr);

	return record_ptr -> title;
}

/* Set the rating. */
void set_Record_rating(struct Record * record_ptr, int new_rating)
{
	assert(record_ptr);

	record_ptr -> rating = new_rating;

	return;
}

/* Print a Record data items to standard output with a final \n character. 
Output order is ID number followed by a ':' then medium, rating, title, separated by one space.
If the rating is zero, a 'u' is printed instead of the rating. */
void print_Record(struct Record * record_ptr)
{
	assert(record_ptr);
	
	if (record_ptr -> rating > MIN_RATING_VALUE)
	{
		printf("%i: %s %i %s\n", record_ptr -> ID, record_ptr -> medium, record_ptr -> rating, record_ptr -> title);
	}
	else
	{
		printf("%i: %s u %s\n", record_ptr -> ID, record_ptr -> medium, record_ptr -> title);
	}
	
	return;
}

/* Write a Record to a file stream with a final \n character. 
The ID number is not saved. Output order is medium, rating, title */
void save_Record(struct Record * record_ptr, FILE * outfile)
{
	assert(record_ptr);

	fprintf(outfile, "%s %i %s\n", record_ptr -> medium, record_ptr -> rating, record_ptr -> title);

	return;
}

/* Read a Record's data from a file stream, create the data object and 
return a pointer to it, NULL if invalid data discovered in file.
No check made in this function for whether the Record already exists or not. 
create_Record is used, so a new ID number is automatically assigned. */
struct Record * load_Record(FILE * infile)
{	
	char medium[MAX_MEDIUM_LENGTH + 1];
	/* We use MAX_TITLE_LENGTH + 2 to ensure that we can pick up a newline to
	     verify if we have a valid title */
	char title[MAX_TITLE_LENGTH + 2];

	struct Record * ptr = safe_malloc(sizeof(struct Record));
	
	if((fscanf(infile, MEDIUM_STRING_FORMAT, medium) != 1) || (fscanf(infile, "%i", &(ptr -> rating)) != 1))
	{
		free(ptr);
		return NULL;
	}
	
	getc(infile);

	fgets(title, MAX_TITLE_LENGTH + 2, infile);

	/* Remove newline */
	if (strlen(title))
	{
		if (title[strlen(title) - 1] == '\n')
		{
			title[strlen(title) - 1] = 0;
		}
		else
		{
			free(ptr);
			return NULL;
		}
	}
	ptr -> medium = str_copy(medium);
	ptr -> title = str_copy(title);
	ptr -> ID = ID_counter++;
	
	g_string_memory += strlen(medium) + strlen(title) + 2;
	
	return ptr;
}

