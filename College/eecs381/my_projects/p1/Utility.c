#include "Utility.h"

#include <assert.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

/* Checks the return value of malloc to ensure that the memory was properly
    allocated, and prints and error message and exists if there was a problem */
void * safe_malloc(size_t size)
{
	void *ptr = malloc(size);
	if (!ptr)
	{
		printf("Failed memory allocation\n");
		exit(1);
	}

	return ptr;
}

/* Duplicates a string and returns a pointer to the newly allocated string */
char * str_copy(char * in)
{
	char * ptr;

	assert(in);

	ptr = safe_malloc(strlen(in) + 1);
	strcpy(ptr, in);

	return ptr;
}
