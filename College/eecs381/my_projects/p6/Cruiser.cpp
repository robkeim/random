#include "Cruiser.h"

using std::cout;
using std::endl;
using std::string;
using std::tr1::shared_ptr;

Cruiser::Cruiser(const string& name_, Point position_) :
	Warship(name_, position_, 1000, 20., 10., 6, 3, 15)
{ }

// If a ship attacks, and the cruiser is not currently attacking, have it
// counter-attack
void Cruiser::receive_hit(int hit_force, shared_ptr<Ship> attacker)
{
    Ship::receive_hit(hit_force, attacker);

    if (!is_attacking())
    {
        attack(attacker);
    }

    return;
}

// Attack the target if it's in range, otherwise stop the attack on that ship
void Cruiser::update()
{
	Warship::update();	
	
	if (is_attacking())
	{
		if (target_in_range())
		{
			fire_at_target();
		}
		else
		{
			cout << get_name() << " target is out of range" << endl;
			stop_attack();
		}
	}
	
	return;
}

// Display that this is a cruiser and then do a Warship update
void Cruiser::describe() const
{
	cout << "\nCruiser ";
	Warship::describe();

	return;
}

