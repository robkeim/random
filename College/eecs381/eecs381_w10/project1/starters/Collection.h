#ifndef COLLECTION_H
#define COLLECTION_H

/* 
Collections are an opaque type containing a name stored as a pointer to a C-string
in allocated member, and a container of members, represented as pointers to
Records.
*/

#include <stdio.h> /* for the declaration of FILE */

/* incomplete declarations */
struct Collection;
struct Record;
struct Ordered_container;

/* Create a Collection object.
This is the only function that allocates memory for a Collection
and the contained data. */
struct Collection * create_Collection(char * name);

/* Destroy a Collection object.
This is the only function that frees the memory for a Collection
and the contained data. It discards the member list,
but of course does not delete the Records themselves. */
void destroy_Collection(struct Collection * collection_ptr);

/* Return tthe collection name. */
char * get_Collection_name(struct Collection * collection_ptr);

/* return non-zero if there are no members, 0 if there are members */
int Collection_empty(struct Collection * collection_ptr);

/* Add a member; return non-zero and do nothing if already present. */
int add_Collection_member(struct Collection * collection_ptr, struct Record * record_ptr);

/* Return non-zero if the record is a member, zero if not. */
int is_Collection_member_present(struct Collection * collection_ptr, struct Record * record_ptr);

/* Remove a member; return non-zero if not present, zero if was present. */
int remove_Collection_member(struct Collection * collection_ptr, struct Record * record_ptr);

/* Print the data in a Collection. */
void print_Collection(struct Collection * collection_ptr);

/* Write the data in a Collection to a file. */
void save_Collection(struct Collection * collection_ptr, FILE * outfile);

/* Read a Collection's data from a file stream, create the data object and 
return a pointer to it, NULL if invalid data discovered in file.
No check made for whether the Collection already exists or not. */
struct Collection * load_Collection(FILE * input_file, struct Ordered_container * records);

#endif
