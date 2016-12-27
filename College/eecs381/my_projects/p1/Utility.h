#ifndef UTILITY_H
#define UTILITY_H

#include <stddef.h>

/* Checks the return value of malloc to ensure that the memory was properly
    allocated, and prints and error message and exists if there was a problem */
void * safe_malloc(size_t size);

/* Duplicates a string and returns a pointer to the newly allocated string */
char * str_copy(char *in);

#endif

