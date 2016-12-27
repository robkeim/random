#ifndef MODEL_H
#define MODEL_H

/*
Model is part of a simplified Model-View-Controller pattern.
Model keeps track of the Sim_objects in our little world. It is the only
component that knows how many Islands and Ships there are, but it does not
know about any of their derived classes, nor which Ships are of what kind of Ship. 
It had facilities for looking up objects by name, and removing Ships.  When
created, it creates an initial group of Islands and Ships using the Ship_factory.
Finally, it keeps the system's time.

Controller tells Model what to do; Model in turn tells the objects what do, and
tells View whenever anything changes that might affect the View, and provides
facilities for looking up objects given their name.

*/

#include <list>
#include <map>
#include <string>
#include <tr1/memory>

class Island;
class Ship;
class Sim_object;
class View;

class Model {
public:
	static Model &get_Instance();

	// return the current time
	int get_time() {return time;}

	// is name already in use for either ship or island?
	bool is_name_in_use(const std::string& name) const;

	// is there such an island?
	bool is_island_present(const std::string& name) const;
	// add a new island to the lists
	void add_island(std::tr1::shared_ptr<Island>);
	// will throw Error("Island not found!") if no island of that name
	std::tr1::shared_ptr<Island> get_island_ptr(const std::string& name) const;
	// returns a list of the current island the model knows about
	std::list<std::tr1::shared_ptr<Island> > get_islands();

	// is there such an ship?
	bool is_ship_present(const std::string& name) const;
	// add a new ship to the list, and update the view
	void add_ship(std::tr1::shared_ptr<Ship>);
	// will throw Error("Ship not found!") if no ship of that name
	std::tr1::shared_ptr<Ship> get_ship_ptr(const std::string& name) const;
	// update the view, remove the ship and then delete it
	void remove_ship(std::tr1::shared_ptr<Ship>);
	
	// tell all objects to describe themselves
	void describe() const;
	// increment the time, and tell all objects to update themselves
	// then update the view.
	void update();	
	
	// Remove all of the objects the model currently knows about
	void clear_objects();
	// Tell each of the views to draw themselves
	void draw();
	
	/* View services */
	// Attaching a View causes it to be updated with all current objects'location.
	void attach(std::tr1::shared_ptr<View>);
	// Detach the View by discarding the pointer - no updates sent to it thereafter.
	// If there were multiple Views, the pointer would be supplied to identify the Views.
	void detach(std::tr1::shared_ptr<View>);
	
private:
	int time;		// the simulated time
	std::map<std::string, std::tr1::shared_ptr<Island> > islands;
	std::map<std::string, std::tr1::shared_ptr<Ship> > ships;
	std::map<std::string, std::tr1::shared_ptr<Sim_object> > objects;
	std::list<std::tr1::shared_ptr<View> > views;

	static Model * ptr;

	Model();
	Model(const Model&);
	Model& operator= (const Model&);
};

#endif

