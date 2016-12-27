/*
This contains a demo of the Ordered_container module; the behavior is the same regardless of whether
the container or array implementation is used. 

This demo uses a container of simple structure types - they are local variables; more commonly they
would be dynamically allocated.

Use a simple program like this as a "test harness" to systematically test your Ordered_container
functions, starting with the simplest and most basic. Be sure that you test them all!
*/

#include <stdio.h>
#include "Ordered_container.h"

/* a simple structure type */
struct Point {
	int x;
	int y;
};

/* function prototypes */
/* for demo purposes, these functions all have void * parameters */
void print_as_Point(void * data_ptr);
int compare_Points(void * data_ptr1, void * data_ptr2);

void find_and_remove(struct Ordered_container * container, struct Point probe);
void demo_func(void * data_ptr, void * arg);

int main(void)
{
	/* test with some Point objects - the container items hold their addresses, not the actual Point structs! */
	/* "brace initialization" is convenient for structure types */
	struct Point s1 = {1, 1};
	struct Point s2 = {1, 2};
	struct Point s3 = {2, 1};
	struct Point s4 = {2, 2};
	struct Point probe1 = {1, 2};
	struct Point probe2 = {99, 99};
		
	struct Ordered_container * container;
	
	container = OC_create_container(compare_Points);
	
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, print_as_Point);
	
	/* fill the container with s1, s2, s3, s4 but insert in this order: s3, s1, s4, s2,
	and see if the points get put in the right order. */
	
	printf("\ninserting (%d, %d)\n", s3.x, s3.y);
	OC_insert(container, &s3); 
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container,print_as_Point);
	
	printf("\ninserting (%d, %d)\n", s1.x, s1.y);
	OC_insert(container, &s1);
	printf("size is %d\n",OC_get_size(container));
	OC_apply(container, print_as_Point);
	
	printf("\ninserting (%d, %d)\n", s4.x, s4.y);
	OC_insert(container, &s4);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, print_as_Point);

	printf("\ninserting (%d, %d)\n", s2.x, s2.y);
	OC_insert(container, &s2);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, print_as_Point);

	/* use find_item and delete_item */
	find_and_remove(container, probe1);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, print_as_Point);

	find_and_remove(container, probe2);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, print_as_Point);
	
	/* demo another apply function - note function pointer cast is not needed for this one */
	{
		int odd_or_even_value = 42;
		OC_apply_arg(container, demo_func, (void *)&odd_or_even_value);
		odd_or_even_value = 3;
		OC_apply_arg(container, demo_func, (void *)&odd_or_even_value);
	}

	OC_clear(container);
	
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, print_as_Point);
	
	OC_destroy_container(container);
	/* using the pointer "container" is undefined at this point */
	
	printf("Done\n");
	return 0;
}

void print_as_Point(void * data_ptr)
{
/* syntax requires forcing the cast to be done before the dereferencing arrow */
/*	printf("(%d, %d)\n", ((struct Point *)data_ptr)->x, ((struct Point *)data_ptr)->y); */
	/* more verbose, less ugly */
	struct Point * ptr = (struct Point *)data_ptr;
	printf("(%d, %d)\n", ptr->x, ptr->y);
	
}

int compare_Points(void * data_ptr1, void * data_ptr2)
{
	/* compare x values if different; if the same, compare y values */
	if(((struct Point *)data_ptr1)->x != ((struct Point *)data_ptr2)->x)
		return (((struct Point *)data_ptr1)->x - ((struct Point *)data_ptr2)->x);
	return (((struct Point *)data_ptr1)->y - ((struct Point *)data_ptr2)->y);
}

void find_and_remove(struct Ordered_container * container, struct Point probe)
{
	void * found_item;
	printf("\nsearch for (%d, %d):\n", probe.x, probe.y);
	found_item = OC_find_item(container, &probe);
	if(found_item) {
		struct Point * p = (struct Point *)OC_get_data_ptr(found_item);
		printf("found item points to (%d, %d)\n", p->x, p->y);
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
	if(*int_ptr % 2)  /* is the arg even or odd? */
		printf("I like this item: ");
	else
		printf("I hate this item: ");
	print_as_Point(data_ptr);
}


/* output
size is 0

inserting (2, 1)
size is 1
(2, 1)

inserting (1, 1)
size is 2
(1, 1)
(2, 1)

inserting (2, 2)
size is 3
(1, 1)
(2, 1)
(2, 2)

inserting (1, 2)
size is 4
(1, 1)
(1, 2)
(2, 1)
(2, 2)

search for (1, 2):
found item points to (1, 2)
item removed
size is 3
(1, 1)
(2, 1)
(2, 2)

search for (99, 99):
probed item not found
size is 3
(1, 1)
(2, 1)
(2, 2)
I hate this item: (1, 1)
I hate this item: (2, 1)
I hate this item: (2, 2)
I like this item: (1, 1)
I like this item: (2, 1)
I like this item: (2, 2)
size is 0
Done
*/

