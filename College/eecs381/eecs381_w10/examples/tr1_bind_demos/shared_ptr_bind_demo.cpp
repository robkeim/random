/*
	These results were obtained with gcc 4.0.1 and the accompanying Std. Lib.
*/

#include <iostream>
#include <list>
#include <map>
#include <string>
#include <utility>
#include <algorithm>
#include <functional>
#include <tr1/memory>
#include <tr1/functional>

using namespace std;
using namespace std::tr1;
using namespace std::tr1::placeholders;

class Thing {
public:
	Thing(int in_i = 0) : i(in_i) {}	// need  default ctor for map container
	void print() const					// a const member function
		{cout << "Thing " << i << endl;}
	void write(ostream& os)	const	// write to a supplied ostream
		{os << "Thing" << i << " written to stream" << endl;}
	void print1arg(int j) const			// a const member function with 1 argument
		{cout << "Thing " << i << " with arg " << j << endl;}
	void print2arg(int j, int k) const	// a const member function with 2 arguments
		{cout << "Thing " << i << " with args " << j << ' ' << k << endl;}
	void update()						// a modifying function with no arguments
		{i++; cout << "Thing updated to " << i << endl;}
	void set_value(int in_i)			// a modifying function with one arguments
		{i = in_i; cout << "Thing value set to " << i << endl;}
	int get_value() const
		{return i;}
private:
	int i;
};

ostream& operator<< (ostream& os, const Thing& t)
{
	os << "Thing: " << t.get_value();
	return os;
}

// Non-member functions that have a Thing parameter
void print_Thing(Thing t)
{
	t.print();
}

void print_Thing_ref(Thing& t)
{
	t.print();
}

void update_Thing(Thing& t)
{
	t.update();
}
void set_Thing(Thing& t, int i)
{
	t.set_value(i);
}

void print_Thing_const_ref(const Thing& t)
{
	t.print();
}

void print_int_Thing(int i, Thing t)
{
	cout << "print_int_Thing " << i << ' ' << t << endl;
}

void print_Thing_int(Thing t, int i)
{
	cout << "print_Thing_int " << t << ' ' << i << endl;
}

void print_Thing_int_int(Thing t, int i, int j)
{
	cout << "print_Thing_int_int " << t << ' ' << i << ' ' << j << endl;
}

void print_Thing_ptr(shared_ptr<Thing> t)
{
	t->print();
}

void print_Thing_ptr_const(shared_ptr<const Thing> t)
{
	t->print();
}

void print_int_Thing_ptr(int i, shared_ptr<Thing> t)
{
	cout << "print_int_Thing_ptr " << i << ' ' << *t << endl;
}

void print_Thing_ptr_int_int(shared_ptr<Thing> t, int i, int j)
{
	cout << "print_Thing_ptr_int " << *t << ' ' << i << ' ' << j << endl;
}


	
void demo_with_ptr();
void demo_with_ptr_list();
void demo_with_ptr_map();


int main()
{
	demo_with_ptr();
	demo_with_ptr_list();
	demo_with_ptr_map();
	cout << "done!" << endl;
}


void demo_with_ptr()
{
	int int1 = 42;
	int int2 = 76;
	int int3 = 88;
	Thing t1(1);
	shared_ptr<Thing> ptr(new Thing(1));
	cout << "\n\n\nUsing a single shared_ptr to Thing" << endl;
	cout << "Output from print const member functions before and after applying update and set_value modifying member functions" << endl;
	bind(&Thing::print, _1)(ptr);
	bind(&Thing::update, _1)(ptr);
	bind(&Thing::set_value, _1, int1)(ptr);
	bind(&Thing::print, _1)(ptr);
	bind(&Thing::set_value, _1, int2)(ptr);
	bind(&Thing::print, _1)(ptr);
	bind(&Thing::set_value, _1, int1)(ptr);
	bind(&Thing::print1arg, _1, _2)(ptr, int2);
	bind(&Thing::print2arg, _2, _1, _3)(int3, ptr, int2);
}

	
void demo_with_ptr_list()
{
	int int1 = 42;
	int int2 = 76;
	typedef list<shared_ptr<Thing> > Plist_t;
	Plist_t ptr_list;
	ptr_list.push_back(shared_ptr<Thing>(new Thing(1)));
	ptr_list.push_back(shared_ptr<Thing>(new Thing(2)));
	ptr_list.push_back(shared_ptr<Thing>(new Thing(3)));

	cout << "\n\n\nUsing list<shared_ptr<Thing> > container" << endl;
	cout << "Using bind on ptr_list for print" << endl;
	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::print, _1) );
	cout << "Using bind on ptr_list to call Thing::print2arg" << endl;
	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::print2arg, _1, int1, int2) );

	cout << "Using mem_fn on ptr_list" << endl;
	for_each(ptr_list.begin(), ptr_list.end(), mem_fn(&Thing::print) );		// use instead of mem_fun and mem_fun_ref
	for_each(ptr_list.begin(), ptr_list.end(), mem_fn(&Thing::update) );
	for_each(ptr_list.begin(), ptr_list.end(), mem_fn(&Thing::print) );

	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::update, _1) );
	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::print, _1) );

	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::update, _1) );
	cout << "Using bind on ptr_list for set_value" << endl;
	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::print, _1) );
	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::set_value, _1, int1) );
	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::print, _1) );
	
	cout << "Using bind on ptr_list to call Thing::write with cout argument" << endl;
	for_each(ptr_list.begin(), ptr_list.end(), bind(&Thing::write, _1, ref(cout)) );

}

void demo_with_ptr_map()
{
	int int1 = 42;
	int int2 = 76;
	typedef map<int, shared_ptr<Thing> > Pmap_t;	//typedef for clarity
	Pmap_t ptr_map;
	ptr_map[1] = shared_ptr<Thing>(new Thing(1));
	ptr_map[2] = shared_ptr<Thing>(new Thing(2));
	ptr_map[3] = shared_ptr<Thing>(new Thing(3));
	
	// There appears to be a problem similar to that of map of objects, that only const member functions can be called;
	// Here the problem manifests as the inner bind needed to return a const shared_ptr<Thing>. Note that it is the shared_ptr OBJECT that 
	// is being declared const, not the pointed-to object.
	
	// lines commented out produce errors with gcc 4.0.1/lib version 4.0.0 that are shown in the preceding line 
	
	cout << "\n\n\nUsing map<int, shared_ptr<Thing> > container" << endl;
	cout << "Output from print const member functions before and after applying update and set_value modifying member functions" << endl;
	// The type of the extracted second of the dereferenced iterator must be specified with the template parameter of bind.
	// For a const member function, the type of the extracted second must be a const shared_ptr
	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::print, bind<const shared_ptr<Thing> >(&Pmap_t::value_type::second, _1)) );
	// tr1/bind_iterate.h:45: error: no match for call to '(std::tr1::_Mem_fn<void (Thing::*)()const>) (std::tr1::shared_ptr<Thing>)'
//	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::print, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1)) );

	// the first argument always has to be the "this" from the map container, but additional values can be supplied with bind
	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::print1arg, bind<const shared_ptr<Thing> >(&Pmap_t::value_type::second, _1), int1) );
	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::print2arg, bind<const shared_ptr<Thing> >(&Pmap_t::value_type::second, _1), int1, int2) );

	// const is not optional for const member function
//tr1/bind_iterate.h:45: error: no match for call to '(std::tr1::_Mem_fn<void (Thing::*)(int)const>) (std::tr1::shared_ptr<Thing>, int&)'
//	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::print1arg, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1), int1) );
//tr1/bind_iterate.h:45: error: no match for call to '(std::tr1::_Mem_fn<void (Thing::*)(int, int)const>) (std::tr1::shared_ptr<Thing>, int&, int&)'
//	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::print2arg, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1), int1, int2) );

	// for a non-const member function, the bind template parameter still has to be a const shared_ptr to Thing
//tr1/bind_iterate.h:45: error: no match for call to '(std::tr1::_Mem_fn<void (Thing::*)()>) (std::tr1::shared_ptr<Thing>)'
//	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::update, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1)) );
	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::update, bind<const shared_ptr<Thing> >(&Pmap_t::value_type::second, _1)) );

//tr1/bind_iterate.h:45: error: no match for call to '(std::tr1::_Mem_fn<void (Thing::*)(int)>) (std::tr1::shared_ptr<Thing>, int&)'
//	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::set_value, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1), int1) );
	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::set_value, bind<const shared_ptr<Thing> >(&Pmap_t::value_type::second, _1), int1) );
	// show the result of setting the value
	for_each(ptr_map.begin(), ptr_map.end(), bind(&Thing::print, bind<const shared_ptr<Thing> >(&Pmap_t::value_type::second, _1)) );
	
	// no problem with ordinary functions that take a shared_ptr to Thing
	for_each(ptr_map.begin(), ptr_map.end(), bind(&print_Thing_ptr, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1)) );
	for_each(ptr_map.begin(), ptr_map.end(), bind(&print_Thing_ptr_const, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1)) );
	for_each(ptr_map.begin(), ptr_map.end(), bind(&print_Thing_ptr_int_int, bind<shared_ptr<Thing> >(&Pmap_t::value_type::second, _1), int1, int2) );

	// use a Std. typedef for less container-specific code, but the const needs to be there
	for_each(ptr_map.begin(), ptr_map.end(), 
		bind(&Thing::print, 
			bind<const Pmap_t::mapped_type>(&Pmap_t::value_type::second, _1)) );
			
	cout << "Output from using bind of Thing::write to bind of second with cout argument" << endl;
	for_each(ptr_map.begin(), ptr_map.end(), 
		bind(&Thing::write, 
			bind<const Pmap_t::mapped_type>(&Pmap_t::value_type::second, _1), ref(cout)) );
}

	

