/* demo of storing ints into an Ordered_array -
assuming that sizeof(int) <= sizeof(void *), 
the int can be stored directly into a cell of the Ordered_array.
*/

#include "Ordered_array.h"

#include <stdio.h>
#include <assert.h>

int comp_int(void * a, void * b);
void printit(void * ptr);
void printem(struct Ordered_array * ptr, void(*f)(void *));

int main(void)
{

	struct Ordered_array * ptr;
	int(*fp)(void *, void *);
	assert(sizeof(int) <= sizeof(void *));	/* check on assumption */
	
	fp = comp_int;
	
	ptr = create_Ordered_array(fp);
	printem(ptr, printit);	
	
	
	while (1) {
		int index;
		int v;
		
		printem(ptr, printit);
		printf("\nEnter a search value, or -1:");
		scanf("%d", &v);
	
		if(v == -1)
			break;

		index = find_in_Ordered_array(ptr, (void *) v);
		if(index < 0) {
			printf("Not found! - adding it!\n");
			insert_in_Ordered_array(ptr, (void *) v);
			}
		else {
			printf("Found! - removing it\n");
			remove_from_Ordered_array(ptr, index);
			}
		}
		
	destroy_Ordered_array(ptr);
	printf("Done!\n");
	return 0;
}
/* cast the pointers to ints and compare */
int comp_int(void * a, void * b)
{
	int i = (int) a;
	int j = (int) b;
	return (i - j);
}

/* print an item */
void printit(void * ptr)
{
	printf("%d", (int) ptr);
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

