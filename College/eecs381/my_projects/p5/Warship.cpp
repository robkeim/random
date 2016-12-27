#include "Warship.h"

#include "Utility.h"

using std::cout;
using std::endl;
using std::string;
using std::tr1::shared_ptr;

Warship::Warship(const string& name_, Point position_, double fuel_capacity_, 
	double maximum_speed_, double fuel_consumption_factor_, int resistance_,
	int firepower_, double maximum_range_) :
		Ship(name_, position_, fuel_capacity_, maximum_speed_, fuel_consumption_factor_, resistance_), firepower(firepower_), maximum_range(maximum_range_), warship_state(NOT_ATTACKING)
{ }

// a pure virtual function to mark this as an abstract class
Warship::~Warship()
{ }
	
// perform warship-specific behavior
void Warship::update()
{
    Ship::update();

    if (warship_state != ATTACKING)
    {
        return;
    }

    if (!is_afloat() || !get_target()->is_afloat())
    {
        stop_attack();
        return;
    }  
    
    cout << get_name() << " is attacking " << endl;

    return;
}

// Warships will act on an attack and stop_attack command

// will	throw Error("Cannot attack!") if not Afloat
// will throw Error("Warship may not attack itself!") if supplied target is the same as this Warship
void Warship::attack(shared_ptr<Ship> target_ptr_)
{
	if (!is_afloat())
	{
		throw Error("Cannot attack!");
	}
	
	if (this == target_ptr_.get())
	{
		throw Error("Warship may not attack itself!");
	}
	
	target = target_ptr_;
	warship_state = ATTACKING;
	
	cout << get_name() << " will attack " << target->get_name() << endl;
	
	return;
}

// will throw Error("Was not attacking!") if not Attacking
void Warship::stop_attack()
{
	if (warship_state != ATTACKING)
	{
		throw Error("Was not attacking!");
	}
	
	cout << get_name() << " stopping attack" << endl;
	
	warship_state = NOT_ATTACKING;
	target.reset();
	
	return;
}

void Warship::describe() const
{
	Ship::describe();
	
	if (warship_state == ATTACKING)
	{	
		if (get_target()->is_afloat())
		{
			cout << "Attacking " << target->get_name() << endl;
		}
		else
		{
			cout << "Attacking absent ship" << endl;
		}
	}
	
	return;
}

// return true if this Warship is in the attacking state
bool Warship::is_attacking() const
{
	return warship_state == ATTACKING;
}
	
// fire at the current target
void Warship::fire_at_target()
{
	cout << get_name() << " fires" << endl;
	
	target->receive_hit(firepower, shared_from_this());
	
	return;
}
		
// is the current target in range?
bool Warship::target_in_range() const
{
	double distance = cartesian_distance(get_location(), target->get_location());
	
	return distance < maximum_range;
}

// get the target
shared_ptr<Ship> Warship::get_target() const
{
	return target;
}

