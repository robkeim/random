#ifndef REFUELER_H
#define REFUELER_H

#include <tr1/memory>

#include "Ship.h"

class Point;

// The Refueler is a type of ship that can hold fuel.  The Refueler helps ships
// that have run out of fuel and are dead in the water.  When the Refueler is
// told to go refuel a ship, it heads immediately toward the ship at full speed
// and upon arriving at the ship offers it the entire amount of fuel that is in
// it's cargo currently.
// Initial values:
// fuel capacity and initial amount 100 tons, maximum speed 10., fuel
// consumption 2. tons/nm, resistance 0, and initial cargo and cargo capacity
// of 1000 tons.
class Refueler : public Ship
{
public:
	Refueler(const std::string& name_, Point position_);

	// This class overrides these Ship functions so that it can see if the
	// Refueler is currently moving to refuel a ship that is dead in the
	// water.  If so, throw an Error("Ship is refueling another ship!");
	// otherwise simply call the Ship functions
	virtual void set_destination_position_and_speed(Point destination, double speed);
	virtual void set_course_and_speed(double course, double speed);
    
    // Begin moving to the destination of the ship that is to be refueled.
    // Upon arriving, offer the ship as much fuel as currently in the cargo.
    // This may throw Error("Can only refuel ships that are dead in the
    // water!"); or Error("Cannot refuel ships without cargo!");
    virtual void fuel_ship(std::tr1::shared_ptr<Ship> ship);
	
	virtual void update();
	virtual void describe() const;
		
private:
    double cur_cargo;
    double cargo_capacity;

    std::tr1::weak_ptr<Ship> destination_ship;
    Point destination;

	enum Refueler_states_e
	{
		REFUELING,
		NOT_REFUELING
	} refueler_state;
};

#endif

