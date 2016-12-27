// This class holds an integer array of any size, and detects illegal subscripts.


#include <iostream>
#include <cstdlib>
using namespace std;

class Smart_Array_Exception {
public:
	Smart_Array_Exception (int v, const char * msg) :
		value (v), msg_ptr(msg) {}

	int value;
	const char * msg_ptr;
};

// version 4 - suitable for actual use - uses exception
class Smart_Array {
public:
	// constructor - argument is desired size of the array
	// constructor allocates memory for the array
	Smart_Array(int in_size) {
		size = in_size;
		ptr = new int[size];
		cout << "Smart_Array of size " << size << " constructed." << endl;  // demo only
		}

	// copy constructor - initialize this object from another one
	Smart_Array(const Smart_Array& source) {
		size = source.size;				// copy the size over
		ptr = new int[size];			// allocate new space of that size
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
	const int& operator[] (int index) const {
		if ( index < 0 || index > size - 1) {
			// throw a bad-subscript exception
			throw Smart_Array_Exception(index, "Index out of range");
			}
		return ptr[index];
		}

	// overload the subscripting operator for this class - nonconst version
	int& operator[] (int index) {
		if ( index < 0 || index > size - 1) {
			// throw a bad-subscript exception
			throw Smart_Array_Exception(index, "Index out of range");
			}
		return ptr[index];
		}

private:
	int size;
	int* ptr;	
};


void get_range (const Smart_Array& a);
void print_range(const Smart_Array& a, int min, int max);

// demonstrate use of Smart_Array Class, version 3
int main()
{	

	Smart_Array a(5);
	for (int i = 0; i < 5; i++)
		a[i] = i;	// 0, 1, 2, 3, 4
	
	for (int count = 0; count < 3; count++) {
		
		// let's try getting and printing a range
		try {
			get_range(a);
			}
			
		catch (Smart_Array_Exception& x) {
				cout << x.msg_ptr << ' ' << x.value << endl;
			}
			
		cout << "Shall we try again?" << endl;

	}
	
	cout << "\nDone!" << endl;
	
}

void get_range (const Smart_Array& a)
{
	int min, max;
	cout << "Enter min, max range:";
	cin >> min >> max;
	print_range(a, min, max);
}


void print_range(const Smart_Array& a, int min, int max)
{
	for (int i = min; i <= max; i++)
		cout << '[' << i << "]:" << a[i] << endl;;
}

/* OUTPUT
Smart_Array of size 5 constructed.
Enter min, max range:2 4
[2]:2
[3]:3
[4]:4
Shall we try again?
Enter min, max range:2 8
[2]:2
[3]:3
[4]:4
Index out of range 5
Shall we try again?
Enter min, max range:-1 +1
Index out of range -1
Shall we try again?

Done!
Smart_Array of size 5 destructed.
*/

/* OUTPUT IF TRY-CATCH IS COMMENTED OUT
Smart_Array of size 5 constructed.
Enter min, max range:2 4
[2]:2
[3]:3
[4]:4
Shall we try again?
Enter min, max range:2 8
[2]:2
[3]:3
[4]:4
*/

