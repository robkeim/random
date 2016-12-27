#ifndef WARSHIP_H
#define WARSHIP_H

/* Warship class
A Warship is a ship with firepower and range member variables, and some services for
protected classes to manage many of the details of warship behavior. This is an
abstract base class, so concrete classes derived from Warship must be declared.
*/

#include <tr1/memory>

#include "Ship.h"

class Warship : public Ship, public std::tr1::enable_shared_from_this<Warship>
{
	public:
		// initialize, then output constructor message
		Warship(const std::string& name_, Point position_, double fuel_capacity_, 
			double maximum_speed_, double fuel_consumption_factor_, int resistance_,
			int firepower_, double maximum_range_);

		// a pure virtual function to mark this as an abstract class, 
		// but defined anyway to output destructor message
		virtual ~Warship() = 0;
	
		// perform warship-specific behavior
		virtual void update();

		// Warships will act on an attack and stop_attack command

		// will	throw Error("Cannot attack!") if not Afloat
		// will throw Error("Warship may not attack itself!") if supplied target is the same as this Warship
		virtual void attack(std::tr1::shared_ptr<Ship> target_ptr_);

		// will throw Error("Was not attacking!") if not Attacking
		virtual void stop_attack();
	
		virtual void describe() const;

	protected:
		// future projects may need additional protected members

		// return true if this Warship is in the attacking state
		bool is_attacking() const;
	
		// fire at the current target
		void fire_at_target();
		
		// is the current target in range?
		bool target_in_range() const;

		// get the target
		std::tr1::shared_ptr<Ship> get_target() const;

    private:
        int firepower;
        double maximum_range;
        std::tr1::shared_ptr<Ship> target;
        
        enum warship_state_e
		{
			ATTACKING,
			NOT_ATTACKING
		} warship_state;

};

#endif
