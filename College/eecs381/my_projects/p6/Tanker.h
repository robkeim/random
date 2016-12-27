#ifndef TANKER_H
#define TANKER_H

#include "Ship.h"

// A Tanker is a ship with a large cargo capacity for fuel.
// It can be told an Island to load fuel at, and an Island to unload at.
// Once it is sent to the loading destination, it will start shuttling between 
// the loading and unloading destination. At the loading destination, 
// it will first refuel then wait until its cargo hold is full, then it will
// go to the unloading destination.

// Initial values:
// fuel capacity and initial amount 100 tons, maximum speed 10., fuel consumption 2.tons/nm, 
// resistance 0, cargo capacity 1000 tons, initial cargo is 0 tons.

class Island;

class Tanker : public Ship
{
public:
	Tanker(const std::string& name_, Point position_);

	// This class overrides these Ship functions so that it can check if this Tanker has assigned cargo destinations.
	// if so, throw an Error("Tanker has cargo destinations!"); otherwise, simply call the Ship functions.
	virtual void set_destination_position_and_speed(Point destination, double speed);
	virtual void set_course_and_speed(double course, double speed);

	// Set the loading and unloading Island destinations
	// if both cargo destination are already set, throw Error("Tanker has cargo destinations!").
	// if they are the same, leave at the set values, and throw Error("Load and unload cargo distinations are the same!")
	// if both destinations are now set, start the cargo cycle
	virtual void set_load_destination(std::tr1::shared_ptr<Island>);
	virtual void set_unload_destination(std::tr1::shared_ptr<Island>);
	
	// when told to stop, clear the cargo destinations and stop
	virtual void stop();
	
	virtual void update();
	virtual void describe() const;
		
private:
	double cur_cargo;
	double cargo_capacity;
	std::tr1::shared_ptr<Island> load_destination;
	std::tr1::shared_ptr<Island> unload_destination;
		
	enum Tanker_states_e
	{
		NO_CARGO_DESTINATIONS,
		MOVING_TO_LOADING,
		LOADING,
		MOVING_TO_UNLOADING,
		UNLOADING
	} tanker_state;
		
	// Update the Tanker's state appropriate if it has both a load and unload
	// destination specified
	void update_state();
};

#endif
