/* The Sim_object class provides the interface for all of simulation objects. 
It also stores the object's name, and has pure virtual describe, update, 
and location accessor functions. */

/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/

class Sim_object {
public:
	Sim_object(const std::string& in_name);
	// *** supply the appropriate destructor declaration
	
	std::string get_name() const
		{return name;}
			
	// *** declare the following to be pure virtual functions
	Point get_location() const
	void describe() const
	void update()

private:
	std::string name;
};

