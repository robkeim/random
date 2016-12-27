/*
A Soldier is an Agent that has attack and defense behaviors. It can be commanded
to start attacking another Agent and will continue the attack as long as 
it is alive and the target is alive and in range. If attacked, the Soldier will
start attacking its attacker.
*/
	
/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/

public:
	
	// *** define as specified
	Soldier(const std::string& in_name, Point in_location);
	~Soldier();
		
	// update implements Soldier behavior
	virtual void update();
	
	// Make this Soldier start attacking the target Agent.
	// Throws an exception if the target is the same as this Agent,
	// is out of range, or is not alive.
	virtual void start_attacking(Agent * target_ptr);
	
	// Overrides Agent's take_hit to counterattack when attacked.
	virtual void take_hit(int attack_strength, Agent * attacker_ptr);

	// output information about the current state
	virtual void describe() const;
