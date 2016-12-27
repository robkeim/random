/*
Model is part of a simplified Model-View-Controller pattern.
Model keeps track of the Sim_objects in our little world. It is the only
component that knows how many Structures and Agents there are, but it does not
know about any of their derived classes, nor which Agents are of what kind of Agent. 
It has facilities for looking up objects by name, and removing Agents.  When
created, it creates an initial group of Structures and Agents using the Structure_factory
and Agent_factory.
Finally, it keeps the system's time.

Controller tells Model what to do; Model in turn tells the objects what do, and
tells View whenever anything changes that might affect the View, and provides
facilities for looking up objects given their name.

Notice how only the Standard Library headers need to be included - reduced coupling!

*/

/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/

class Model {
public:
	// create the initial objects
	Model();
	
	// destroy all objects
	~Model();

	// return the current time
	int get_time() {return time;}

	// is name already in use for either agent or structure?
	bool is_name_in_use(const std::string& name) const;

	// is there a structure with this name?
	bool is_structure_present(const std::string& name) const;
	// add a new structure; assumes none with the same name
	void add_structure(Structure *);
	// will throw Error("Structure not found!") if no structure of that name
	Structure * get_structure_ptr(const std::string& name) const;

	// is there an agent with this name?
	bool is_agent_present(const std::string& name) const;
	// add a new agent; assumes none with the same name
	void add_agent(Agent *);
	// will throw Error("Agent not found!") if no agent of that name
	Agent * get_agent_ptr(const std::string& name) const;
	// removes the agent and then deletes it; no error if not found
	void remove_agent(Agent *);
	
	// tell all objects to describe themselves to the console
	void describe() const;

	// update the objects and the view
	void update();	
	
	/* View services */
	// Attaching a View causes it to be updated with all current objects'location.
	void attach(View *);
	// Detach the View by discarding the pointer - no updates sent to it thereafter.
	// If there were multiple Views, the pointer would be supplied to identify the Views.
	void detach();
	
private:
	// *** declare an int member variable to store the simulated time



	// no copy or assignment allowed
	Model(const Model&);
	Model& operator= (const Model&);
};