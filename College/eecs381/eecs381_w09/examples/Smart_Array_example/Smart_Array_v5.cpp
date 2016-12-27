// This class holds an integer array of any size, and detects illegal subscripts.
// template version of Smart_Array class

#include <iostream>
using namespace std;

class Smart_Array_Exception {
public:
	Smart_Array_Exception (int v, const char * msg) :
		value (v), msg_ptr(msg) {}

	int value;
	const char * msg_ptr;
};

// version 5 - a template - suitable for actual use
template <class T>
class Smart_Array {
public:
	// constructor - argument is desired size of the array
	// constructor allocates memory for the array
	Smart_Array(int in_size) {
		size = in_size;
		ptr = new T[size];
		cout << "Smart_Array of size " << size << " constructed." << endl;  // demo only
		}

	// copy constructor - initialize this object from another one
	Smart_Array(const Smart_Array& source) {
		size = source.size;				// copy the size over
		ptr = new T[size];			// allocate new space of that size
		for (int i = 0; i < size; i++)	// copy the data over
			ptr[i] = source.ptr[i];
		cout << "Smart_Array of size " << size << " constructed from another one" << endl;  // demo only
		}
	
	// copy-swap idiom assignment operator overload - copy the data from rhs into lhs object
	// return a reference to this object to allow cascaded assignments
	Smart_Array& operator= (const Smart_Array& source) {
		// create a temp copy of source (right-hand side)
		Smart_Array temp(source);
		// swap the guts of this object with the temp;
		swap(temp);
		// return reference to this object as value of the assignment expression
		cout << "Smart_Array of size " << size << " assigned from another one" << endl;  // demo only
		return *this;
		// destructor deallocates memory of temp that used to belong to this object
		}	

	// swap the member variable values of this object with the other
	void swap(Smart_Array& other) {
		int t_size = size;
		size = other.size;
		other.size = t_size;
		int * t_ptr = ptr;
		ptr = other.ptr;
		other.ptr = t_ptr;
		}		
		
	// destructor is responsible for freeing memory when array object is deallocated
	~Smart_Array() {
		delete[] ptr;
		cout << "Smart_Array of size " << size << " destroyed." << endl;  // demo only
		}	
	
	// public access function for private data
	int get_size() const {return size;}

	// overload the subscripting operator for this class - const version
	const T& operator[] (int index) const {
		if ( index < 0 || index > size - 1) {
			// throw a bad-subscript exception
			throw Smart_Array_Exception(index, "Index out of range");
			}
		return ptr[index];
		}

	// overload the subscripting operator for this class - nonconst version
	T& operator[] (int index) {
		if ( index < 0 || index > size - 1) {
			// throw a bad-subscript exception
			throw Smart_Array_Exception(index, "Index out of range");
			}
		return ptr[index];
		}

private:
	int size;
	T* ptr;	
};


// demonstrate use of Smart_Array Class, version 5
int main()
{	

	Smart_Array<int> a(5);
	Smart_Array<char> b(5);
	cout << "Fill 5 values into int Smart_Array:" << endl;
	for (int i = 0; i < 5; i++)
		cin >> a[i];	
		
	cout << "Fill 5 values into char Smart_Array:" << endl;
	for (int i = 0; i < 5; i++)
		cin >> b[i];

	cout << "Output of int Smart_Array:" << endl;
	for (int i = 0; i < 5; i++)
		cout << a[i] << endl;	
		
	cout << "Output of char Smart_Array:" << endl;
	for (int i = 0; i < 5; i++)
		cout <<  b[i] << endl;

	cout << "\nDone!" << endl;
	
}


/* OUTPUT
Smart_Array of size 5 constructed.
Smart_Array of size 5 constructed.
Fill 5 values into int Smart_Array:
5 4 3 2 1
Fill 5 values into char Smart_Array:
z y x w v
Output of int Smart_Array:
5
4
3
2
1
Output of char Smart_Array:
z
y
x
w
v

Done!
Smart_Array of size 5 destructed.
Smart_Array of size 5 destructed.


*/

