/*
Demonstration of using shared_ptr with a class hierarchy,
showing conversion to base type possibilities
and static and dynamic casts.
*/

#include <iostream>
#include <tr1/memory>
using namespace std;

using std::tr1::shared_ptr;
using std::tr1::weak_ptr;
using std::tr1::static_pointer_cast;
using std::tr1::dynamic_pointer_cast;

class Base {
public:
	Base() : i(++count) {}
	~Base () {cout << "Base " << i << " destruction" << endl;}
	int get() const {return i;}
	virtual void print() const
	{cout << "Base " << i << endl;}
private:
	int i;
	static int count;
};

int Base::count = 0;

class Derived1 : public Base {
public:
	Derived1(int j_) : j(j_) {}
	~Derived1() {cout << "Derived1 " << j << " destruction" << endl;}
	virtual void print() const {cout << "Derived1 " << get() << ':' << j << endl;}
private:
	int j;
};

class Derived2 : public Base {
public:
	Derived2(int j_) : j(j_) {}
	~Derived2() {cout << "Derived2 " << j << " destruction" << endl;}
	virtual void print() const {cout << "Derived2 " << get() << ':' << j << endl;}
private:
	int j;
};

int main()
{
	shared_ptr<Derived1> dp1(new Derived1(10));
	dp1->print();
	shared_ptr<Base> bp1 = dp1;	// assignment to Base pointer type
	bp1->print();				// virtual function call
	
	shared_ptr<Derived1> dp2 = static_pointer_cast<Derived1>(bp1);	// downcast - legal, but might be invalid
	dp2->print();

	shared_ptr<Base> bp2(dp1); // construction of Base from Derived shared_ptr type
	bp2->print();
	
	shared_ptr<Base> bp3(new Derived1(20)); // construction of Base from Derived * type
	bp3->print();

	shared_ptr<Derived1> dp3 = dynamic_pointer_cast<Derived1>(bp3); // downcast to valid type
	if(dp3)
		dp3->print();
	else
		cout << "dynamic cast failed" << endl;
	
	shared_ptr<Base> bp4(new Derived2(30));
	bp4->print();
	shared_ptr<Derived1> dp4 = dynamic_pointer_cast<Derived1>(bp4);  // downcast to invalid type
	if(dp4)
		dp4->print();
	else
		cout << "dynamic cast failed" << endl;
}

/* output
Derived1 1:10
Derived1 1:10
Derived1 1:10
Derived1 1:10
Derived1 2:20
Derived1 2:20
Derived2 3:30
dynamic cast failed
Derived2 30 destruction
Base 3 destruction
Derived1 20 destruction
Base 2 destruction
Derived1 10 destruction
Base 1 destruction
*/


