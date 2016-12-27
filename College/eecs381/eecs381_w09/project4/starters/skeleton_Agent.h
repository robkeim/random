/* 
Agents are a kind of Sim_object, and privately inherit from Moving_object.
Agents can be commanded to move to a destination. Agents have a health, which
is decreased when they take a hit. If the Agent's health > 0, it is alive.
If its heath <= 0, it starts dying, then on subsequent updates, 
it becomes dead, and finally disappearing.
*/

/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/


class Structure;

// *** declare as inheriting from Sim_object and Moving_object, as specified

public:
	// *** create with initial health is 5, speed is 5, state is Alive
	Agent(const std::string& in_name, Point in_location);
	
	// *** declare and define destructor appropriately

 	// *** Make this an abstract class by making the destructor pure virtual
		
	// *** provide the definition of the following reader functions here in the class declaration
	// return true if this agent is Alive or Disappearing
	bool is_alive() const
	bool is_disappearing() const
	
	// return this Agent's location
	virtual Point get_location() const;

	// return true if this Agent is in motion
	bool is_moving() const;
	
	// tell this Agent to start moving to location destination_
	virtual void move_to(Point destination_);

	// tell this Agent to stop its activity
	virtual void stop();

	// Tell this Agent to accept a hit from an attack of a specified strength
	// The attacking Agent identifies itself with its this pointer.
	// A derived class can override this function.
	// The function lose_health is called to handle the effect of the attack.
	virtual void take_hit(int attack_strength, Agent *attacker_ptr);
	
	// update the moving state and Agent state of this object.
	virtual void update();
	
	// output information about the current state
	virtual void describe() const;

	/* Fat Interface for derived classes */
	// Throws exception that an Agent cannot work.
	virtual void start_working(Structure *, Structure *);

	// Throws exception that an Agent cannot attack.
	virtual void start_attacking(Agent *);

protected:
	// calculate loss of health due to hit.
	// if health decreases to zero or negative, Agent state becomes Dying, and any movement is stopped.
	void lose_health(int attack_strength);
