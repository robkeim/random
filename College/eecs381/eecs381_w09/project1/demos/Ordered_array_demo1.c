/* Demonstrate Ordered_array by using it to store an set of pointers to C-strings
in alphabetical order, using the standard library strcmp for the ordering function.
The void * pointer in the Ordered_array cell must be converted 
to and from a char * pointer.
*/

#include "Ordered_array.h"

#include <stdio.h>
#include <string.h>
#include <stdlib.h>

void printit(void * ptr);
void printem(struct Ordered_array * ptr, void(*f)(void *));
char * create_string(char * instring);

int main(void)
{
	struct Ordered_array * ptr;
	int(*fp)(void *, void *);
	int i;
	
	/* cast the strcmp function to match the comparison function type */
	fp = (int(*)(void *, void *))strcmp;
	
	ptr = create_Ordered_array(fp);
	printem(ptr, printit);
		
	while (1) {
		int index;
		char buffer[10];
		char * str;
		
		printem(ptr, printit);
		printf("\nEnter a search string, or STOP:");
		scanf("%9s", buffer);
	
		if(strcmp(buffer, "STOP") == 0)
			break;

		index = find_in_Ordered_array(ptr, buffer);
		if(index < 0) {
			printf("Not found! - adding it!\n");
			/* create_string copies the supplied string
			into a piece of allocated memory and returns a pointer to it */
			str = create_string(buffer);
			insert_in_Ordered_array(ptr, str);
			}
		else {
			printf("Found! - removing it\n");
			/* Free the memory for the pointed-to string before removing
			the pointer from the array */	
			free(get_Ordered_array_item(ptr, index));
			remove_from_Ordered_array(ptr, index);
			}
		}
		
	/* Free all of the memory for the pointed-to strings before throwing away
	all of the pointers */	
	for(i = 0; i < get_Ordered_array_size(ptr); i++) {
		free(get_Ordered_array_item(ptr, i));
		}
	clear_Ordered_array(ptr);
	printem(ptr, printit);

	printf("Done!\n");
	return 0;
}

/* print an item */
void printit(void * ptr)
{
	/* cast the void * to a char * for printing */
	printf("%s", (char *) ptr);
}


/* print the contents of the Ordered_array */
void printem(struct Ordered_array * ptr, void(*f)(void *))
{
	int i;
	printf("size, allocation are %d, %d\n", get_Ordered_array_size(ptr), 
		get_Ordered_array_allocation(ptr));
	for(i = 0; i < get_Ordered_array_size(ptr); i++) {
		f(get_Ordered_array_item(ptr, i));
		printf("\n");
		}
}

/* Copy the supplied string into enough allocated memory, checking for allocation failure.  
NOTE: The function strdup is NOT Standard, and so cannot be used in this course. */
char * create_string(char * instring)
{
	char * p = malloc(strlen(instring) + 1);
	if(!p) {
		fprintf(stderr, "Out of memory!\n");
		exit(EXIT_FAILURE);
		}	
	strcpy(p, instring);
	return p;
}

