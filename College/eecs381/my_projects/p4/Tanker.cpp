#include "Tanker.h"

#include <cassert>

#include "Island.h"
#include "Utility.h"

using std::cout;
using std::endl;
using std::string;

// initialize, the output constructor message
Tanker::Tanker(const string& name_, Point position_) :
	Ship(name_, position_, 100, 10., 2., 0), cur_cargo(0), cargo_capacity(1000),
	load_destination(0), unload_destination(0), tanker_state(NO_CARGO_DESTINATIONS)
{
	cout << "Tanker " << get_name() << " constructed" << endl;
}

// output destructor message
Tanker::~Tanker()
{
	cout << "Tanker " << get_name() << " destructed" << endl;
}
	
// This class overrides these Ship functions so that it can check if this Tanker has assigned cargo destinations.
// if so, throw an Error("Tanker has cargo destinations!"); otherwise, simply call the Ship functions.
void Tanker::set_destination_position_and_speed(Point destination, double speed)
{
	if (tanker_state != NO_CARGO_DESTINATIONS)
	{
		throw Error("Tanker has cargo destinations!");
	}
	
	Ship::set_destination_position_and_speed(destination, speed);
	
	return;
}

void Tanker::set_course_and_speed(double course, double speed)
{
	if (tanker_state != NO_CARGO_DESTINATIONS)
	{
		throw Error("Tanker has cargo destinations!");
	}
	
	Ship::set_course_and_speed(course, speed);
	
	return;
}

// Set the loading and unloading Island destinations
// if both cargo destination are already set, throw Error("Tanker has cargo destinations!").
// if they are the same, leave at the set values, and throw Error("Load and unload cargo distinations are the same!")
// if both destinations are now set, start the cargo cycle
void Tanker::set_load_destination(Island * load_destination_)
{
	if (tanker_state != NO_CARGO_DESTINATIONS)
	{
		throw Error("Tanker has cargo destinations!");
	}
	
	if (unload_destination == load_destination_)
	{
		throw Error("Load and unload cargo destinations are the same!");
	}
	
	load_destination = load_destination_;
	
	cout << get_name() << " will load at " << load_destination->get_name() << endl;
	
	update_state();
	
	return;
}

void Tanker::set_unload_destination(Island *unload_destination_)
{
	if (tanker_state != NO_CARGO_DESTINATIONS)
	{
		throw Error("Tanker has cargo destinations!");
	}
	
	if (load_destination == unload_destination_)
	{
		throw Error("Load and unload cargo destinations are the same!");
	}
	
	unload_destination = unload_destination_;
	
	cout << get_name() << " will unload at " << unload_destination->get_name() << endl;
	
	update_state();
	return;
}

void Tanker::update_state()
{
	if (!load_destination || !unload_destination)
	{
		return;
	}
	
	if (is_docked())
	{
		if (get_docked_Island() == load_destination)
		{
			tanker_state = LOADING;			
			return;
		}
		
		if (get_docked_Island() == unload_destination)
		{
			tanker_state = UNLOADING;		
			return;
		}
	}
	
	if (!is_moving())
	{
		if (can_dock(load_destination))
		{
			tanker_state = LOADING;
			return;
		}
		
		if (can_dock(unload_destination))
		{
			tanker_state = UNLOADING;
			return;
		}
	}
	
	if (!cur_cargo)
	{
		set_destination_position_and_speed(load_destination->get_location(), get_maximum_speed());
		tanker_state = MOVING_TO_LOADING;
		return;
	}
	
	set_destination_position_and_speed(unload_destination->get_location(), get_maximum_speed());
	tanker_state = MOVING_TO_UNLOADING;
	return;
}

// when told to stop, clear the cargo destinations and stop
void Tanker::stop()
{
	Ship::stop();
	
	load_destination = unload_destination = 0;
	tanker_state = NO_CARGO_DESTINATIONS;
	
	cout << get_name() << " now has no cargo destinations" << endl;
	
	return;
}
	
void Tanker::update()
{
	Ship::update();
	
	if (!can_move())
	{
		tanker_state = NO_CARGO_DESTINATIONS;
		load_destination = unload_destination = 0;
		cout << get_name() << " now has no cargo destinations" << endl;
	}

	switch (tanker_state)
	{
		case NO_CARGO_DESTINATIONS:
			break;
		case MOVING_TO_LOADING:
			if (!is_moving() && can_dock(load_destination))
			{
				dock(load_destination);
				tanker_state = LOADING;
			}
			break;
		case MOVING_TO_UNLOADING:
			if (!is_moving() && can_dock(unload_destination))
			{
				dock(unload_destination);
				tanker_state = UNLOADING;
			}
			break;
		case LOADING:
            {
            Ship::refuel();
			double cargo_needed = cargo_capacity - cur_cargo;
			
			if (cargo_needed < 0.005)
			{
				cur_cargo = cargo_capacity;
				Ship::set_destination_position_and_speed(unload_destination->get_location(), get_maximum_speed());
				tanker_state = MOVING_TO_UNLOADING;
			}
			else
			{
				cur_cargo += load_destination->provide_fuel(cargo_needed);
				cout << get_name() << " now has " << cur_cargo << " of cargo" << endl;
			}
			}
			break;
		case UNLOADING:
			if (cur_cargo == 0.0)
			{
				Ship::set_destination_position_and_speed(load_destination->get_location(), get_maximum_speed());
				tanker_state = MOVING_TO_LOADING;
			}
			else
			{
				unload_destination->accept_fuel(cur_cargo);
				cur_cargo = 0.0;
			}
			break;
		default:
			// This case will never be reached
			assert(false);
            break;	
	}

	return;
}

void Tanker::describe() const
{
	cout << "\nTanker ";
	Ship::describe();
	
	cout << "Cargo: " << cur_cargo << " tons";
	
	switch (tanker_state)
	{
		case NO_CARGO_DESTINATIONS:
			cout << ", no cargo destinations";
			break;
		case LOADING:
			cout << ", loading";
			break;
		case UNLOADING:
			cout << ", unloading";
			break;
		case MOVING_TO_LOADING:
			cout << ", moving to loading destination";
			break;
		case MOVING_TO_UNLOADING:
			cout << ", moving to unloading destination";
			break;
		default:
			// This will never execute
			assert(false);
            break;
	}
	
	cout << endl;
	
	return;
}

