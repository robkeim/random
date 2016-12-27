#ifndef CONTROLLER_H
#define CONTROLLER_H

#include <list>
#include <map>
#include <string>
#include <tr1/memory>

// Controller
// This class is responsible for controlling the Model and View according to 
// interactions with the user.

class Bridge_View;
class Component;
class Island;
class Map_View;
class Model;
class Ship;
class View;

class Controller 
{
public:	
	Controller();

	// Run the program by acccepting user commands
	void run();
	
private:
	// Map command strings to members functions that perform an action on a group
    typedef void (Controller::*Group_ptr_t)(std::tr1::shared_ptr<Component>);
    typedef std::map<std::string, Group_ptr_t> Group_commands_t;
   
    void add_group_commands(Group_commands_t &group_commands);

    // Group commands
    void group_cmd_add(std::tr1::shared_ptr<Component> component);
    void group_cmd_course(std::tr1::shared_ptr<Component> component);
    void group_cmd_position(std::tr1::shared_ptr<Component> component);
    void group_cmd_remove(std::tr1::shared_ptr<Component> component);
    void group_cmd_stop(std::tr1::shared_ptr<Component> component);

    // Map command strings to member functions that perform an action to a ship
	typedef void (Controller::*Ship_ptr_t)(std::tr1::shared_ptr<Ship>);
	typedef std::map<std::string, Ship_ptr_t> Ship_commands_t;
	
	void add_ship_commands(Ship_commands_t &ship_commands);
	
	// Ship commands
	void ship_cmd_course(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_position(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_destination(std::tr1::shared_ptr<Ship> ship);
    void ship_cmd_fuel(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_load_at(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_unload_at(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_dock_at(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_attack(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_refuel(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_stop(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_stop_attack(std::tr1::shared_ptr<Ship> ship);

	// Map command strings to member functions
	typedef void (Controller::*Ptr_t)();
	typedef std::map<std::string, Ptr_t> Commands_t;

	void add_commands(Commands_t &commands);

	// Map_View commands
	void cmd_open_map_view();
	void cmd_close_map_view();
	void cmd_default();
	void cmd_size();
	void cmd_zoom();
	void cmd_pan();
	void cmd_show();
	void cmd_open_bridge_view();
	void cmd_close_bridge_view();
	
	// Model commands
	void cmd_status();
	void cmd_go();
	void cmd_create();
    void cmd_group();

	std::tr1::shared_ptr<Island> get_island_ptr();

	Model &model;
	
	typedef std::tr1::shared_ptr<Map_View> Map_view_t;
	Map_view_t map_view;
	
	typedef std::map<std::string, std::tr1::shared_ptr<Bridge_View> > Bridge_view_t;
	typedef Bridge_view_t::iterator Bridge_view_it_t;
	Bridge_view_t bridge_views;
	
	typedef std::list<std::tr1::shared_ptr<View> > Views_t;
	Views_t views;
	
	void clear_stream();
	double get_double();
	int get_int();
};

#endif
