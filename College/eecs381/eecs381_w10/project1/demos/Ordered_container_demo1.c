/*
This contains a demo of the Ordered_container module; the behavior is the same regardless of whether
the container or array implementation is used. 

This demo uses a container of pointers to string literals; more commonly they
would be dynamically allocated.

Use a simple program like this as a "test harness" to systematically test your Ordered_container
functions, starting with the simplest and most basic. Be sure that you test them all!
*/

#include <stdio.h>
#include <string.h>
#include "Ordered_container.h"

/* function prototypes */
void print_as_string(char * data_ptr);
int compare_string(char * data_ptr1, char * data_ptr2);

void find_and_remove(struct Ordered_container * container, char * probe);
void demo_func(void * data_ptr, void * arg);

int main(void)
{
	/* test with some string constants - the container items hold their addresses, not the actual strings! */
	
	char * s1 = "s1";
	char * s2 = "s2";
	char * s3 = "s3";
	char * s4 = "s4";
	char * probe1 = "s3";
	char * probe2 = "s99";
		
	struct Ordered_container * container;
	
	container = OC_create_container((int (*)(void *, void *))compare_string);
	/* we can use the function pointer typedefs in Ordered_container.h
	instead of writing out the casts ourselves */
/*	container = OC_create_container((OC_comp_fp_t)compare_string); */
	
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);
	
	/* fill the container of s1, s2, s3, s4 but insert in this order: s3, s1, s4, s2,
	and see if the strings get put in the right order. */
	
	printf("\ninserting %s\n", s3);
	OC_insert(container, s3); 
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);
	
	printf("\ninserting %s\n", s1);
	OC_insert(container, s1);
	printf("size is %d\n",OC_get_size(container));
	/* we can use the function pointer typedefs in Ordered_container.h
	instead of writing out the casts ourselves */
	OC_apply(container, (OC_apply_fp_t)print_as_string);
	
	printf("\ninserting %s\n", s4);
	OC_insert(container, s4);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);

	printf("\ninserting %s\n", s2);
	OC_insert(container, s2);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);

	/* use find_item and delete_item */
	find_and_remove(container, probe1);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);

	find_and_remove(container, probe2);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);
	
	/* demo another apply function - note function pointer cast is not needed for this one */
	{
		int odd_or_even_value = 42;
		OC_apply_arg(container, demo_func, (void *)&odd_or_even_value);
		odd_or_even_value = 3;
		OC_apply_arg(container, demo_func, (void *)&odd_or_even_value);
	}

	OC_clear(container);
	
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);
	
	OC_destroy_container(container);
	/* using the pointer "container" is undefined at this point */
	
	printf("Done\n");
	return 0;
}

void print_as_string(char * data_ptr)
{
	printf("%s\n", data_ptr);
}

/* could also write this way (with change to prototype also), 
making a function pointer cast redundant */
/*
void print_as_string(void * data_ptr)
{
	printf("%s\n", (char *)data_ptr);
}
*/

int compare_string(char * data_ptr1, char * data_ptr2)
{
	return strcmp(data_ptr1, data_ptr2);
}

void find_and_remove(struct Ordered_container * container, char * probe)
{
	void * found_item;
	printf("\nsearch for %s:\n", probe);
	found_item = OC_find_item(container, probe);
	if(found_item) {
		printf("found item points to %s\n", (char *)OC_get_data_ptr(found_item));
		OC_delete_item(container, found_item);
		printf("item removed\n");
		/* found_item now points to an undefined value - it is invalid */
		}
	else {
		printf("probed item not found\n");
		}
}

void demo_func(void * data_ptr, void * arg)
{
	int * int_ptr = (int *)arg;
	if(*int_ptr % 2) /* is the arg even or odd? */
		printf("I like this item: %s\n", (char *)data_ptr);
	else
		printf("I hate this item: %s\n", (char *)data_ptr);
}


/* output
size is 0

inserting s3
size is 1
s3

inserting s1
size is 2
s1
s3

inserting s4
size is 3
s1
s3
s4

inserting s2
size is 4
s1
s2
s3
s4

search for s3:
found item points to s3
item removed
size is 3
s1
s2
s4

search for s99:
probed item not found
size is 3
s1
s2
s4
I hate this item: s1
I hate this item: s2
I hate this item: s4
I like this item: s1
I like this item: s2
I like this item: s4
size is 0
Done
*/

