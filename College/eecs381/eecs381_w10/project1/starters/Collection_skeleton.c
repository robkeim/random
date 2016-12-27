/* skeleton file for Collection.c
The struct declaration below must be used for Collection objects.
Remove this comment and complete this file with all necessary code.
*/


/* a Collection contains a pointer to a C-string name and a container
that holds pointers to Records - the members. */
struct Collection {
	char * name;
	struct Ordered_container * members;
};
