#ifndef ORDERED_ARRAY_H
#define ORDERED_ARRAY_H

/* This header file must be used as-is, without modification, for Project 1 */

/* An Ordered_array is an opaque type that contains a dynamically 
allocated array of void * pointers which point to items created and
destroyed by the user.  These are kept in the order specified by 
a comparison function supplied when the Ordered_array is created. 
The comparison function takes two void * pointers that point to two items,
and returns an int which specifies the order in which the two items should
be placed in the array:
	< 0 if the first item should come before the second,
	== 0 if the first is equivalent in ordering to the second,
	> 0 if the first should come after the second.
	
The normal way for pointers to be added to the Ordered_array is through the
insert_in_Ordered_array function; this guarantees that the items are
always in order. No check is made for whether the items added to the 
Ordered_array are already present. To avoid duplicate entries, the
client should first check with the find function for whether the item is
already present. A binary search is used to determine where to place 
the new item.

To enable rapid filling of the array from an already-ordered source,
use the function insert_at_Ordered_array_end, which inserts the supplied item
at the end of those already present. No check is made for whether the items
are already present or in fact in the correct order, so the results are
undefined if the input is not correct.

If additional memory is needed to hold a new item, the internal array is 
automatically reallocated.  The allocation is always greater than or equal 
to the size, which is the number of void * pointers of data that have been inserted
into the array.

The find_in_Ordered_array function uses a binary search to locate items, 
using the comparison function to compare the items. Depending on the comparison
function and the nature of the items, it may be necessary to construct 
a dummy item to use in the find function call. 

*/

/* incomplete type declaration */
struct Ordered_array;

/* Create an Ordered Array using an ordering comparison function,
and initialize it to its default allocation; return the pointer to it, */
struct Ordered_array * create_Ordered_array(int (*)(void *, void *));

/* Deallocate the internal array and the Ordered_array object.
Client is responsible for deallocating memory for any pointed-to objects. */
void destroy_Ordered_array(struct Ordered_array *);

/* Deallocate the internal array, and reinitialize the Ordered_array.
Client is responsible for deallocating memory for any pointed-to objects. */
void clear_Ordered_array(struct Ordered_array *);

/* Return the number of array cells in use, which is the size of the stored set of items. */
int get_Ordered_array_size(struct Ordered_array *);

/* Return the number of array cells currently allocated;
This is always greater than or equal to the size. */
int get_Ordered_array_allocation(struct Ordered_array *);

/* Return the number of bytes in the array cells currently allocated;
This will be platform specific, depending on the size of a void * pointer. */
int get_Ordered_array_allocation_bytes(struct Ordered_array *);

/* Return the stored pointer for cell whose index is i.
As in ordinary arrays, i must be in the range {0 ... size - 1}. 
If the index is outside this range, or if the array is empty 
(size == 0), the result is undefined. */
void * get_Ordered_array_item(struct Ordered_array *, int i);

/* Return the index of the cell matching the supplied probe.
the comparison function is used to find the matching item; 
if no item is found, the result is < 0. */
int find_in_Ordered_array(struct Ordered_array *, void * probe);

/* Store the new_item in the array in order as specified by 
the comparison function. The array is expanded as needed. */
void insert_in_Ordered_array(struct Ordered_array *, void * new_item);

/* Insert the item at the end of the array.
No comparison with previously inserted items is performed. 
The results are undefined if the resulting array of items is 
not in the order specified by the ordering function. */
void insert_at_Ordered_array_end(struct Ordered_array *, void * new_item);

/* Remove the item from the array whose index is i, and move the
remaining items down to fill in the vacated cell.
The size decreases by one, but the allocation is unchanged.
Client is responsible for deallocating memory for any pointed-to objects. 
If the value of i is invalid, the results are undefined. */
void remove_from_Ordered_array(struct Ordered_array *, int i);

#endif


