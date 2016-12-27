#include <stdio.h>
#include <string.h>

#include "Ordered_container.h"

/* function prototypes */
void print_as_string(char * data_ptr);
int compare_string(char * data_ptr1, char * data_ptr2);

void find_and_remove(struct Ordered_container * container, char * probe);
void demo_func(void * data_ptr, void * arg);
void check_size(struct Ordered_container * container);
void add(struct Ordered_container * c_ptr, char * str);
void remove1(struct Ordered_container * c_ptr, char * str);

void add(struct Ordered_container * c_ptr, char * str)
{
	check_size(c_ptr);
	printf("\ninserting %s\n", str);
	OC_insert(c_ptr, str); 
	printf("size is %d\n", OC_get_size(c_ptr));
	OC_apply(c_ptr, (void (*)(void *))print_as_string);
	check_size(c_ptr);

	return;
}

void remove1(struct Ordered_container * c_ptr, char * str)
{
	check_size(c_ptr);
	/* use find_item and delete_item */
	find_and_remove(c_ptr, str);
	printf("size is %d\n", OC_get_size(c_ptr));
	OC_apply(c_ptr, (void (*)(void *))print_as_string);

	return;
}

int main(void)
{	
	char * s0 = "s0";
	char * s1 = "s1";
	char * s2 = "s2";
	char * s3 = "s3";
	char * s4 = "s4";
	char * s5 = "s5";
	char * s6 = "s6";
	char * s7 = "s7";
	char * s8 = "s8";
		
	struct Ordered_container * container;
	
	container = OC_create_container((OC_comp_fp_t)compare_string);
	
	check_size(container);
	printf("size is %d\n", OC_get_size(container));
	OC_apply(container, (void (*)(void *))print_as_string);
	
	add(container, s1);
	add(container, s3);
	add(container, s5);

	add(container, s0);
	remove1(container, s0);
	
	add(container, s2);
	remove1(container, s2);
	
	add(container, s4);
	remove1(container, s4);
	
	add(container, s6);
	remove1(container, s6);
	
	add(container, s7);
	
	add(container, s0);
	remove1(container, s0);
	
	add(container, s2);
	remove1(container, s2);
	
	add(container, s4);
	remove1(container, s4);
	
	add(container, s6);
	remove1(container, s6);
	
	add(container, s8);
	remove1(container, s8);	


	OC_destroy_container(container);
	
	printf("Done\n");
	return 0;
}

void print_as_string(char * data_ptr)
{
	printf("%s\n", data_ptr);
}

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

void check_size(struct Ordered_container * container)
{
	if ((OC_empty(container) && OC_get_size(container)) ||
			(!OC_empty(container) && !OC_get_size(container)))
	{
		printf("Empty and size don't agree!\n");
		return;
	}
}

