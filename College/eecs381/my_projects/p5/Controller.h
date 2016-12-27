#ifndef CONTROLLER_H
#define CONTROLLER_H

/* Controller
This class is responsible for controlling the Model and View according to interactions
with the user.
*/

#include <map>
#include <string>
#include <tr1/memory>

class Bridge_View;
class Map_View;
class Model;
class Ship;

class Controller {
public:	
	Controller();

	// Run the program by acccepting user commands
	void run();
	
private:
	typedef void (Controller::*ship_ptr_t)(std::tr1::shared_ptr<Ship>);
	typedef std::map<std::string, ship_ptr_t> ship_commands_t;

	typedef void (Controller::*ptr_t)();
	typedef std::map<std::string, ptr_t> commands_t;
	
	void add_ship_commands(ship_commands_t &ship_commands);
	
	// Ship commands
	void ship_cmd_course(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_position(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_destination(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_load_at(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_unload_at(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_dock_at(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_attack(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_refuel(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_stop(std::tr1::shared_ptr<Ship> ship);
	void ship_cmd_stop_attack(std::tr1::shared_ptr<Ship> ship);
	
	void add_commands(commands_t &commands);

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

	Model &model;
	
	std::tr1::shared_ptr<Map_View> map_view;
	std::map<std::string, std::tr1::shared_ptr<Bridge_View> > bridge_views;
	
	void clear_stream();
	double get_double();
	int get_int();
};

#endif
