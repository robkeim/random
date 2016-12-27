// This class holds an integer array of any size, and detects illegal subscripts.


#include <iostream>
#include <cstdlib>
using namespace std;

// version 2 - safe for actual use, but limited
class Smart_Array {
public:
	// constructor - argument is desired size of the array
	// constructor allocates memory for the array
	Smart_Array(int in_size) {
		size = in_size;
		ptr = new int[size];
		cout << "Smart_Array of size " << size << " constructed." << endl;  // demo only
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
			// this is simple, but there are better actions possible
			cerr << "Attempt to access Smart_Array with illegal index = " << index << endl;
			cerr << "Terminating program" << endl;
			exit(EXIT_FAILURE);
			}
		return ptr[index];
		}

	// overload the subscripting operator for this class - nonconst version
	int& operator[] (int index) {
		if ( index < 0 || index > size - 1) {
			// this is simple, but there are better actions possible
			cerr << "Attempt to access Smart_Array with illegal index = " << index << endl;
			cerr << "Terminating program" << endl;
			exit(EXIT_FAILURE);
			}
		return ptr[index];
		}

private:
	Smart_Array(const Smart_Array& source); // forbid use of copy constructor
	Smart_Array& operator= (const Smart_Array& source); // forbid use of assignment operator
	int size;
	int* ptr;	
};

void printem(const Smart_Array& a); // copy ctor not called, read-only
double averagem(Smart_Array a); // copy ctor called
void zeroem(Smart_Array& a);	// copy ctor not called
Smart_Array squarem(const Smart_Array& a);

// demonstrate use of Smart_Array Class, version 2
int main()
{	
//	Smart_Array x;	// error - no default constructor

	Smart_Array a(5);
	for (int i = 0; i < 5; i++)
		a[i] = i;	// 0, 1, 2, 3, 4

	// demonstrate call by const reference
	cout << "\nContents of a in main:" << endl;
	printem(a);

	// demonstrate initialization from copy ctor
//	Smart_Array b(a);	// error - illegal access to private member
	cout << "Contents of b after Smart_Array b(a);" << endl;
	printem(b);
				
	// demonstrate call by value
	cout << "\nAbout to call averagem(a)" << endl;
//	double avg = averagem(a); 	// error - illegal access to private member
	cout << "Average is " << avg << endl;
	cout << "Contents of a after double avg = averagem(a);" << endl;
	printem(a);
	
	// demonstrate modification through call by reference
	cout << "\nAbout to call zeroem(a);" << endl;
	zeroem(a);
	cout << "Contents of a after zeroem(a);" << endl;
	printem(a);
	
	// demonstrate assignment
	cout << "\nAbout to execute a = b;" << endl;
//	a = b; 	// error - illegal access to private member
	cout << "Contents of a after a = b;" << endl;
	printem(a);
	
	// demonstrate return value assignment
	cout << "\nAbout to execute a = squarem(b);" << endl;
//	a = squarem(b); 	// error - illegal access to private member
	cout << "Contents of a after a = squarem(b);" << endl;
	printem(a);
	
	cout << "\nDone!" << endl;
	
}


void printem(const Smart_Array& a) // copy ctor not called
{
	for (int i = 0; i < a.get_size(); i++)
		cout << '[' << i << "]:" << a[i] << endl;;
}

double averagem(Smart_Array a) // copy ctor called
{
	double sum = 0;
	
	for (int i = 0; i < a.get_size(); i++) {
		sum += a[i];
		a[i] = 0;	// demo only
		}
	cout << "Contents of a in averagem:" << endl;
	printem(a);
	return sum/a.get_size();
}

void zeroem(Smart_Array& a)
{	
	for (int i = 0; i < a.get_size(); i++) 
		a[i] = 0;
}



// return a new smart array object
Smart_Array squarem(const Smart_Array& a)
{
	Smart_Array b(a.get_size());
	
	for (int i = 0; i < a.get_size(); i++)
		b[i] = a[i] * a[i];
		
//	return b; 	// error - illegal access to private member
}

