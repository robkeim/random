#ifndef ISLAND_H
#define ISLAND_H

#include <string>

#include "Sim_object.h"

// Islands are a kind of Sim_object; they have an amount of fuel and a an amount by which it increases
// every update (default is zero). The can also provide or accept fuel, and update their amount
// accordingly.

class Island : public Sim_object
{
public:
	Island (const std::string& name_, Point position_, double fuel_ = 0., double production_rate_ = 0.);

	// Return whichever is less, the request or the amount left,
	// update the amount on hand accordingly, and output the amount supplied.
	double provide_fuel(double request);
	// Add the amount to the amount on hand, and output the total as the amount the Island now has.
	void accept_fuel(double amount);

	// Return the location of the Island
	virtual Point get_location() const
	{return position;}

	// if production_rate > 0, compute production_rate * unit time, and add to amount, and print an update message
	virtual void update();
	// output information about the current state
	virtual void describe() const;

private:
	Point position;				// Location of this island
	double fuel;
	double production_rate;

	// no  copy, assignment
	Island(const Island&);
	Island& operator= (const Island&);
};

#endif

