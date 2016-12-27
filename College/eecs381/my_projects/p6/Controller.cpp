#include "Controller.h"

#include <algorithm>
#include <iostream>
#include <tr1/functional>

#include "Composite.h"
#include "Island.h"
#include "Model.h"
#include "Ship.h"
#include "Ship_factory.h"
#include "Utility.h"
#include "Views.h"

using std::cin;
using std::cout;
using std::endl;
using std::map;
using std::string;
using std::tr1::bind;
using std::tr1::placeholders::_1;
using std::tr1::shared_ptr;

Controller::Controller() :
	model(Model::get_Instance())
{ }

// Run the program by acccepting user commands
void Controller::run()
{
	Group_commands_t group_commands;
    add_group_commands(group_commands);
    
    Ship_commands_t ship_commands;	
	add_ship_commands(ship_commands);

	Commands_t commands;
	add_commands(commands);
	
	while (true)
	{
		cout << "\nTime " << model.get_time() << ": Enter command: ";
	
		string command;
		cin >> command;
	
		if (command == "quit")
		{
			break;
		}

		try
		{
			// Check if the first parameter read in is the name of a group,
            // then the name of a ship, and finally if it is the name of a
            // command
            if (model.is_group_present(command))
            {
                shared_ptr<Component> group = model.get_group_ptr(command);
                cin >> command;

                Group_ptr_t group_fn_ptr = group_commands[command];
                if (group_fn_ptr)
                {
                    (this->*group_fn_ptr)(group);
                }
                else
                {
                    throw Error("Unrecognized command!");
                }
            }            
            else if (model.is_ship_present(command))
			{
				shared_ptr<Ship> ship = model.get_ship_ptr(command);
				cin >> command;
				
				Ship_ptr_t ship_fn_ptr = ship_commands[command];
				if (ship_fn_ptr)
				{
					(this->*ship_fn_ptr)(ship);
				}
				else
				{
					throw Error("Unrecognized command!");
				}
			}
			else
			{
				Ptr_t fn_ptr = commands[command];
				if (fn_ptr)
				{
					(this->*fn_ptr)();
				}
				else
				{
					throw Error("Unrecognized command!");
				}
			}
		}
		catch (Error& error)
		{
			// Print the error message, reset the input stream, and clear the
			// input stream until a newline is reached
			cout << error.msg << endl;
			cin.clear();
			clear_stream();
		}
	}

	cout << "Done" << endl;
	
	model.clear_objects();
	
	return;
}

// Add the group commands to the map
void Controller::add_group_commands(Group_commands_t &group_commands)
{
    group_commands["add"] = &Controller::group_cmd_add;
    group_commands["course"] = &Controller::group_cmd_course;
    group_commands["position"] = &Controller::group_cmd_position;
    group_commands["remove"] = &Controller::group_cmd_remove;
    group_commands["stop"] = &Controller::group_cmd_stop;

    return;
}

// Add an object to a group.  An error occurs if no object with such a name
// exists.  Both objects and groups can be added to groups.
void Controller::group_cmd_add(shared_ptr<Component> component)
{
    string name;
    cin >> name;

    if (!model.is_name_in_use(name))
    {
        throw Error("Object with that name does not exist");
    }

    if (model.is_group_present(name))
    {
        component->add(model.get_group_ptr(name));
    }
    else
    {
        component->add(model.get_ship_ptr(name));
    }

    cout << "Added " << name << " to group " << component->get_name() << endl;

    return;
}

// Read in the course and speed, and set a new course and speed for the
// specified group.
void Controller::group_cmd_course(shared_ptr<Component> group)
{
	double course = get_double(),
		   speed = get_double();
	
	if (course < 0.0 || course >= 360.0)
	{
		throw Error("Invalid heading entered!");
	}
	
	if (speed < 0.0)
	{
		throw Error("Negative speed entered!");
	}
	
	group->set_course_and_speed(course, speed);
	
	return;
}

// Read in a destination point and speed, and set the group's destination 
// and speed
void Controller::group_cmd_position(shared_ptr<Component> group)
{
	double x = get_double(),
		   y = get_double(),
		   speed = get_double();
	
	if (speed < 0.0)
	{
		throw Error("Negative speed entered!");
	}
	
	group->set_destination_position_and_speed(Point(x, y), speed);	
	
	return;
}

// Remove an object from the group
void Controller::group_cmd_remove(std::tr1::shared_ptr<Component> component)
{
    string name;
    cin >> name;

    if (!model.is_name_in_use(name))
    {
        throw Error("Object with that name does not exist");
    }

    shared_ptr<Component> ptr = component->get_child_by_name(name);
    component->remove(ptr);

    cout << name << " removed from group " << component->get_name() << endl;

    return;
}

// Tell the group to stop moving
void Controller::group_cmd_stop(shared_ptr<Component> group)
{
	group->stop();
	
	return;
}

// Add the ship command member functions to the map of ship commands
void Controller::add_ship_commands(Ship_commands_t &ship_commands)
{
	ship_commands["course"] = &Controller::ship_cmd_course;
	ship_commands["position"] = &Controller::ship_cmd_position;
	ship_commands["destination"] = &Controller::ship_cmd_destination;
    ship_commands["fuel"] = &Controller::ship_cmd_fuel;
	ship_commands["load_at"] = &Controller::ship_cmd_load_at;
	ship_commands["unload_at"] = &Controller::ship_cmd_unload_at;
	ship_commands["dock_at"] = &Controller::ship_cmd_dock_at;
	ship_commands["attack"] = &Controller::ship_cmd_attack;
	ship_commands["refuel"] = &Controller::ship_cmd_refuel;
	ship_commands["stop"] = &Controller::ship_cmd_stop;
	ship_commands["stop_attack"] = &Controller::ship_cmd_stop_attack;
	
	return;
}

// Read in the course and speed, and set a new course and speed for the
// specified ship.
void Controller::ship_cmd_course(shared_ptr<Ship> ship)
{
	double course = get_double(),
		   speed = get_double();
	
	if (course < 0.0 || course >= 360.0)
	{
		throw Error("Invalid heading entered!");
	}
	
	if (speed < 0.0)
	{
		throw Error("Negative speed entered!");
	}
	
	ship->set_course_and_speed(course, speed);
	
	return;
}

// Read in a destination point and speed, and set the ship's destination 
// and speed
void Controller::ship_cmd_position(shared_ptr<Ship> ship)
{
	double x = get_double(),
		   y = get_double(),
		   speed = get_double();
	
	if (speed < 0.0)
	{
		throw Error("Negative speed entered!");
	}
	
	ship->set_destination_position_and_speed(Point(x, y), speed);	
	
	return;
}

// Read in a destination island and speed, and set the ship's destination and
// speed to the specified island.
void Controller::ship_cmd_destination(shared_ptr<Ship> ship)
{
	string island_name;
	cin >> island_name;
	
	double speed = get_double();
	
	shared_ptr<Island> island = model.get_island_ptr(island_name);
	
	if (speed < 0.0)
	{
		throw Error("Negative speed entered!");
	}
	
	ship->set_destination_position_and_speed(island->get_location(), speed);
	
	return;
}

void Controller::ship_cmd_fuel(shared_ptr<Ship> ship)
{
    string name;
    cin >> name;

    ship->fuel_ship(model.get_ship_ptr(name));
    
    return;
}

// Set the load destination for a ship
void Controller::ship_cmd_load_at(shared_ptr<Ship> ship)
{	
	shared_ptr<Island> island = get_island_ptr();
	
	ship->set_load_destination(island);
	
	return;
}

// Read in a string and return a pointer to the corresponding island
shared_ptr<Island> Controller::get_island_ptr()
{
	string name;
	cin >> name;
	
	return model.get_island_ptr(name);
}

// Set the unload destination for a ship
void Controller::ship_cmd_unload_at(shared_ptr<Ship> ship)
{	
	shared_ptr<Island> island = get_island_ptr();
	
	ship->set_unload_destination(island);
	
	return;
}

// Dock at a specified island
void Controller::ship_cmd_dock_at(shared_ptr<Ship> ship)
{	
	shared_ptr<Island> island = get_island_ptr();
	
	ship->dock(island);
	
	return;
}

// Set a ship to attack
void Controller::ship_cmd_attack(shared_ptr<Ship> ship)
{
	string ship_name;
	cin >> ship_name;
	
	shared_ptr<Ship> target_ship = model.get_ship_ptr(ship_name);
	
	ship->attack(target_ship);
	
	return;
}

// Refuel the ship
void Controller::ship_cmd_refuel(shared_ptr<Ship> ship)
{
	ship->refuel();
	
	return;
}

// Tell the ship to stop moving
void Controller::ship_cmd_stop(shared_ptr<Ship> ship)
{
	ship->stop();
	
	return;
}

// Tell the ship to stop attacking
void Controller::ship_cmd_stop_attack(shared_ptr<Ship> ship)
{
	ship->stop_attack();
	
	return;
}

// Add the command member functions to the map of commands
void Controller::add_commands(Commands_t &commands)
{
	// Map_View commands
	commands["open_map_view"] = &Controller::cmd_open_map_view;
	commands["close_map_view"] = &Controller::cmd_close_map_view;
	commands["default"] = &Controller::cmd_default;
	commands["size"] = &Controller::cmd_size;
	commands["zoom"] = &Controller::cmd_zoom;
	commands["pan"] = &Controller::cmd_pan;
	commands["show"] = &Controller::cmd_show;
	commands["open_bridge_view"] = &Controller::cmd_open_bridge_view;
	commands["close_bridge_view"] = &Controller::cmd_close_bridge_view;

	// Model commands
	commands["status"] = &Controller::cmd_status;
	commands["go"] = &Controller::cmd_go;
	commands["create"] = &Controller::cmd_create;
    commands["group"] = &Controller::cmd_group;

	return;
}

// Create a new map view
void Controller::cmd_open_map_view()
{
	if (map_view)
	{
		throw Error("Map view is already open!");
	}
	
	map_view = shared_ptr<Map_View>(new Map_View());
	model.attach(map_view);
	views.push_back(map_view);
	
	return;
}

// Close the map view
void Controller::cmd_close_map_view()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
	
	views.remove(map_view);
	model.detach(map_view);
	map_view.reset();
	
	return;
}

// Reset the defaults for the map view
void Controller::cmd_default()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}

    map_view->set_defaults();

	return;
}

// Set the size for the map view
void Controller::cmd_size()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
		
    map_view->set_size(get_int());

	return;
}

// Set the scale for the map view
void Controller::cmd_zoom()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
	
    map_view->set_scale(get_double());

	return;
}

// Set the origin for the map view
void Controller::cmd_pan()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
	
	double x = get_double(),
		   y = get_double();
	
    map_view->set_origin(Point(x, y));

	return;
}

// Draw the views
void Controller::cmd_show()
{
	for_each(views.begin(), views.end(), bind(&View::draw, _1));

	return;
}

// Open a new bridge view
void Controller::cmd_open_bridge_view()
{
	string name;
	cin >> name;

	if (bridge_views.find(name) != bridge_views.end())
	{
		throw Error("Bridge view is already open for that ship!");
	}
	
	shared_ptr<Ship> ship = model.get_ship_ptr(name);

	bridge_views[name] = shared_ptr<Bridge_View>(new Bridge_View(ship));
	model.attach(bridge_views[name]);
	views.push_back(bridge_views[name]);
	
	return;
}

// Close a bridge view
void Controller::cmd_close_bridge_view()
{
	string name;
	cin >> name;
	
	Bridge_view_it_t it = bridge_views.find(name);
	
	if (it == bridge_views.end())
	{
		throw Error("Bridge view for that ship is not open!");
	}
	
	model.detach(it->second);
	views.remove(it->second);
	bridge_views.erase(it);
	
	return;
}

// Display a description of the state
void Controller::cmd_status()
{
	model.describe();
	
	return;
}

// Update the state
void Controller::cmd_go()
{
	model.update();
	
	return;
}

// Create a new ship
void Controller::cmd_create()
{
	string name;
	cin >> name;
	
	if (model.is_ship_present(name))
	{
		throw Error("Name is already in use!");
	}
	
	string type;
	cin >> type;
	
	double x = get_double(),
		   y = get_double();
		
	model.add_ship(create_ship(name, type, Point(x, y)));
	
	return;
}

// Create a new group
void Controller::cmd_group()
{
    string name;
    cin >> name;

    if (model.is_group_present(name))
    {
        throw Error("Name is already in use!");
    }

    model.add_group(shared_ptr<Component>(new Composite(name)));

    cout << "Created group " << name << endl;
}

// Clear stdin until a newline is reached
void Controller::clear_stream()
{
	char c;
	
	cin.get(c);
	while (c != '\n')
	{
		cin.get(c);
	}
	
	return;
}

// Read a double from stdin and throw an error if it fails
double Controller::get_double()
{
	double tmp;
	if(!(cin >> tmp))
	{
		throw Error("Expected a double!");
	}
	
	return tmp;
}

// Read an integer from stdin and throw an error if it fails
int Controller::get_int()
{
	int tmp;
	if(!(cin >> tmp))
	{
		throw Error("Expected an integer!");
	}
	
	return tmp;
}

