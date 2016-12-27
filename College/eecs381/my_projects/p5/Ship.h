#ifndef SHIP_H
#define SHIP_H

/***** Ship Class *****/
/* A Ship has a name, initial position, amount of fuel, and parameters 
that govern its movement. It can be commanded to move to either a position or follow a course, or stop,
dock at or refuel at an Island. It consumes fuel while moving, and becomes immobile
if it runs out of fuel. It inherits the Sim_object interface to the rest of the system,
and the Track_base class provides the basic movement functionality.

The update function updates the position and/or state of the ship.
The describe function outputs information about the ship state.
Accessors make the ship state available to either the public or to derived classes.
The is a "fat interface" for the capabilities of derived types of Ships. These
functions are implemented in this class to throw an Error exception.
*/

#include <tr1/memory>

#include "Sim_object.h"
#include "Track_base.h"

class Island;

class Ship : public Sim_object, private Track_base
{
	public:
		Ship(const std::string& name_, Point position_, double fuel_capacity_, 
			double maximum_speed_, double fuel_consumption_, int resistance_);
		virtual ~Ship() = 0;
	
		/*** Readers ***/
		// return the current position
		virtual Point get_location() const {return Track_base::get_position();}
		
		double get_course() const {return Track_base::get_course();}
	
		// Return true if ship can move (it is not dead in the water or in the process or sinking); 
		bool can_move() const;
	
		// Return true if ship is moving; 
		bool is_moving() const;

		// Return true if ship is docked; 
		bool is_docked() const;
	
		// Return true if ship is afloat (not in process of sinking), false if not
		bool is_afloat() const;
	
		// Return true if Stopped and within 0.1 nm of the island
		bool can_dock(std::tr1::shared_ptr<Island> island_ptr) const;
	
		/*** Interface to derived classes ***/
		// Update the state of the Ship
		virtual void update();
		// output a description of current state to cout
		virtual void describe() const;
	
		/*** Command functions ***/
		// Start moving to a destination position at a speed
		 // may throw Error("Ship cannot move!")
		 // may throw Error("Ship cannot go that fast!")
		virtual void set_destination_position_and_speed(Point destination_position, double speed);
		// Start moving on a course and speed
		 // may throw Error("Ship cannot move!")
		 // may throw Error("Ship cannot go that fast!");
		virtual void set_course_and_speed(double course, double speed);
		// Stop moving
		 // may throw Error("Ship cannot move!")
		virtual void stop();
		// dock at an Island - set our position = Island's position, go into Docked state
		 // may throw Error("Can't dock!");
		virtual void dock(std::tr1::shared_ptr<Island> island_ptr);
		// Refuel - must already be docked at an island; fill takes as much as possible
		 // may throw Error("Must be docked!");
		virtual void refuel();

		/*** Fat interface command functions ***/
		// These functions throw an Error exception for this class
		// will always throw Error("Cannot load at a destination!");
		virtual void set_load_destination(std::tr1::shared_ptr<Island>);
		// will always throw Error("Cannot unload at a destination!");
		virtual void set_unload_destination(std::tr1::shared_ptr<Island>);
		// will always throw Error("Cannot attack!");
		virtual void attack(std::tr1::shared_ptr<Ship> in_target_ptr);
		// will always throw Error("Cannot attack!");
		virtual void stop_attack();

		// interactions with other objects
		// receive a hit from an attacker
		virtual void receive_hit(int hit_force, std::tr1::shared_ptr<Ship> attacker);
		
	protected:
		// future projects may need additional protected members

		double get_maximum_speed() const;
		// return pointer to the Island currently docked at, or 0 if not docked
		std::tr1::shared_ptr<Island> get_docked_Island() const;

	private:
		double fuel;						// Current amount of fuel
		double fuel_capacity;
        double maximum_speed;
        double fuel_consumption;			// tons/nm requireda
        int resistance;
		Point destination;					// Current destination if any
        std::tr1::shared_ptr<Island> cur_island;

		enum ship_state_e
		{
			DOCKED,
			STOPPED,
			MOVING_TO_POSITION,
			MOVING_ON_COURSE,
			DEAD_IN_THE_WATER,
			SUNK
		} ship_state;

		// Updates position, fuel, and movement_state, assuming 1 time unit (1 hr)
		void calculate_movement();

		// no copy, assignment
		Ship(const Ship&);
		Ship& operator= (const Ship&);
};

#endif
