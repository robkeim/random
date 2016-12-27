#include "Island.h"

#include <iostream>

using std::cout;
using std::endl;
using std::string;

// initialize then output constructor message
Island::Island (const string& name_, Point position_, double fuel_, double production_rate_) : 
	Sim_object(name_), position(position_), fuel(fuel_), production_rate(production_rate_)
{ 
	cout << "Island " << get_name() << " constructed" << endl;
}

// output destructor message
Island::~Island()
{
	cout << "Island " << get_name() << " destructed" << endl;
}

// Return whichever is less, the request or the amount left,
// update the amount on hand accordingly, and output the amount supplied.
double Island::provide_fuel(double request)
{
	double fuel_provided = (request < fuel) ? request : fuel;
	fuel -= fuel_provided;
	cout << "Island " << get_name() << " supplied " << fuel_provided << " tons of fuel" << endl;
	return fuel_provided;
}

// Add the amount to the amount on hand, and output the total as the amount the Island now has.
void Island::accept_fuel(double amount)
{
	fuel += amount;
	cout << "Island " << get_name() << " now has " << fuel << " tons" << endl;
	return;
}

// if production_rate > 0, compute production_rate * unit time, and add to amount, and print an update message
void Island::update()
{
	if (production_rate > 0)
	{
		fuel += production_rate;
		cout << "Island " << get_name() << " now has " << fuel << " tons" << endl;
	}
	return;
}

// output information about the current state
void Island::describe() const
{
	cout << "\nIsland " << get_name() << " at position " << position << endl;
	cout << "Fuel available: " << fuel << " tons" << endl;
	return;
}

