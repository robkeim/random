#include "Cruiser.h"

using std::cout;
using std::endl;
using std::string;

// initialize, then output constructor message
Cruiser::Cruiser(const string& name_, Point position_) :
	Warship(name_, position_, 1000, 20., 10., 6, 3, 15)
{
	cout << "Cruiser " << name_ << " constructed" << endl;
}

// output destructor message
Cruiser::~Cruiser()
{
	cout << "Cruiser " << get_name() << " destructed" << endl;
}

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

void Cruiser::describe() const
{
	cout << "\nCruiser ";
	Warship::describe();

	return;
}

