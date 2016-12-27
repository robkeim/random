#include "Ship.h"

#include "Island.h"
#include "Utility.h"

#include <cassert>
#include <string>

using std::cout;
using std::endl;
using std::string;
using std::tr1::shared_ptr;

Ship::Ship(const string& name_, Point position_, double fuel_capacity_,
        double maximum_speed_, double fuel_consumption_, int resistance_) :
    Sim_object(name_), Track_base(position_), fuel(fuel_capacity_), fuel_capacity(fuel_capacity_), 
    maximum_speed(maximum_speed_), fuel_consumption(fuel_consumption_), 
    resistance(resistance_), ship_state(STOPPED)
{ }

Ship::~Ship()
{ }

// Return true if ship can move (it is not dead in the water or in the process or sinking); 
bool Ship::can_move() const
{
    return ship_state != DEAD_IN_THE_WATER && is_afloat();
}
	
// Return true if ship is moving; 
bool Ship::is_moving() const
{
    return ship_state == MOVING_TO_POSITION || ship_state == MOVING_ON_COURSE;
}

// Return true if ship is docked; 
bool Ship::is_docked() const
{
    return ship_state == DOCKED;
}
	
// Return true if ship is afloat (not in process of sinking), false if not
bool Ship::is_afloat() const
{
    return ship_state != SUNK;
}
	
// Return true if Stopped and within 0.1 nm of the island
bool Ship::can_dock(shared_ptr<Island> island_ptr) const
{
    double distance = cartesian_distance(get_location(), island_ptr->get_location());

    return ship_state == STOPPED && distance <= 0.1;
}
	
/*** Interface to derived classes ***/
// Update the state of the Ship
void Ship::update()
{
    if (is_afloat() && resistance < 0)
    {
        ship_state = SUNK;
        cout << get_name() << " sunk" << endl;
        return;
    }
    
    switch (ship_state)
    {
        case MOVING_ON_COURSE:
        case MOVING_TO_POSITION:
            calculate_movement();
            cout << get_name() << " now at " << get_position() << endl;
            break;
        case STOPPED:
            cout << get_name() << " stopped at " << get_position() << endl;
            break;
        case DOCKED:
            cout << get_name() << " docked at " << get_docked_Island()->get_name() << endl;
            break;
        case DEAD_IN_THE_WATER:
            cout << get_name() << " dead in the water at " << get_position() << endl;
            break;
        default:
            // This case will never be reached
            assert(false);
            break;
    }

    return;
}

// output a description of current state to cout
void Ship::describe() const
{
	cout << get_name() << " at " << get_position();
	
    if (!is_afloat())
    {
		cout << " sunk" << endl;
        return;
    }
    
    cout << ", fuel: " << fuel << " tons" << ", resistance: " << resistance << endl;
    
    switch (ship_state)
    {
        case MOVING_ON_COURSE:
            cout << "Moving on " << get_course_speed() << endl;
        	break;
        case MOVING_TO_POSITION:
        	cout << "Moving to " << destination << " on " << get_course_speed() << endl;
            break;
        case STOPPED:
			cout << "Stopped" << endl;
            break;
        case DOCKED:
      		cout << "Docked at " << get_docked_Island()->get_name() << endl;
            break;
        case DEAD_IN_THE_WATER:
			cout << "Dead in the water" << endl;
            break;
        default:
            // This case will never be reached
            assert(false);
            break;
    }

	return;
}
	
/*** Command functions ***/
// Start moving to a destination position at a speed
 // may throw Error("Ship cannot move!")
 // may throw Error("Ship cannot go that fast!")
void Ship::set_destination_position_and_speed(Point destination_position, double speed)
{
	if (!can_move())
	{
		throw Error("Ship cannot move!");
	}
	
	if (speed > maximum_speed)
	{
		throw Error("Ship cannot go that fast!");
	}

	destination = destination_position;
	
	Compass_vector compass_vector(get_location(), destination);
	set_course(compass_vector.direction);
	
	set_speed(speed);
	
	cout << get_name() << " will sail on " << get_course_speed() << " to " << destination << endl;
	
	ship_state = MOVING_TO_POSITION;
	
	return;
}

// Start moving on a course and speed
 // may throw Error("Ship cannot move!")
 // may throw Error("Ship cannot go that fast!");
void Ship::set_course_and_speed(double course, double speed)
{
	if (!can_move())
	{
		throw Error("Ship cannot move!");
	}
	
	if (speed > maximum_speed)
	{
		throw Error("Ship cannot go that fast!");
	}
	
	set_course(course);
	set_speed(speed);
	
	cout << get_name() << " will sail on " << get_course_speed() << endl;

	ship_state = MOVING_ON_COURSE;

	return;
}

// Stop moving
 // may throw Error("Ship cannot move!")
void Ship::stop()
{
	if (!can_move())
	{
		throw Error("Ship cannot move!");
	}
	
	set_speed(0);
	
	cout << get_name() << " stopping at " << get_position() << endl;
	
	ship_state = STOPPED;
	
	return;
}

// dock at an Island - set our position = Island's position, go into Docked state
 // may throw Error("Can't dock!");
void Ship::dock(shared_ptr<Island> island_ptr)
{
	if (ship_state != STOPPED || !can_dock(island_ptr))
	{
		throw Error("Can't dock!");
	}
	
	set_position(island_ptr->get_location());
	cur_island = island_ptr;
	ship_state = DOCKED;
	
	cout << get_name() << " docked at " << island_ptr->get_name() << endl;
	
	return;
}

// Refuel - must already be docked at an island; fill takes as much as possible
 // may throw Error("Must be docked!");
void Ship::refuel()
{
	if (ship_state != DOCKED)
	{
		throw Error("Must be docked!");
	}
	
	double fuel_needed = fuel_capacity - fuel;
	
	if (fuel_needed < 0.005)
	{
		fuel = fuel_capacity;
		return;
	}
	
	fuel += cur_island->provide_fuel(fuel_needed);
	
	cout << get_name() << " now has " << fuel << " tons of fuel" << endl;
	
	return;
}

/*** Fat interface command functions ***/
// These functions throw an Error exception for this class
// will always throw Error("Cannot load at a destination!");
void Ship::set_load_destination(shared_ptr<Island>)
{
	throw Error("Cannot load at a destination!");
	
	return;
}

// will always throw Error("Cannot unload at a destination!");
void Ship::set_unload_destination(shared_ptr<Island>)
{
	throw Error("Cannot unload at a destination!");
	
	return;
}

// will always throw Error("Cannot attack!");
void Ship::attack(shared_ptr<Ship> in_target_ptr)
{
	throw Error("Cannot attack!");
	
	return;
}

// will always throw Error("Cannot attack!");
void Ship::stop_attack()
{
	throw Error("Cannot attack!");
	
	return;
}

// interactions with other objects
// receive a hit from an attacker
void Ship::receive_hit(int hit_force, shared_ptr<Ship> attacker)
{
	resistance -= hit_force;
	cout << get_name() << " hit with " << hit_force << ", resistance now " << resistance << endl;
	
	return;
}

double Ship::get_maximum_speed() const
{
	return maximum_speed;
}

// return pointer to the Island currently docked at, or 0 if not docked
shared_ptr<Island> Ship::get_docked_Island() const
{
	if (is_docked())
	{
		return cur_island;
	}
	return shared_ptr<Island>();
}

/* Private Function Definitions */

/*
Calculate the new position of a ship based on how it is moving, its speed, and
fuel state. This function should be called only if the state is 
MOVING_TO_DESTINATION or MOVING_ON_COURSE.

Track_base has an update_position(double time) function that computes the new position
of an object after the specified time has elapsed. If the Ship is going to move
for a full time unit (one hour), then it will get go the "full step" distance,
so update_position would be called with time = 1.0. If we can move less than that,
e.g. due to not enough fuel, update position  will be called with the corresponding
time less than 1.0.

For clarity in specifying the computation, this code assumes the specified private variable names, 
but you may modify any other variables if you wish (e.g. movement_state).
*/
void Ship:: calculate_movement()
{
	// Compute values for how much we need to move, and how much we can, and how long we can,
	// given the fuel state, then decide what to do.
	double time = 1.0;	// "full step" time
	// get the distance to destination
	double destination_distance = cartesian_distance(get_location(), destination);
	// get full step distance we can move on this time step
	double full_distance = get_speed() * time;
	// get fuel required for full step distance
	double full_fuel_required = full_distance * fuel_consumption;	// tons = nm * tons/nm
	// how far and how long can we sail in this time period based on the fuel state?
	double distance_possible, time_possible;
	if(full_fuel_required <= fuel) {
		distance_possible = full_distance;
		time_possible = time;
		}
	else {
		distance_possible = fuel / fuel_consumption;	// nm = tons / tons/nm
		time_possible = (distance_possible / full_distance) * time;
		}
	
	// are we are moving to a destination, and is the destination within the distance possible?
	if(ship_state == MOVING_TO_POSITION && destination_distance <= distance_possible) {
		// yes, make our new position the destination
		set_position(destination);
		// we travel the destination distance, using that much fuel
		double fuel_required = destination_distance * fuel_consumption;
		fuel -= fuel_required;
		set_speed(0.);
		ship_state = STOPPED;
		}
	else {
		// go as far as we can, stay in the same movement state
		// simply move for the amount of time possible
		update_position(time_possible);
		// have we used up our fuel?
		if(full_fuel_required >= fuel) {
			fuel = 0.0;
			ship_state = DEAD_IN_THE_WATER;
			}
		else {
			fuel -= full_fuel_required;
			}
		}
}

