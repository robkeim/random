#include "Model.h"

#include <algorithm>
#include <tr1/functional>

#include "Island.h"
#include "Ship.h"
#include "Ship_factory.h"
#include "Utility.h"
#include "Views.h"

using std::list;
using std::map;
using std::string;
using std::tr1::bind;
using std::tr1::placeholders::_1;
using std::tr1::shared_ptr;

// Return a reference to the model, using  Meyer's Singleton to ensure that
// the Model will be destructed when the program terminates
Model &Model::get_Instance()
{
	static Model model;
	
	return model;
}

// Create the initial objects
Model::Model()
{
	add_island(shared_ptr<Island>(new Island("Exxon", Point(10, 10), 1000, 200)));
	add_island(shared_ptr<Island>(new Island("Shell", Point(0, 30), 1000, 200)));
	add_island(shared_ptr<Island>(new Island("Bermuda", Point(20, 20))));
	add_island(shared_ptr<Island>(new Island("Treasure_Island", Point(50, 5), 100, 5)));
	
	add_ship(create_ship("Ajax", "Cruiser", Point (15, 15)));
	add_ship(create_ship("Xerxes", "Cruiser", Point (25, 25)));
	add_ship(create_ship("Valdez", "Tanker", Point (30, 30)));
}

// is name already in use for either ship or island?
bool Model::is_name_in_use(const string& name) const
{
	return (objects.find(name) != objects.end()) || (groups.find(name) != groups.end());
}

// is there such an island?
bool Model::is_island_present(const string& name) const
{
	return islands.find(name) != islands.end();
}

// add a new island to the lists
void Model::add_island(shared_ptr<Island> island)
{
	string name = island->get_name();
	islands[name] = island;
	objects[name] = island;

	for_each(views.begin(), views.end(), bind(&View::update_location, _1, name, island->get_location()));	
	
	return;
}

// will throw Error("Island not found!") if no island of that name
shared_ptr<Island> Model::get_island_ptr(const string& name) const
{
	if (islands.find(name) == islands.end())
	{
		throw Error("Island not found!");
	}
	
	return islands.find(name)->second;
}

// returns a list of the current islands the model knows about
list<shared_ptr<Island> > Model::get_islands()
{
	list<shared_ptr<Island> > island_list;

	Islands_it_t it;
	for (it = islands.begin(); it != islands.end(); ++it)
	{
		island_list.push_back(it->second);
	}
	
	return island_list;
}

// is there such an ship?
bool Model::is_ship_present(const string& name) const
{
	return ships.find(name) != ships.end();
}

// add a new ship to the list, and update the views
void Model::add_ship(shared_ptr<Ship> ship)
{
	string name = ship->get_name();
	ships[name] = ship;
	objects[name] = ship;
	
	for_each(views.begin(), views.end(), bind(&View::update_location, _1, name, ship->get_location()));

    return;
}

// will throw Error("Ship not found!") if no ship of that name
shared_ptr<Ship> Model::get_ship_ptr(const string& name) const
{
	if (ships.find(name) == ships.end())
	{
		throw Error("Ship not found!");
	}
	
	return ships.find(name)->second;
}

// update the views, remove the ship and then delete it
void Model::remove_ship(shared_ptr<Ship> ship)
{
	for_each(views.begin(), views.end(), bind(&View::update_remove, _1, ship->get_name()));

	objects.erase(ship->get_name());
	ships.erase(ship->get_name());
	
	return;
}

// is there such a group?
bool Model::is_group_present(const string& name) const
{
    return groups.find(name) != groups.end();
}

// add a group and throw an Error if a group with that name already exists
void Model::add_group(shared_ptr<Component> component)
{
    string name = component->get_name();

    if (is_group_present(name))
    {
        throw Error("Group with this name already exists");
    }

    groups[name] = component;

    return;
}

// return a pointer to the group given it's name
shared_ptr<Component> Model::get_group_ptr(const std::string& name) const
{
    if (!is_group_present(name))
    {
        return shared_ptr<Component>();
    }

    return groups.find(name)->second;
}

// remove a group and throw an Error if the specified group does not exist
void Model::remove_group(std::tr1::shared_ptr<Component> component)
{
    string name = component->get_name();
    
    if (!is_group_present(name))
    {
        throw Error("Group with this name does not exist");
    }

    groups.erase(name);

    return;
}

// tell all objects to describe themselves
void Model::describe() const
{
	Objects_const_it_t it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		it->second->describe();
	}

	return;
}

// increment the time, and tell all objects to update themselves
// then update the views.
void Model::update()
{
	time++;

	Objects_const_it_t it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		it->second->update();
		
		for_each(views.begin(), views.end(), bind(&View::update_location, _1, it->first, it->second->get_location()));
    }
	
	Ships_it_t ship_it;
	for (ship_it = ships.begin(); ship_it != ships.end(); ++ship_it)
	{
		if (!ship_it->second->is_afloat())
		{
			for_each(views.begin(), views.end(), bind(&View::update_remove, _1, ship_it->first));
		
            objects.erase(ship_it->second->get_name());
			ships.erase(ship_it);
		}
	}
	
	return;
}

// Remove all of the objects the model currently knows about
void Model::clear_objects()
{
	islands.clear();
	ships.clear();
	objects.clear();
	
	return;
}
	
/* View services */
// Attaching a View causes it to be updated with all current objects'location.
void Model::attach(shared_ptr<View> view_)
{
	views.push_back(view_);

	Objects_it_t it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		view_->update_location(it->first, it->second->get_location());	
    }

	return;
}

// Detach the View by discarding the pointer - no updates sent to it thereafter.
void Model::detach(shared_ptr<View> view_)
{
	Views_it_t it = find(views.begin(), views.end(), view_);
	
	views.erase(it);
	
	return;
}

