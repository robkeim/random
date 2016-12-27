/* 
An Ordered_array is a template container class that contains a dynamically 
allocated array of items of type T. Because the container keeps a copy of the
supplied items, and will have to move them around in the internal array, 
the items of type T must have a public default constructor, copy constructor, 
assignment operator, and destructor that all function correctly for type T.

The items are kept in the order specified by a comparison function supplied 
when the Ordered_array is created. The comparison function takes two T items, 
and returns an int which specifies the order in which the two items 
should be placed in the array:

	< 0 if the first item should come before the second,
	== 0 if the first is equivalent in ordering to the second,
	> 0 if the first should come after the second.
	
Items can be added to the Ordered_array through the insert function; 
this guarantees that the items are always in order. Alternatively, one can add items
to the end of the Ordered_array with the insert_at_end function. The calling code
is responsible for ensuring that the items are added in order; the results are
undefined if not.

When an item is inserted, a copy of the item is stored in the Ordered_array.
If additional memory is needed, the internal array is automatically reallocated.
The allocation is  always greater than or equal to the size (number of items in 
the array).

The effect of inserting an item into the array that compares equal to an
item already present is undefined, and this class does not check for whether 
such an equivalent item is already present. 

Iterators are used to "point to" individual items in the container. 
Overloaded operators and a begin() and end() member function allow these
Iterators to be used analogously to the Standard Library iterators. E.g.:

	Ordered_array<int> my_ints (my_int_comp_fun);	// a container of ints
	Ordered_array<int>::Iterator it;	// declares an iterator, it
	for(it = my_ints.begin(); it != my_ints.end(); it++) {
		cout << *it << endl;	// output each int
		}
If it is a container of structure or class types, then use the -> operator to
select a member of a pointed-to type, as in:
	Ordered_array<Action_item>::Iterator it;
	....
	cout << it->match_str << endl;
	
Consistent with the performance orientation of the Standard Library, no checks are
made for whether advancing or dererferencing an iterator produces a valid result.
Removing an item from the container will invalidate any Iterators pointing at that item or 
later items in the container, and inserting an item invalidates all iterators pointing 
into the container.

The find function uses a binary search to locate items, using the comparison
function to compare the items. It may be necessary to construct a dummy item 
to use in the call of find. The find function returns an Iterator that is
equal to end() if the item is not found in the container. If it is found, 
then dereferencing the Iterator produces a reference to the item in the container.
*/

/*** NOTE: The comment "fill this in" means remove the comment and replace
it with the proper code. Remove this comment and all comments starting with /***. */


template <typename T>
class Ordered_array {
public:
	// Create an Ordered_array using an ordering comparison function,
	// and initialize it to its default allocation.
	Ordered_array(int (*)(const T&, const T&));

	// When an Ordered_array is initialized from another, 
	// it has an allocation equal to the other's number of items,
	// or the initial allocation, whichever is largest.
	// It also gets the original's comparison function.
	Ordered_array(const Ordered_array&);
	
	// When an Ordered_array is assigned, the lhs gets an allocation 
	// equal to the rhs's number of items,
	// or the initial allocation, whichever is larger.
	// It also gets the rhs's comparison function.
	// The copy-swap idiom is used for assignment.
	Ordered_array& operator= (const Ordered_array&);

	// Deallocate the internal array
	// User is responsible for deallocating any pointed-to objects before 
	// this object is destroyed.
	~Ordered_array();

	// Deallocate the internal array, and reinitialize the Ordered_array.
	// User is responsible for deallocating any pointed-to objects before 
	// calling this function.
	void clear();
	
	// Return the number of array cells in use (the size of the stored set).
	int size() const
		{/*** fill this in */}
	
	// Return true if the none of the cells are in use (the size is zero)
	bool empty() const
		{/*** fill this in */}

	// Return the number of array cells currently allocated;
	// This is always greater than or equal to the size.
	int get_allocation() const
		{/*** fill this in */}
	
	// Return the total number of bytes in the current allocation.
	int get_allocation_bytes() const
		{/*** fill this in */}
	
	// A nested iterator class - used analogous to Standard Library Iterators
	// An Iterator contains a pointer to a cell of the internal array
	class Iterator {
		public:
			// a default-initialized iterator points to nothing
			Iterator() /* fill this in */
				
			// * returns a const reference to the item in the pointed-to cell
			const T& operator* () const
				{/*** fill this in */}
				
			// -> returns a const pointer to the item in the pointed-to cell
			const T* operator-> () const
				{return cell_ptr;}

			// ++ operator moves the iterator forward to point to the next cell
			Iterator operator++ ()	// prefix
				{/*** fill this in */}
			Iterator operator++ (int)	// postfix
				{/*** fill this in */}
				
			// Iterators are equal if they point to the same cell
			bool operator== (Iterator rhs) const
				{/*** fill this in */}
			bool operator!= (Iterator rhs) const
				{/*** fill this in */}

			// declare the outer class as a friend
			/*** fill this in */
			
		private:
			/***  cell_ptr below is the only member variable allowed in this class
			but you can add additional private member functions of your choice */
			T * cell_ptr;	// pointer to the cell of the array
			
		};	// end of nested Iterator class declaration


	// return an iterator pointing to the first cell
	Iterator begin() const
		{/*** fill this in */}
	// return an iterator pointing to "past the end"
	Iterator end() const
		{/*** fill this in */;}

	// Return an Iterator pointing to the cell matching the supplied probe.
	// The comparison function is used to find the matching item; 
	// if no matching item is found, the returned value == end();
	Iterator find(const T& probe) const;

	// Return true if the probe matches a cell in the container.
	bool is_present(const T& probe) const;
	
	// Store the new_item in the array in order as specified by 
	// the comparison function. The array is expanded as needed.
	void insert(const T& new_item);

	// Insert the item at the end of the array, expanding if needed.
	// No comparison with previously inserted items is performed. 
	// The results are undefined if the resulting array of items is 
	// not in the order specified by the ordering function.
	void insert_at_end(const T& item);

	// Remove the item from the array pointed to by the Iterator, and move the
	// remaining items down to fill in the vacated cell.
	// The size decreases by one, but the allocation is unchanged
	void remove(Iterator it);
	
	// Swap the contents of this Ordered_array with another one.
	// The member variable values are interchanged, along with the
	// pointers to the allocated arrays, but the two arrays 
	// are neither copied nor modified.
	void swap (Ordered_array<T>& other);

private:
	/*** you can add any private member variables and functions of your choice */

};


/*** Template implementation functions should follow after this point 
Defining them outside the class declaration makes the declaration and its
documenting comments easier to read. */

