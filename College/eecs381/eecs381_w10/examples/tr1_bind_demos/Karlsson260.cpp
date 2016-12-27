// example based on Karlsson's example on p. 260 of Beyond the C++ Standard Library
#include <iostream>
#include <list>
#include <map>
#include <string>
#include <utility>
#include <algorithm>
#include <functional>
#include <tr1/functional>

// using gcc 4.0.1
using namespace std;
using namespace std::tr1;
using namespace std::tr1::placeholders;

void print_string(const string& s)
{
	cout << s << endl;
}

void clear_string(string& s)
{
	cout << "clear " << s << endl;
	s.clear();
}

void print_string_ptr(const string* s)
{
	cout << *s << endl;
}


int main()
{
	// a list of objects
	typedef list<string> list_t;
	list_t my_list;
	my_list.push_back("Boost");
	my_list.push_back("Bind");
	cout << "print, clear, print a list of string objects" << endl;
	for_each(my_list.begin(), my_list.end(), bind(&print_string, _1) );
	// below works
//	for_each(my_list.begin(), my_list.end(), bind(&clear_string, _1) );
	for_each(my_list.begin(), my_list.end(), bind(&string::clear, _1) );
	for_each(my_list.begin(), my_list.end(), bind(&print_string, _1) );
	// simple and no surprises for a simple sequence container of objects

	// a map containing pointers to objects as the mapped_type
	typedef map<int, string *> pmap_t;
	pmap_t my_pmap;
	my_pmap[0] = new string("Boost");
	my_pmap[1] = new string("Bind");
	cout << "print, clear, print the strings pointed to by a map <int, string *>" << endl;
	// explicit return type is required
	for_each(my_pmap.begin(), my_pmap.end(), bind(&print_string_ptr, bind<string *>(&pmap_t::value_type::second, _1)));
	for_each(my_pmap.begin(), my_pmap.end(), bind(&string::clear, bind<string *>(&pmap_t::value_type::second, _1)));
	for_each(my_pmap.begin(), my_pmap.end(), bind(&print_string_ptr, bind<string *>(&pmap_t::value_type::second, _1)));
	// there is no problem if the mapped_type is a pointer
	
	// a map containing objects as the mapped_type
	typedef map<int, string> map_t;
	map_t my_map;
	my_map[0] = "Boost";
	my_map[1] = "Bind";
	cout << "print, fail to clear, the strings in map <int, string>" << endl;
	// following line from Karlsson does not compile - explicitly specifying type of return value is necessary
//	for_each(my_map.begin(), my_map.end(), bind(&print_string, bind(&map_t::value_type::second, _1)));
	// below works and is required for documented reasons
	for_each(my_map.begin(), my_map.end(), bind(&print_string, bind<string>(&map_t::value_type::second, _1)));
	// below works as expected
//	for_each(my_map.begin(), my_map.end(), bind(&print_string, bind<map_t::mapped_type>(&map_t::value_type::second, _1)));
	// following produces reference to reference error
//	for_each(my_map.begin(), my_map.end(), bind(&string::clear, bind<string>(&map_t::value_type::second, ref(_1))));
	// following produces error of initializing reference to temporary 
//	for_each(my_map.begin(), my_map.end(), bind(&print_string, bind<string>(ref(&map_t::value_type::second), _1)));
//	for_each(my_map.begin(), my_map.end(), bind(&clear_string, bind<string>(&map_t::value_type::second, _1)));

	// no apparent way to apply a modifying function to the second in a map container with mapped_type being an object
}

