#include <iostream>
#include <list>
#include <map>
#include <string>
#include <algorithm>
#include <functional>

#include "use_second.h"

using namespace std;

class Thing {
public:
	Thing(int in_i = 0) : i(in_i) {}	// need  default ctor for map container
	void print() const
		{cout << "Thing " << i << endl;}
	void update()
		{i++;}
	void set_value(int in_i)
		{i = in_i;}
	int get_value() const
		{return i;}
private:
	int i;
};

ostream& operator<< (ostream& os, const Thing& t)
{
	os << "Thing: " << t.get_value() << endl;
	return os;
}

/* The functions below do not take reference type parameters because the adapters
and binders end up creating "references to references" (& &) which are currently illegal in the
language, which turns out to be an extreme inconvenience that hopefully will be fixed in the
next Standard (which is being worked on right now). The fix will be that a reference to a reference
is simply a reference. Some compilers are ahead of the others in already
doing this, but it is not in fact standard. */

void print_int(int i)
{
	cout << i << endl;
}

void print_Thing(Thing t)
{
	t.print();
}

void print_int_Thing(int i, Thing t)
{
	cout << "print_int_Thing " << i << ' ' << t;
}

void print_Thing_int(Thing t, int i)
{
	cout << "print_Thing_int " << i << ' ' << t;
}

int main()
{
	Thing t1(1), t2(2), t3(3);
	
	map<int, Thing> obj_map;
	obj_map[1] = t1;	// a copy
	obj_map[2] = t2;
	obj_map[3] = t3;
	
			
	// try setting the value using an adapter to pass an argument
	
	cout << "Output from applying an ordinary function using obj_map" << endl;
	for_each(obj_map.begin(), obj_map.end(), use_second(print_Thing));
	
	cout << "Output from applying an ordinary function with ptr_fun using obj_map" << endl;
	for_each(obj_map.begin(), obj_map.end(), use_second(ptr_fun(print_Thing)));	

	cout << "Before and after applying a modifying member function using obj_map" << endl;
	for_each(obj_map.begin(), obj_map.end(), use_second(mem_fun_ref(&Thing::print)) );
	for_each(obj_map.begin(), obj_map.end(), use_second(mem_fun_ref(&Thing::update)) );
	for_each(obj_map.begin(), obj_map.end(), use_second(mem_fun_ref(&Thing::print)) );

	// using binders with ordinary functions
	int a_value = 42;
	
	cout << "Output from applying use_second and a bind2nd adapter on an ordinary function" << endl;
	for_each(obj_map.begin(), obj_map.end(), use_second(bind2nd(ptr_fun(print_Thing_int), a_value)) );
	cout << "Output from applying use_second and a bind1st adapter on an ordinary function" << endl;
	for_each(obj_map.begin(), obj_map.end(), use_second(bind1st(ptr_fun(print_int_Thing), a_value)) );
	
	// using binders with member functions
	// try setting the value using an adapter to pass an argument for the object list
	int new_value = 42;
	
	cout << "Set the value with an adapter on obj_map" << endl;
	for_each(obj_map.begin(), obj_map.end(), use_second(bind2nd(mem_fun_ref(&Thing::set_value), new_value)) );
	cout << "Output from using the print member function on obj_map" << endl;
	for_each(obj_map.begin(), obj_map.end(), use_second(mem_fun_ref(&Thing::print)) );



	cout << "done!" << endl;
}