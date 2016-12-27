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

/* 
This skeleton file shows the required public and protected interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You should delete this comment.
*/

class Model {
public:
	// create the initial objects, output constructor message
	Model();
	
	// destroy all objects, output destructor message
	~Model();

	// return the current time
	int get_time() {return time;}

	// is name already in use for either ship or island?
	bool is_name_in_use(const std::string& name) const;

	// is there such an island?
	bool is_island_present(const std::string& name) const;
	// add a new island to the lists
	void add_island(Island *);
	// will throw Error("Island not found!") if no island of that name
	Island * get_island_ptr(const std::string& name) const;

	// is there such an ship?
	bool is_ship_present(const std::string& name) const;
	// add a new ship to the list, and update the view
	void add_ship(Ship *);
	// will throw Error("Ship not found!") if no ship of that name
	Ship * get_ship_ptr(const std::string& name) const;
	// update the view, remove the ship and then delete it
	void remove_ship(Ship *);
	
	// tell all objects to describe themselves
	void describe() const;
	// increment the time, and tell all objects to update themselves
	// then update the view.
	void update();	
	
	/* View services */
	// Attaching a View causes it to be updated with all current objects'location.
	void attach(View *);
	// Detach the View by discarding the pointer - no updates sent to it thereafter.
	// If there were multiple Views, the pointer would be supplied to identify the Views.
	void detach();
	
private:
	int time;		// the simulated time


	// no copy or assignment allowed
	Model(const Model&);
	Model& operator= (const Model&);
};

