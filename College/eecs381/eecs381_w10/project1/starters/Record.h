#ifndef RECORD_H
#define RECORD_H

/* 
A Record is an opaque type containing a unique ID number, a rating, and a title
and medium name as pointers to C-strings that are stored in dynamically allocated memory.
*/

#include <stdio.h> /* for the declaration of FILE */

/* incomplete declaration */
struct Record;

/* Create a Record object, giving it a unique ID number using a static
local variable. This is the only function that allocates dynamic memory 
for a Record and the contained data. The rating is set to 0. */
struct Record * create_Record(char * medium, char * title);

/* Destroy a Record object
This is the only function that frees the memory for a Record
and the contained data. */
void destroy_Record(struct Record * record_ptr);

/* Accesssors */

/* Return the ID number. */
int get_Record_ID(struct Record * record_ptr);

/* Get the title pointer. */
char * get_Record_title(struct Record * record_ptr);

/* Set the rating. */
void set_Record_rating(struct Record * record_ptr, int new_rating);

/* Print a Record data items to standard output with a final \n character. 
Output order is ID number followed by a ':' then medium, rating, title, separated by one space.
If the rating is zero, a 'u' is printed instead of the rating. */
void print_Record(struct Record * record_ptr);

/* Write a Record to a file stream with a final \n character. 
The ID number is not saved. Output order is medium, rating, title */
void save_Record(struct Record * record_ptr, FILE * outfile);

/* Read a Record's data from a file stream, create the data object and 
return a pointer to it, NULL if invalid data discovered in file.
No check made in this function for whether the Record already exists or not. 
create_Record is used, so a new ID number is automatically assigned. */
struct Record * load_Record(FILE * infile);

#endif
