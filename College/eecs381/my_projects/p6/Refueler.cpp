#include "Refueler.h"

#include <cassert>
#include <tr1/memory>

using std::cout;
using std::endl;
using std::string;
using std::tr1::shared_ptr;

Refueler::Refueler(const string& name_, Point position_) :
	Ship(name_, position_, 100, 10., 2., 0), cur_cargo(1000), cargo_capacity(1000), refueler_state(NOT_REFUELING)
{ }

// This class overrides these Ship functions so that it can see if the
// Refueler is currently moving to refuel a ship that is dead in the
// water.  If so, throw an Error("Ship is refueling another ship!");
// otherwise simply call the Ship functions
void Refueler::set_destination_position_and_speed(Point destination, double speed)
{
	if (refueler_state == REFUELING)
	{
		throw Error("Ship is refueling another ship!");
	}
	
	Ship::set_destination_position_and_speed(destination, speed);
	
	return;
}

void Refueler::set_course_and_speed(double course, double speed)
{
	if (refueler_state == REFUELING)
	{
		throw Error("Ship is refueling another ship!");
	}
	
	Ship::set_course_and_speed(course, speed);
	
	return;
}

// Throw an error if the target ship is not dead in the water or the Refueler
// currently has no cargo.  Otherwise, set the destination for the location
// of the ship and travel there at maximum speed.
void Refueler::fuel_ship(shared_ptr<Ship> ship)
{
    if (ship->can_move())
    {
        throw Error("Can only refuel ships that are dead in the water!");
    }
    // Consider roundoff error
    if (cur_cargo < 0.005)
    {
    	cur_cargo = 0;
        throw Error("Cannot refuel ships without cargo!");
    }
    refueler_state = REFUELING;
    destination_ship = ship;
    destination = ship->get_location();
    Ship::set_destination_position_and_speed(destination, get_maximum_speed());

    cout << get_name() << " is going to refuel " << ship->get_name() << endl;

    return;
}

// Update the Refueler's state
void Refueler::update()
{
	Ship::update();

    switch (refueler_state)
    {
        case REFUELING:
            {
            shared_ptr<Ship> p = destination_ship.lock();
            if (!p)
            {
            	refueler_state = NOT_REFUELING;
                throw Error("The ship sunk");
            }        
        	// Once the destination is reached, offer the ship as much fuel
        	// as currently in the cargo hold.
            if (get_location() == destination)
            {
                refueler_state = NOT_REFUELING;
                double amount = p->refuel(cur_cargo);
                cur_cargo -= amount;
                cout << get_name() << " refueled " << p->get_name() << " with " << amount << " tons of fuel\n";
            }
            else
            {
            	cout << get_name() << " traveling to refuel " << p->get_name() << endl;
            }
            break;
            }
        case NOT_REFUELING:
            break;
        default:
			// This case will never be reached
			assert(false);
            break;	
    }

	return;
}

// Print out a description about the current state of the Refueler
void Refueler::describe() const
{
	cout << "\nRefueler ";
	Ship::describe();
	
	cout << "Cargo: " << cur_cargo << " tons\n";
	
	return;
}


