#ifndef MODEL_H
#define MODEL_H

#include <list>
#include <map>
#include <string>
#include <tr1/memory>

// Model is part of a simplified Model-View-Controller pattern.
// Model keeps track of the Sim_objects in our little world. It is the only
// component that knows how many Islands and Ships there are, but it does not
// know about any of their derived classes, nor which Ships are of what kind of Ship. 
// It had facilities for looking up objects by name, and removing Ships.  When
// created, it creates an initial group of Islands and Ships using the Ship_factory.
// Model keeps track of the groups and can add or remove groups
// Finally, it keeps the system's time.

// Controller tells Model what to do; Model in turn tells the objects what do, and
// tells View whenever anything changes that might affect the View, and provides
// facilities for looking up objects given their name.

class Component;
class Island;
class Ship;
class Sim_object;
class View;

class Model 
{
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
	// add a new ship to the list, and update the views
	void add_ship(std::tr1::shared_ptr<Ship>);
	// will throw Error("Ship not found!") if no ship of that name
	std::tr1::shared_ptr<Ship> get_ship_ptr(const std::string& name) const;
	// update the views, remove the ship and then delete it
	void remove_ship(std::tr1::shared_ptr<Ship>);

	// is there such a group?
    bool is_group_present(const std::string& name) const;
    // add a new group to the list
    void add_group(std::tr1::shared_ptr<Component> component);
    // return a pointer to the group with the given name
    std::tr1::shared_ptr<Component> get_group_ptr(const std::string& name) const;
    // remove the group
    void remove_group(std::tr1::shared_ptr<Component> component);

	// tell all objects to describe themselves
	void describe() const;
	// increment the time, and tell all objects to update themselves
	// then update the views.
	void update();	
	
	// Remove all of the objects the model currently knows about
	void clear_objects();
	
	/* View services */
	// Attaching a View causes it to be updated with all current objects'location.
	void attach(std::tr1::shared_ptr<View>);
	// Detach the View by discarding the pointer - no updates sent to it thereafter.
	void detach(std::tr1::shared_ptr<View>);
	
private:
	int time;		// the simulated time
	
	typedef std::map<std::string, std::tr1::shared_ptr<Island> > Islands_t;
	typedef Islands_t::iterator Islands_it_t;
	Islands_t islands;
	
	typedef std::map<std::string, std::tr1::shared_ptr<Ship> > Ships_t;
	typedef Ships_t::iterator Ships_it_t;
	Ships_t ships;
	
	typedef std::map<std::string, std::tr1::shared_ptr<Sim_object > > Objects_t;
	typedef Objects_t::iterator Objects_it_t;
	typedef Objects_t::const_iterator Objects_const_it_t;
	Objects_t objects;
	
	typedef std::list<std::tr1::shared_ptr<View> > Views_t;
	typedef Views_t::iterator Views_it_t;
	Views_t views;

    typedef std::map<std::string, std::tr1::shared_ptr<Component> > Groups_t;
    Groups_t groups;

	static Model * ptr;

	// Since Model is a Singleton prevent accidental construction, 
	// destruction, and copying of Model
	Model();
	~Model() { };
	Model(const Model&);
	Model& operator= (const Model&);
};

#endif

