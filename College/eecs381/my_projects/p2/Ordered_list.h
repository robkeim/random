#ifndef ORDERED_LIST_H
#define ORDERED_LIST_H

#include <cassert>

#include "p2_globals.h"
#include "Utility.h"

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

// This function template defines a default ordering function
// based on the less-than operator for the type T.
template<typename T>
bool less_than(const T& t1, const T& t2) 
{
	return t1 < t2;
}

template<typename T>
class Ordered_list 
{
	private:
		// Node is a nested class that is private to the Ordered_list<T> class; 
		// declared first to simplify later declarations.
		struct Node 
		{
			Node(const T& in_datum, Node * in_next) :
				datum(in_datum), next(in_next), prev(0)
			{
				g_Ordered_list_Node_count++;
			}
			
			// copy ctor and dtor defined only to support allocation counting
			Node(const Node& other) :
				datum(other.datum), next(other.next), prev(0)
			{
				g_Ordered_list_Node_count++;
			}
			
			~Node()
			{
				g_Ordered_list_Node_count--;
			}
			
			T datum;
			Node * next;
			Node * prev;
		};

	public:
		// The constructor takes a ordering function that returns true if the first argument should come
		// before the second; the arguments are passed in by reference-to-const to avoid data copying.
		// The default constructor parameter is the less_than function for the type.
		Ordered_list(bool (*ordering_function_)(const T&, const T&) = less_than<T>) :
			num_nodes(0), head(0), tail(0)
		{
			g_Ordered_list_count++;
			ordering_function = ordering_function_;

			return;
		}

		// Copy an Ordered_list
		Ordered_list(const Ordered_list& original) :
			num_nodes(original.num_nodes), head(0), tail(0)
		{
			g_Ordered_list_count++;
			ordering_function = original.ordering_function;
	
			if (!num_nodes)
			{
				return;
			}
	
			Node * cur = original.tail;
			Node * new_node = new Node(cur -> datum, 0);
			tail = new_node;
			cur = cur -> prev;
			
			while (cur)
			{				
				new_node -> prev = new Node(cur -> datum, new_node);
				new_node = new_node -> prev;
				cur = cur -> prev;
			}
			
			head = new_node;

			return;
		}

		// Assign an ordered list
		Ordered_list& operator= (const Ordered_list& original)
		{
			Ordered_list<T> tmp(original);
			swap(tmp);

			return *this;
		}

		// Destroy an ordered list
		~Ordered_list()
		{
			g_Ordered_list_count--;
			Node * cur = head;
			Node * del;

			while (cur)
			{
				del = cur;
				cur = cur -> next;
				delete del;
			}

			return;
		}

		// Delete the nodes in the list, if any, and initialize it. 
		void clear()
		{
			Node * cur = head;
			Node * del;

			while (cur)
			{
				del = cur;
				cur = cur -> next;
				delete del;
			}

			head = 0;
			tail = 0;
			num_nodes = 0;

			return;
		}

		// Return the number of nodes in the list
		int size() const
		{
			return num_nodes;
		}
		
		// Return true if the list is empty
		bool empty() const
		{
			return !num_nodes;
		}

		// An Iterator object designates a Node by encapsulating a pointer to the Node, 
		// and provides Standard Library-style operators for using, manipulating, and comparing Iterators.
		// This class is nested inside Ordered_list<> as a public member, refer to as e.g. Ordered_list<int>::Iterator
		class Iterator 
		{
			public:
				// default initialize to zero
				Iterator() :
					node_ptr(0)
				{ }

				// Overloaded dereferencing operators
				// * returns a reference to the datum in the pointed-to node
				T& operator* () const
				{
					assert(node_ptr);
					return node_ptr->datum;
				}
				// operator-> simply returns the address of the data in the pointed-to node.
				// For this operator, the compiler reapplies the -> operator with the returned pointer.
				T* operator-> () const
				{
					assert(node_ptr);
					return &(node_ptr->datum);
				}

				// ++ operator moves the iterator forward to point to the next node
				Iterator operator++ ()	// prefix
				{	
					node_ptr = node_ptr -> next;
					return *this;
				}
				
				Iterator operator++ (int)	// postfix
				{	
					node_ptr = node_ptr -> next;
					return *this;
				}
				
				// Iterators ar equal if they point to the same node
				bool operator== (Iterator rhs) const
				{
					return node_ptr == rhs.node_ptr;
				}
				
				bool operator!= (Iterator rhs) const
				{
					return node_ptr != rhs.node_ptr;
				}

				friend class Ordered_list;

			private:
				Iterator(Node * node_ptr_) :
					node_ptr(node_ptr_) 
				{ }
				Node * node_ptr;
		};
		// end of nested Iterator class declaration

		// return an iterator pointing to the first node
		Iterator begin() const
		{
			return Iterator(head);
		}
		
		// return an iterator pointing to "past the end"
		Iterator end() const
		{
			return Iterator(0); // same as next pointer of last node
		}	

		// The insert functions add the new datum to the list using the ordering function. 
		// If an "equal" object is already in the list, then the new datum object 
		// is placed in the list before the "equal" one that is already there.
		void insert(const T& new_datum)
		{
			Node * add = new Node(new_datum, 0);
			add -> next = 0;
			add -> prev = 0;
			Node * cur = head;
			
			while (cur && ordering_function(cur -> datum, new_datum))
			{
				cur = cur -> next;
			}
			
			Node * prev = 0;
			if (cur)
			{
				prev = cur -> prev;
			}
			else
			{
				prev = tail;
			}
			
			if (prev)
			{
				prev -> next = add;
			}
			else
			{
				// We're inserting the first element of the list
				head = add;
			}
			add -> prev = prev;
			
			if (cur)
			{
				cur -> prev = add;
			}
			else
			{
				// We're inserting the last element of the list
				tail = add;
			}
			add -> next = cur;
			
			num_nodes++;

			return;
		}

		// Delete the specified node.
		// Caller is responsible for any required deletion of any pointed-to data beforehand.
		// Do not attempt to dereference the iterator after calling this function - it
		// is invalid after this function executes.
		void erase(Iterator it)
		{
			Node * cur = head;
			Node * del = it.node_ptr;

			while (cur && !equal(cur -> datum, del -> datum))
			{
				cur = cur -> next;
			}

			if (!cur)
			{
				return;
			}
			
			if (del -> prev)
			{				
				(del -> prev) -> next = del -> next;
			}
			else
			{
				// The deleted item was the head of the list
				head = del -> next;
			}
			
			if (del -> next)
			{
				(del -> next) -> prev = del -> prev;
			}
			else
			{
				// The deleted item was the tail of the list
				tail = del -> prev;
			}
			
			num_nodes--;
			
			delete del;
			
			return;
		}

		// The find function returns an iterator designating the node containing the datum that according to
		// the ordering function, is equal to the supplied probe_datum; end() is returned if the node is not found. 
		// If more than one item is equal to the probe, the returned iterator points to the first one.
		// If a matching item is not present, the scan is terminated as soon as possible by detecting 
		// when the scan goes past where the matching item would be.
		Iterator find(const T& probe_datum) const
		{
			Node * cur = head;
			
			while (cur && ordering_function(cur -> datum, probe_datum))
			{
				cur = cur -> next;
			}

			if (cur && equal(cur -> datum, probe_datum))
			{
				return Iterator(cur);
			}

			return end();
		}

		// None of the following "apply" functions is allowed to modify the list or items in the list

		// The apply function takes a pointer to a function that takes a type T argument, and
		// iterates through the list calling this function or each datum in the list. The function
		// is not allowed to modify items in the list or th.
		void apply(void (*apply_function) (const T&)) const
		{
			Node * cur = head;

			while (cur)
			{
				apply_function(cur -> datum);
				cur = cur -> next;
			}

			return;
		}

		// The apply_if functions are like the apply functions in that they call the supplied function 
		// for each item in the list, but stop the iteration and return true if the function returns true.
		// The function is not allowed to modify items in the list.
		bool apply_if(bool (*apply_function) (const T&)) const
		{
			Node * cur = head;

			while (cur && !apply_function(cur -> datum))
			{
				cur = cur -> next;
			}

			return cur;
		}

		// The following are templated member functions - they have an additional template argument for
		// the type of the additional function parameter. 

		// The apply_arg functions take a pointer to a function that takes a type T argument and a second argument
		// of type Arg, and iterates through the list calling this function for each datum in the list.
		template <typename Arg>
			void apply_arg(void (*apply_function) (const T&, Arg), Arg apply_arg) const
			{
				Node * cur = head;

				while (cur)
				{
					apply_function(cur -> datum, apply_arg);
					cur = cur -> next;
				}

				return;
			}

		template <typename Arg>
			bool apply_if_arg(bool (*apply_function) (const T&, Arg), Arg apply_arg) const
			{
				Node * cur = head;

				while (cur && !apply_function(cur -> datum, apply_arg))
				{
					cur = cur -> next;
				}

				return cur;
			}

		// interchange the member variable values of this list with the other list
		void swap(Ordered_list & other)
		{
			swapem(head, other.head);
			swapem(num_nodes, other.num_nodes);
			swapem(ordering_function, other.ordering_function);

			return;
		}

	private:
		bool (*ordering_function) (const T&, const T&);
		int num_nodes;
		Node * head;
		Node * tail;
		
		bool equal(const T& item1, const T& item2) const
		{
			return !ordering_function(item1, item2) && !ordering_function(item2, item1);
		}
};

#endif

