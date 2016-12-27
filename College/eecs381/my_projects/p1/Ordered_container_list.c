#include "Ordered_container.h"

#include <assert.h>
#include <stddef.h>
#include <stdlib.h>

#include "p1_globals.h"
#include "Utility.h"

struct Ordered_container
{
	struct Node * head;
	struct Node * tail;
	int num_items;
	OC_comp_fp_t comp_fp;
};

struct Node
{
	struct Node * next;
	struct Node * prev;
	void * data;
};

/* Create an empty container using the supplied comparison function, and return the pointer to it. */
struct Ordered_container * OC_create_container(OC_comp_fp_t comp_fp)
{
	struct Ordered_container * c_ptr = safe_malloc(sizeof(struct Ordered_container));

	g_Container_count++;
	
	c_ptr -> head = NULL;
	c_ptr -> tail = NULL;
	c_ptr -> num_items = 0;
	c_ptr -> comp_fp = comp_fp;

	return c_ptr;
}

/* Destroy the container and its items; caller is responsible for 
deleting all pointed-to data before calling this function. 
After this call, the container pointer value must not be used again. */
void OC_destroy_container(struct Ordered_container * c_ptr)
{
	assert(c_ptr);

	g_Container_count--;

	OC_clear(c_ptr);
	free(c_ptr);
	
	return;
}

/* Delete all the items in the container and initialize it. 
Caller is responsible for deleting any pointed-to data first. */
void OC_clear(struct Ordered_container * c_ptr)
{
	struct Node * cur;
	struct Node * del;
	
	int num_items;
	
	assert(c_ptr);
	
	num_items = OC_get_size(c_ptr);

	g_Container_items_allocated -= num_items;
	g_Container_items_in_use -= num_items;
	
	cur = c_ptr -> head;
	while (cur)
	{
		del = cur;
		cur = cur -> next;
		free(del);
	}

	c_ptr -> head = NULL;
	c_ptr -> tail = NULL;

	c_ptr -> num_items = 0;

	return;
}

/* Return the number of items currently stored in the container */
int OC_get_size(struct Ordered_container * c_ptr)
{
	assert(c_ptr);

	return c_ptr -> num_items;
}

/* Return non-zero (true) if the container is empty, zero (false) if the container is non-empty */
int OC_empty(struct Ordered_container * c_ptr)
{
	assert(c_ptr);

	return !(c_ptr -> num_items);
}

/* Get the data object pointer from an item. */
void * OC_get_data_ptr(void * item)
{
	assert(item);
	
	return ((struct Node *) item) -> data;	
}

/* Delete the specified item.
Caller is responsible for any deletion of the data pointed to by the item. */
void OC_delete_item(struct Ordered_container * c_ptr, void * item)
{
	struct Node * ptr;
	struct Node * cur;
	
	assert(c_ptr);
	assert(item);

	ptr = (struct Node *) item;
	cur = c_ptr -> head;
	
	while (cur && cur != ptr)
	{
		cur = cur -> next;
	}

	/* The item is not in the container */
	if (!cur)
	{
		return;
	}

	if (ptr -> prev)
	{
		ptr -> prev -> next = ptr -> next;
	}
	else
	{
		/* The deleted item is the first in the list */
		c_ptr -> head = ptr -> next; 
	}

	if (ptr -> next)
	{
		ptr -> next -> prev = ptr -> prev;
	}
	else
	{
		/* The deleted item is the last in the list */
		c_ptr -> tail = ptr -> prev;
	}

	free(cur);
	c_ptr -> num_items--;

	g_Container_items_in_use--;
	g_Container_items_allocated--;
	
	return;
}

/* Create a new item for the specified data pointer and put it in the container in order by
finding the first item whose data does not compare less than the specified data,
and insert the new item before it */
void OC_insert(struct Ordered_container * c_ptr, void * data_ptr)
{
	struct Node * new;
	struct Node * cur;
	struct Node * prev = NULL;

	assert(c_ptr);
	
	new = safe_malloc(sizeof(struct Node));
	cur = c_ptr -> head;

	while (cur && c_ptr -> comp_fp(cur -> data, data_ptr) < 0)
	{
		cur = cur -> next;
	}
	
	if (cur)
	{
		prev = cur -> prev;
	}
	else
	{
		prev = c_ptr -> tail;
	}

	if (prev)
	{
		prev -> next = new;
	}
	else
	{
		/* We are inserting the first element of the list */
		c_ptr -> head = new;
	}
	new -> prev = prev;

	if (cur)
	{
		cur -> prev = new;
	}
	else
	{
		/* We are inserting the last element of the list */
		c_ptr -> tail = new;
	}	
	new -> next = cur;

	new -> data = data_ptr;

	c_ptr -> num_items++;
	
	g_Container_items_in_use++;
	g_Container_items_allocated++;
	
	return;
}

/* Return a pointer to the item that points to data equal to the data object pointed to by data_ptr,
using the ordering function to do the comparison with data_ptr as the first argument.
The data_ptr object is assumed to be of the same type as the data objects pointed to by container items.
NULL is returned if no matching item is found. */
void * OC_find_item(struct Ordered_container * c_ptr, void * data_ptr)
{
	struct Node * cur;
	
	assert(c_ptr);
	
	cur = c_ptr -> head;

	while (cur && c_ptr -> comp_fp(cur -> data, data_ptr))
	{
		cur = cur -> next;
	}

	/* The element was not found in the list */
	if (!cur || c_ptr -> comp_fp(cur -> data, data_ptr))
	{
		return NULL;
	}

	return (void *) cur;
}

/* Return a pointer to the item that points to data that matches the supplied argument given by arg_ptr
according to the supplied function, which compares arg_ptr as the first argument with the data pointer
in each item. This function does not require that arg_ptr be of the same type as the data objects, and
so allows the container to be searched without creating a complete data object first.
NULL is returned if no matching item is found. */
void * OC_find_item_arg(struct Ordered_container * c_ptr, void * arg_ptr, OC_find_item_arg_fp_t fafp)
{
	struct Node * cur;
	
	assert(c_ptr);

	cur = c_ptr -> head;
	
	while (cur && fafp(arg_ptr, cur -> data))
	{
		cur = cur -> next;
	}

	/* The element was not found in the list */
	if (!cur || fafp(arg_ptr, cur -> data))
	{
		return NULL;
	}

	return (void *) cur;
}

/* Apply the supplied function to the data pointer in each item of the container. */
void OC_apply(struct Ordered_container * c_ptr, OC_apply_fp_t afp)
{
	struct Node * cur;
	
	assert(c_ptr);

	cur = c_ptr -> head;

	while (cur)
	{
		afp(cur -> data);
		cur = cur -> next;
	}

	return;
}

/* Apply the supplied function to the data pointer in each item in the container. 
If the function returns non-zero, the iteration is terminated, and that value
returned. Otherwise, zero is returned. */
int OC_apply_if(struct Ordered_container * c_ptr, OC_apply_if_fp_t afp)
{
	struct Node * cur;
	int return_value = 0;

	assert(c_ptr);

	cur = c_ptr -> head;

	while (cur && !return_value)
	{
		return_value = afp(cur -> data);
		cur = cur -> next;
	}

	return return_value;
}

/* Apply the supplied function to the data pointer in each item in the container;
the function takes a second argument, which is the supplied void pointer. */
void OC_apply_arg(struct Ordered_container * c_ptr, OC_apply_arg_fp_t afp, void * arg_ptr)
{
	struct Node * cur;

	assert(c_ptr);

	cur = c_ptr -> head;

	while (cur)
	{
		afp(cur -> data, arg_ptr);
		cur = cur -> next;
	}

	return;
}

/* Apply the supplied function to the data pointer in each item in the container;
the function takes a second argument, which is the supplied void pointer. 
If the function returns non-zero, the iteration is terminated, and that value
returned. Otherwise, zero is returned. */
int OC_apply_if_arg(struct Ordered_container * c_ptr, OC_apply_if_arg_fp_t afp, void * arg_ptr)
{
	struct Node * cur;
	int return_value = 0;

	assert(c_ptr);

	cur = c_ptr -> head;

	while (cur && !return_value)
	{
		return_value = afp(cur -> data, arg_ptr);
		cur = cur -> next;
	}

	return return_value;
}

