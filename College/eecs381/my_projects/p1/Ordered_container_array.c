#include "Ordered_container.h"

#include <assert.h>
#include <stddef.h>
#include <stdlib.h>

#include "p1_globals.h"
#include "Utility.h"

#define INITIAL_ARRAY_SIZE 3

struct Ordered_container
{
	int items_alloacted;
	int num_items;
	void **array;
	OC_comp_fp_t comp_fp;
};

/* Create an empty container using the supplied comparison function, and return the pointer to it. */
struct Ordered_container * OC_create_container(OC_comp_fp_t comp_fp)
{	
	struct Ordered_container * c_ptr = safe_malloc(sizeof(struct Ordered_container));

	g_Container_count++;
	g_Container_items_allocated += INITIAL_ARRAY_SIZE;

	c_ptr -> items_alloacted = INITIAL_ARRAY_SIZE;
	c_ptr -> num_items = 0;
	c_ptr -> array = safe_malloc(INITIAL_ARRAY_SIZE * sizeof(void *));
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
	g_Container_items_allocated -= c_ptr -> items_alloacted;
	g_Container_items_in_use -= c_ptr -> num_items;

	free(c_ptr -> array);
	free(c_ptr);
	
	return;
}

/* Delete all the items in the container and initialize it. 
Caller is responsible for deleting any pointed-to data first. */
void OC_clear(struct Ordered_container * c_ptr)
{
	assert(c_ptr);

	g_Container_items_allocated -= (c_ptr -> items_alloacted - INITIAL_ARRAY_SIZE);
	g_Container_items_in_use -= c_ptr -> num_items;

	free(c_ptr -> array);
	c_ptr -> items_alloacted = INITIAL_ARRAY_SIZE;
	c_ptr -> num_items = 0;
	c_ptr -> array = safe_malloc(INITIAL_ARRAY_SIZE * sizeof(void *));	

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
	
	return item;	
}

/* Delete the specified item.
Caller is responsible for any deletion of the data pointed to by the item. */
void OC_delete_item(struct Ordered_container * c_ptr, void * item)
{
	int first = 0;
	int last = c_ptr -> num_items - 1;
	int mid;
	
	assert(c_ptr);
	assert(item);

	while (first <= last)
	{
		mid = (first + last) / 2;
		assert(mid >= 0);
		if (c_ptr -> comp_fp(c_ptr -> array[mid], item) < 0)
		{
			first = mid + 1;
		}
		else if (c_ptr -> comp_fp(c_ptr -> array[mid], item) > 0)
		{
			last = mid - 1;
		}
		else
		{
			for ( ; mid < c_ptr -> num_items - 1; mid++)
			{
				c_ptr -> array[mid] = c_ptr -> array[mid + 1];
			}
			
			g_Container_items_in_use--;
			
			c_ptr -> num_items--;

			return;
		}
	}

	return;
}

/* Create a new item for the specified data pointer and put it in the container in order by
finding the first item whose data does not compare less than the specified data,
and insert the new item before it */
void OC_insert(struct Ordered_container * c_ptr, void * data_ptr)
{
	void **tmp;	
	int i;
	int mid;
	int min = 0;
	int max = c_ptr -> num_items - 1;
	
	assert(c_ptr);

	/* The array is full so we need to increase it's size */
	if (c_ptr -> num_items == c_ptr -> items_alloacted)
	{
		g_Container_items_allocated += (2 * (c_ptr -> items_alloacted + 1) - c_ptr -> items_alloacted);
			
		c_ptr -> items_alloacted = 2 * (c_ptr -> items_alloacted + 1);
		tmp = safe_malloc(c_ptr -> items_alloacted * sizeof(void *));
		
		for (i = 0; i < c_ptr -> num_items; i++)
		{
			tmp[i] = c_ptr -> array[i];
		}

		free(c_ptr -> array);
		c_ptr -> array = tmp;
	}

	while (min <= max)
	{
		mid = (min + max) / 2;
		if (c_ptr -> comp_fp(c_ptr -> array[mid], data_ptr) < 0)
		{
			min = mid + 1;
		}
		else if (c_ptr -> comp_fp(c_ptr -> array[mid], data_ptr) > 0)
		{
			max = mid - 1;
		}
		else
		{
			break;
		}
	}

	/* Copy all of the required elements to make space for the new element */
	for (i = c_ptr -> num_items - 1; i >= min; i--)
	{
		c_ptr -> array[i + 1] = c_ptr -> array[i];
	}

	c_ptr -> array[min] = data_ptr;
	c_ptr -> num_items++;

	g_Container_items_in_use++;

	return;
}

/* Return a pointer to the item that points to data equal to the data object pointed to by data_ptr,
using the ordering function to do the comparison with data_ptr as the first argument.
The data_ptr object is assumed to be of the same type as the data objects pointed to by container items.
NULL is returned if no matching item is found. */
void * OC_find_item(struct Ordered_container * c_ptr, void * data_ptr)
{
	int first = 0;
	int last = c_ptr -> num_items - 1;
	int mid;

	while (first <= last)
	{
		mid = (first + last) / 2;
		assert(mid >= 0);
		if (c_ptr -> comp_fp(c_ptr -> array[mid], data_ptr) < 0)
		{
			first = mid + 1;
		}
		else if (c_ptr -> comp_fp(c_ptr -> array[mid], data_ptr) > 0)
		{
			last = mid - 1;
		}
		else
		{
			return c_ptr -> array[mid];
		}
	}

	return NULL;
}

/* Return a pointer to the item that points to data that matches the supplied argument given by arg_ptr
according to the supplied function, which compares arg_ptr as the first argument with the data pointer
in each item. This function does not require that arg_ptr be of the same type as the data objects, and
so allows the container to be searched without creating a complete data object first.
NULL is returned if no matching item is found. */
void * OC_find_item_arg(struct Ordered_container * c_ptr, void * arg_ptr, OC_find_item_arg_fp_t fafp)
{
	int i = 0;
	
	assert(c_ptr);

	while (i < c_ptr -> num_items && fafp(arg_ptr, c_ptr -> array[i]) < 0)
	{
		i++;
	}

	if (i >= c_ptr -> num_items || fafp(arg_ptr, c_ptr -> array[i]))
	{
		return NULL;
	}

	return c_ptr -> array[i];
}

/* Apply the supplied function to the data pointer in each item of the container. */
void OC_apply(struct Ordered_container * c_ptr, OC_apply_fp_t afp)
{
	int i;

	assert(c_ptr);

	for (i = 0; i < c_ptr -> num_items; i++)
	{
		afp(c_ptr -> array[i]);
	}

	return;
}

/* Apply the supplied function to the data pointer in each item in the container. 
If the function returns non-zero, the iteration is terminated, and that value
returned. Otherwise, zero is returned. */
int OC_apply_if(struct Ordered_container * c_ptr, OC_apply_if_fp_t afp)
{
	int i = 0;
	int return_value = 0;

	assert(c_ptr);

	while (i < c_ptr -> num_items && !return_value)
	{
		return_value = afp(c_ptr -> array[i]);
		i++;
	}

	return return_value;
}

/* Apply the supplied function to the data pointer in each item in the container;
the function takes a second argument, which is the supplied void pointer. */
void OC_apply_arg(struct Ordered_container * c_ptr, OC_apply_arg_fp_t afp, void * arg_ptr)
{
	int i;

	assert(c_ptr);

	for (i = 0; i < c_ptr -> num_items; i++)
	{
		afp(c_ptr -> array[i], arg_ptr);
	}

	return;
}

/* Apply the supplied function to the data pointer in each item in the container;
the function takes a second argument, which is the supplied void pointer. 
If the function returns non-zero, the iteration is terminated, and that value
returned. Otherwise, zero is returned. */
int OC_apply_if_arg(struct Ordered_container * c_ptr, OC_apply_if_arg_fp_t afp, void * arg_ptr)
{
	int i = 0;
	int return_value = 0;

	assert(c_ptr);

	while (i < c_ptr -> num_items && !return_value)
	{
		return_value = afp(c_ptr -> array[i], arg_ptr);
		i++;
	}

	return return_value;
}

