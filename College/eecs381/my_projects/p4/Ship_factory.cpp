#include "Ship_factory.h"

// The types of ships the factory can create
#include "Cruiser.h"
#include "Tanker.h"

#include "Utility.h"

using std::string;

// may throw Error("Trying to create ship of unknown type!")
Ship * create_ship(const string& name, const string& type, Point initial_position)
{
	Ship *ship;
	
	if (type == "Cruiser")
	{
		ship = new Cruiser(name, initial_position);
	}
	else if (type == "Tanker")
	{
		ship = new Tanker(name, initial_position);
	}
	else
	{
		throw Error("Trying to create ship of unknown type!");
	}
	
	return ship;
}

