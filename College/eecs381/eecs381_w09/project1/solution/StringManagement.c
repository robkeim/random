/* From Utilities.cpp */

/* Call malloc with the supplied size, and check the result.
If memory allocation failed, print a message and exit the program. */
void * checked_malloc(size_t n_bytes)
{
	void * p = malloc(n_bytes);
	if(!p) {
		fprintf(stderr, "Out of memory!\n");
		exit(EXIT_FAILURE);
		}
	return p;
}

/* Functions for creating and destroying strings in allocated memory; these functions are called
whenever a string needs to be copied into allocated memory, or the allocated memory needs to be freed.
These functions also update the global variables that monitor string memory allocations.
As called for by the project specifications, these functions follow a well-organized
plan for the use of global variables. They are the only functions that modify the global
variables. The code for the print-memory-allocations command on p1.c is the only other code to refer to these
variables, and does so strictly read-only. 
*/

/* Copy the supplied string into enough allocated memory, checking for allocation failure.  
Increment the number of strings and string bytes. */
char * create_string(char * instring)
{
	char * p;
	size_t len;
	assert(instring);
	len = strlen(instring) + 1;
	p = checked_malloc(len);
	strcpy(p, instring);
	g_string_bytes += (int)len;
	g_num_strings++;
	return p;
}

/* Deallocate the pointed-to string and decrement the number of strings and string bytes. 
Results undefined if the pointed-to string was not originally created by create_string. */
void destroy_string(char * str)
{
	size_t len = strlen(str) + 1;
	free(str);
	g_string_bytes -= (int)len;
	g_num_strings--;
}


