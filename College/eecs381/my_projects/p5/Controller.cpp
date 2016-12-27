#include "Controller.h"

#include <iostream>

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
using std::tr1::shared_ptr;

Controller::Controller() :
	model(Model::get_Instance())
{ }

// Run the program by acccepting user commands
void Controller::run()
{
	ship_commands_t ship_commands;
	commands_t commands;
	
	add_ship_commands(ship_commands);
	add_commands(commands);
	
	ship_ptr_t ship_fn_ptr;
	ptr_t fn_ptr;

	cout << "\nTime " << model.get_time() << ": Enter command: ";
	
	string command;
	cin >> command;
	
	while (command != "quit")
	{
		try
		{
			if (model.is_ship_present(command))
			{
				shared_ptr<Ship> ship = model.get_ship_ptr(command);
				cin >> command;
				
				ship_fn_ptr = ship_commands[command];
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
				fn_ptr = commands[command];
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
			cout << error.msg << endl;
			clear_stream();
		}
		
		cout << "\nTime " << model.get_time() << ": Enter command: ";
		
		cin >> command;
	}

	cout << "Done" << endl;
	
	model.clear_objects();
	
	return;
}

void Controller::add_ship_commands(ship_commands_t &ship_commands)
{
	ship_commands["course"] = &Controller::ship_cmd_course;
	ship_commands["position"] = &Controller::ship_cmd_position;
	ship_commands["destination"] = &Controller::ship_cmd_destination;
	ship_commands["load_at"] = &Controller::ship_cmd_load_at;
	ship_commands["unload_at"] = &Controller::ship_cmd_unload_at;
	ship_commands["dock_at"] = &Controller::ship_cmd_dock_at;
	ship_commands["attack"] = &Controller::ship_cmd_attack;
	ship_commands["refuel"] = &Controller::ship_cmd_refuel;
	ship_commands["stop"] = &Controller::ship_cmd_stop;
	ship_commands["stop_attack"] = &Controller::ship_cmd_stop_attack;
	
	return;
}

void Controller::ship_cmd_course(shared_ptr<Ship> ship)
{
	double course = get_double();
	double speed = get_double();
	
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

void Controller::ship_cmd_position(shared_ptr<Ship> ship)
{
	double x = get_double();
	double y = get_double();
	double speed = get_double();
	
	if (speed < 0.0)
	{
		throw Error("Negative speed entered!");
	}
	
	ship->set_destination_position_and_speed(Point(x, y), speed);	
	
	return;
}

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

void Controller::ship_cmd_load_at(shared_ptr<Ship> ship)
{
	string island_name;
	cin >> island_name;
	
	shared_ptr<Island> island = model.get_island_ptr(island_name);
	
	ship->set_load_destination(island);
	
	return;
}

void Controller::ship_cmd_unload_at(shared_ptr<Ship> ship)
{
	string island_name;
	cin >> island_name;
	
	shared_ptr<Island> island = model.get_island_ptr(island_name);
	
	ship->set_unload_destination(island);
	
	return;
}

void Controller::ship_cmd_dock_at(shared_ptr<Ship> ship)
{
	string island_name;
	cin >> island_name;
	
	shared_ptr<Island> island = model.get_island_ptr(island_name);
	
	ship->dock(island);
	
	return;
}

void Controller::ship_cmd_attack(shared_ptr<Ship> ship)
{
	string ship_name;
	cin >> ship_name;
	
	shared_ptr<Ship> target_ship = model.get_ship_ptr(ship_name);
	
	ship->attack(target_ship);
	
	return;
}

void Controller::ship_cmd_refuel(shared_ptr<Ship> ship)
{
	ship->refuel();
	
	return;
}

void Controller::ship_cmd_stop(shared_ptr<Ship> ship)
{
	ship->stop();
	
	return;
}

void Controller::ship_cmd_stop_attack(shared_ptr<Ship> ship)
{
	ship->stop_attack();
	
	return;
}

void Controller::add_commands(commands_t &commands)
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
	
	return;
}

void Controller::cmd_open_map_view()
{
	if (map_view)
	{
		throw Error("Map view is already open!");
	}
	
	map_view = shared_ptr<Map_View>(new Map_View());
	model.attach(map_view);
	
	return;
}

void Controller::cmd_close_map_view()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
	
	map_view.reset();
	
	return;
}

void Controller::cmd_default()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}

    map_view->set_defaults();

	return;
}

void Controller::cmd_size()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
	
	int size = get_int();
	
    map_view->set_size(size);

	return;
}

void Controller::cmd_zoom()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
	
	double scale = get_double();
	
    map_view->set_scale(scale);

	return;
}

void Controller::cmd_pan()
{
	if (!map_view)
	{
		throw Error("Map view is not open!");
	}
	
	double x = get_double();
	double y = get_double();
	
    map_view->set_origin(Point(x, y));

	return;
}

void Controller::cmd_show()
{
	model.draw();

	return;
}

void Controller::cmd_open_bridge_view()
{
	string name;
	cin >> name;

	if (bridge_views.find(name) != bridge_views.end())
	{
		throw Error("Bridge view is already open for that ship!");
	}
	
	shared_ptr<Ship> ship = model.get_ship_ptr(name);
	
	shared_ptr<Bridge_View> bridge_view(new Bridge_View(ship));
	bridge_views[name] = bridge_view;
	model.attach(bridge_view);
	
	return;
}

void Controller::cmd_close_bridge_view()
{
	string name;
	cin >> name;
	
	map<string, shared_ptr<Bridge_View> >::iterator it = bridge_views.find(name);
	
	if (it == bridge_views.end())
	{
		throw Error("Bridge view for that ship is not open!");
	}
	
	model.detach(it->second);
	bridge_views.erase(it);
	
	return;
}

void Controller::cmd_status()
{
	model.describe();
	
	return;
}

void Controller::cmd_go()
{
	model.update();
	
	return;
}

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
	
	double x = get_double();
	double y = get_double();
	
	shared_ptr<Ship> ship = create_ship(name, type, Point(x, y));
	
	model.add_ship(ship);
	
	return;
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
		cin.clear();
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
		cin.clear();
		throw Error("Expected an integer!");
	}
	
	return tmp;
}

