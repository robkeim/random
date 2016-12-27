#include "Ship_factory.h"

// The types of ships the factory can create
#include "Cruise_ship.h"
#include "Cruiser.h"
#include "Tanker.h"

#include "Utility.h"

using std::string;
using std::tr1::shared_ptr;

// may throw Error("Trying to create ship of unknown type!")
shared_ptr<Ship> create_ship(const string& name, const string& type, Point initial_position)
{
	if (type == "Cruiser")
	{
		return shared_ptr<Ship>(new Cruiser(name, initial_position));
	}
	else if (type == "Tanker")
	{
		return shared_ptr<Ship>(new Tanker(name, initial_position));
	}
	else if (type == "Cruise_ship")
	{
		return shared_ptr<Cruise_ship>(new Cruise_ship(name, initial_position));
	}
	
	throw Error("Trying to create ship of unknown type!");
}

