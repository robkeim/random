#ifndef CONTROLLER_H
#define CONTROLLER_H

/* Controller
This class is responsible for controlling the Model and View according to interactions
with the user.
*/

#include <map>
#include <string>

class Model;
class Ship;
class View;

class Controller {
public:	
	// output constructor message
	Controller(Model& model_);
	// output destructor message
	~Controller();

	// create View object, run the program by acccepting user commands, then destroy View object
	void run();
	
private:
	typedef void (Controller::*ship_ptr_t)(Ship*);
	typedef std::map<std::string, ship_ptr_t> ship_commands_t;

	typedef void (Controller::*ptr_t)();
	typedef std::map<std::string, ptr_t> commands_t;
	
	void add_ship_commands(ship_commands_t &ship_commands);
	
	// Ship commands
	void ship_cmd_course(Ship *ship);
	void ship_cmd_position(Ship *ship);
	void ship_cmd_destination(Ship *ship);
	void ship_cmd_load_at(Ship *ship);
	void ship_cmd_unload_at(Ship *ship);
	void ship_cmd_dock_at(Ship *ship);
	void ship_cmd_attack(Ship *ship);
	void ship_cmd_refuel(Ship *ship);
	void ship_cmd_stop(Ship *ship);
	void ship_cmd_stop_attack(Ship *ship);
	
	void add_commands(commands_t &commands);

	// View commands
	void cmd_default();
	void cmd_size();
	void cmd_zoom();
	void cmd_pan();
	void cmd_show();
	
	// Model commands
	void cmd_status();
	void cmd_go();
	void cmd_create();

	Model &model;
	View *view;
};

#endif
