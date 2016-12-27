#include "Model.h"

#include "Island.h"
#include "Utility.h"
#include "Ship.h"
#include "Ship_factory.h"
#include "View.h"

using std::cout;
using std::endl;
using std::map;
using std::string;
	
// create the initial objects, output constructor message
Model::Model() :
	time(0), view(0)
{
	add_island(new Island("Exxon", Point(10, 10), 1000, 200));
	add_island(new Island("Shell", Point(0, 30), 1000, 200));
	add_island(new Island("Bermuda", Point(20, 20)));
	
	add_ship(create_ship("Ajax", "Cruiser", Point (15, 15)));
	add_ship(create_ship("Xerxes", "Cruiser", Point (25, 25)));
	add_ship(create_ship("Valdez", "Tanker", Point (30, 30)));
	cout << "Model constructed" << endl;
}

// destroy all objects, output destructor message
Model::~Model()
{
	map<string, Sim_object*>::iterator it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		delete it->second;
	}
	
	cout << "Model destructed" << endl;
}

// is name already in use for either ship or island?
bool Model::is_name_in_use(const string& name) const
{
	return objects.find(name) != objects.end();
}


// is there such an island?
bool Model::is_island_present(const string& name) const
{
	return islands.find(name) != islands.end();
}

// add a new island to the lists
void Model::add_island(Island *island)
{
	string name = island->get_name();
	islands[name] = island;
	objects[name] = island;
	
	if (view)
	{
		view->update_location(name, island->get_location());
	}

	return;
}

// will throw Error("Island not found!") if no island of that name
Island * Model::get_island_ptr(const string& name) const
{
	if (islands.find(name) == islands.end())
	{
		throw Error("Island not found!");
	}
	
	return islands.find(name)->second;
}

// is there such an ship?
bool Model::is_ship_present(const string& name) const
{
	return ships.find(name) != ships.end();
}

// add a new ship to the list, and update the view
void Model::add_ship(Ship *ship)
{
	string name = ship->get_name();
	ships[name] = ship;
	objects[name] = ship;
	if (view)
    {
        view->update_location(name, ship->get_location());
    }

    return;
}

// will throw Error("Ship not found!") if no ship of that name
Ship * Model::get_ship_ptr(const string& name) const
{
	if (ships.find(name) == ships.end())
	{
		throw Error("Ship not found!");
	}
	
	return ships.find(name)->second;
}

// update the view, remove the ship and then delete it
void Model::remove_ship(Ship *ship)
{
	if (view)
    {
        view->update_remove(ship->get_name());
    }

	objects.erase(ship->get_name());
	ships.erase(ship->get_name());
	
	delete ship;
	
	return;
}
	
// tell all objects to describe themselves
void Model::describe() const
{
	map<string, Sim_object*>::const_iterator it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		it->second->describe();
	}

	return;
}

// increment the time, and tell all objects to update themselves
// then update the view.
void Model::update()
{
	time++;

	map<string, Sim_object*>::const_iterator it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		it->second->update();
		if (view)
        {
            view->update_location(it->first, it->second->get_location());
	    }
    }
	
	map<string, Ship*>::iterator ship_it;
	for (ship_it = ships.begin(); ship_it != ships.end(); ++ship_it)
	{
		if (ship_it->second->is_on_the_bottom())
		{
			if (view)
            {
                view->update_remove(ship_it->first);
            }
            objects.erase(ship_it->second->get_name());
			delete ship_it->second;
			ships.erase(ship_it);
		}
	}
	
	return;
}
	
/* View services */
// Attaching a View causes it to be updated with all current objects'location.
void Model::attach(View *view_)
{
	view = view_;

	map<string, Sim_object*>::iterator it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		if (view)
        {
            view->update_location(it->first, it->second->get_location());
	    }
    }

	return;
}

// Detach the View by discarding the pointer - no updates sent to it thereafter.
// If there were multiple Views, the pointer would be supplied to identify the Views.
void Model::detach()
{
	view = 0;
	
	return;
}

