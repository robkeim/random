#include <iostream>
using namespace std;

class Base {
public:
	void zap() {
		cout << "Start zapping!" << endl;
		defrangulate();
		degauss();
		transmogrify();
		cout << "Done zapping!" << endl;
		}
protected:
	// derived classes need to call this
	virtual void degauss()
		{cout << "Base deguass" << endl;}
private:
	// derived classes do not need to call these
	virtual void defrangulate()
		{cout << "Base defrangulate" << endl;}
	virtual void transmogrify()
		{cout << "Base transmogrify" << endl;}

};

class Derived0 : public Base {
// use default behavior defined in base
private:
};

class Derived1 : public Base {
// customize part of default behavior
private:
	virtual void defrangulate()
		{cout << "Derived1 defrangulate" << endl;}
};

class Derived2 : public Base {
private:
// customize different parts of default behavior
	virtual void transmogrify()
		{cout << "Derived2 transmogrify" << endl;}
	virtual void degauss()
		{cout << "Derived2 deguass" << endl;
		Base::degauss();	// do Base's version also
		}
};


int main()
{
	Derived0 d0; Derived1 d1; Derived2 d2;
	
	(&d0)->zap();
	(&d1)->zap();
	(&d2)->zap();
}

/* Output
Start zapping!
Base defrangulate
Base deguass
Base transmogrify
Done zapping!
Start zapping!
Derived1 defrangulate
Base deguass
Base transmogrify
Done zapping!
Start zapping!
Base defrangulate
Derived2 deguass
Base deguass
Derived2 transmogrify
Done zapping!
*/