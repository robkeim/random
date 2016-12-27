/* Ordered_list is a linked-list class template  with iterators similar to the Standard Library std::list class.
The iterators encapsulate a pointer to the list nodes, and are a public class nested within the Ordered_list
class, and would be declared e.g. as Ordered_list<int>::Iterator; Operators ++, *, and -> are overloaded for
iterators similar to std::list<>::iterator. Copy constructor and assignment operators are defined, so that
Ordered_lists can be used like built-in types.

This is an ordered list in that the nodes are automatically kept in order. The ordering function is a constructor parameter.
It takes two objects as arguments and returns true if the first should come before the second in order, and false otherwise.
The ordering function is an optional constructor parameter; if none is supplied, the default is to use the defined less_than 
function template which uses the < operator for the type, which orders the list from smallest to largest. The only way to 
add to the list is with the insert function, which automatically puts the new item in the proper place in the list. 

The ordering function arguments are of type reference-to-const, meaning that the ordering function neither copies
nor modifies the objects in the list. This produces improved performance with complex types, but is of course
unnecessarily convoluted for simple types like pointers and integers. 

The easiest way to declare an ordering function with simple types is with a typedef for the simple type, 
followed by a function declaration using the typedef declared as reference-to-const, as in:

typedef Record * Record_ptr_t;
bool order_Records_by_title(const Record_ptr_t& rp1, const Record_ptr_t& rp2);
 
To find an object in the list that matches a supplied "probe" object, the ordering function is used to 
to determine equality. That is, the find functions assume that if both (x < y) and (y < x) are false, then x == y. 
This allows both insert and find operations to be done with only the less-than relation.
 
When an object is inserted in the list, a copy is made and assigned into the list node, so
objects stored in the list must have accessible and properly defined copy constructors
and assignment operators.

When a node is removed from the list with erase(), it is destroyed, and so the object contained in the node must
have an accessible and properly defined destructor function.  When the list is cleared with the clear()
function, or destroyed, all of the list nodes are destroyed.

This class does not attempt to protect the list items from being modified. If a list item is
modified in a way that changes where it should appear in the list, the list will become disordered and list items
may become un-findable or new items will be inserted incorrectly - the effects are undefined, although a specific
implementation might behave in a predictable manner.

It is user's responsibility to ensure that items are not changed in a way that affects the validity of 
the ordering in the list. 

If the user declares a list of pointers to objects, the user is responsible for allocating and deallocating 
the pointed-to objects. Note especially that if the Ordered_list is deallocated or cleared, or a 
single node is erased from the list, the pointed-to data is NOT deallocated. In short, the Ordered_list 
does not attempt to manage the user's objects.

If any operations are attempted that are erroneous (e.g. erasing a non-existent node), the
results are undefined. 
*/

/* *** NOTE: If after a function header is a comment "fill this in" remove the comment and replace
it with the proper code here in the class declaration.  All other functions should be defined
after the class declaration in this header file. 
Comments starting with "***" are instructions to you - remove them from your finished code.
Remove this comment too. */

// This function template defines a default ordering function
// based on the less-than operator for the type T.
/* *** Complete this definition */
template<typename T>
bool less_than(const T& t1, const T& t2)


class Ordered_list {
private:
	// Node is a nested class that is private to the Ordered_list<T> class; 
	// declared first to simplify later declarations.
	// *** you may add another Node * member, and constructor parameter, if you want to use a two-way linked list
	struct Node {
		Node(const T& in_datum, Node * in_next) :
			datum(in_datum), next(in_next)
			{g_Ordered_list_Node_count++;}
		// copy ctor and dtor defined only to support allocation counting
		Node(const Node& other) :
			datum(other.datum), next(other.next)
			{g_Ordered_list_Node_count++;}
		~Node()
			{g_Ordered_list_Node_count--;}
		T datum;
		Node * next;
		};
		
		
public:
	// The constructor takes a ordering function that returns true if the first argument should come
	// before the second; the arguments are passed in by reference-to-const to avoid data copying.
	// The default constructor parameter is the less_than function for the type.
	Ordered_list(bool (*ordering_function_)(const T&, const T&) = less_than<T>);

	/* *** Defined the constructor, and declare and define the destructor, copy constructor, and assignment operator.
	The destructor must deallocate all nodes. Copy and assignment must produce a list that
	contains nodes that have a copy of the data in the other ordered list. Assignment must use
	the "copy-swap" idiom. 
	
	The constructors and destructor must increment/decrement g_Ordered_list_count.
	*/
	
	// Delete the nodes in the list, if any, and initialize it. 
	void clear();
	// Return the number of nodes in the list
	int size() const
		{/* fill this in */}
	// Return true if the list is empty
	bool empty() const
		{/* fill this in */}
		
	// An Iterator object designates a Node by encapsulating a pointer to the Node, 
	// and provides Standard Library-style operators for using, manipulating, and comparing Iterators.
	// This class is nested inside Ordered_list<> as a public member, refer to as e.g. Ordered_list<int>::Iterator
	class Iterator {
		public:
			// default initialize to zero
			Iterator() :
				node_ptr(0)
				{}
				
			// Overloaded dereferencing operators
			// * returns a reference to the datum in the pointed-to node
			T& operator* () const
				{/* fill this in */}
			// operator-> simply returns the address of the data in the pointed-to node.
			// For this operator, the compiler reapplies the -> operator with the returned pointer.
			/* *** definition supplied here because it is a special-case of operator overloading. */
			T* operator-> () const
				{assert(node_ptr); return &(node_ptr->datum);}

			// ++ operator moves the iterator forward to point to the next node
			Iterator operator++ ()	// prefix
				{	
					/* fill this in */
				}
			Iterator operator++ (int)	// postfix
				{	
					/* fill this in */
				}
			// Iterators ar equal if they point to the same node
			bool operator== (Iterator rhs) const
				{/* fill this in */}
			bool operator!= (Iterator rhs) const
				{/* fill this in */}
	
			// *** here, declare the outer Ordered_list class is a friend			

		private:
			/* *** define here a private constructor for Iterator that takes a Node * parameter.
			Ordered_list<T> can use this to create Iterators conveniently initialized to point to a Node.
			It is private because the client code can't and shouldn't be using it - it isn't even supposed to
			know about the Node objects.  */
			/* *** you may have other private member functions, but not member variables */
			Node * node_ptr;
		};
	// end of nested Iterator class declaration
	
	// return an iterator pointing to the first node
	Iterator begin() const
		{/* fill this in */}
	// return an iterator pointing to "past the end"
	Iterator end() const
		{return Iterator(0);}	// same as next pointer of last node

	// The insert functions add the new datum to the list using the ordering function. 
	// If an "equal" object is already in the list, then the new datum object 
	// is placed in the list before the "equal" one that is already there.
	void insert(const T& new_datum);
	
	// Delete the specified node.
	// Caller is responsible for any required deletion of any pointed-to data beforehand.
	// Do not attempt to dereference the iterator after calling this function - it
	// is invalid after this function executes.
	void erase(Iterator it);

	// The find function returns an iterator designating the node containing the datum that according to
	// the ordering function, is equal to the supplied probe_datum; end() is returned if the node is not found. 
	// If more than one item is equal to the probe, the returned iterator points to the first one.
	// If a matching item is not present, the scan is terminated as soon as possible by detecting 
	// when the scan goes past where the matching item would be.
	Iterator find(const T& probe_datum) const;
	
	// None of the following "apply" functions is allowed to modify the list or items in the list

	// The apply function takes a pointer to a function that takes a type T argument, and
	// iterates through the list calling this function or each datum in the list. The function
	// is not allowed to modify items in the list or th.
	void apply(void (*apply_function) (const T&)) const;

	// The apply_if functions are like the apply functions in that they call the supplied function 
	// for each item in the list, but stop the iteration and return true if the function returns true.
	// The function is not allowed to modify items in the list.
	bool apply_if(bool (*apply_function) (const T&)) const;
	
	// The following are templated member functions - they have an additional template argument for
	// the type of the additional function parameter. 
	/* *** if you define these outside the class declaration, declare the second template parameter
	as follows:
	template<typename T> template <typename Arg>
	void Ordered_list<T>::apply_arg(void (*apply_function) (const T&, Arg), Arg apply_arg) const
	{
	}
	*/
	// The apply_arg functions take a pointer to a function that takes a type T argument and a second argument
	// of type Arg, and iterates through the list calling this function for each datum in the list.
	template <typename Arg>
	void apply_arg(void (*apply_function) (const T&, Arg), Arg apply_arg) const;
	
	template <typename Arg>
	bool apply_if_arg(bool (*apply_function) (const T&, Arg), Arg apply_arg) const;
		
	// interchange the member variable values of this list with the other list
	void swap(Ordered_list & other);

private:
// *** this is the member variable declaration for the ordering function - name is your choice.
	bool (*ordering_function) (const T&, const T&);
	/* *** private member variables and functions are your choice. */
};

