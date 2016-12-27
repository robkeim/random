/* A Structure is a Sim_object with a location and interface to derived types */

/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/

public:
	// *** provide a constructor and destructor
	// *** first constructor parameter should be the name, specified as a string, the second should be a Point for the location.
	
	// *** Make this an abstract class by making the destructor pure virtual
		
	// *** declare and define here appropriately
	Point get_location() const

	// *** declare and define the following functions as specified
	void update()

	// output information about the current state
	void describe() const;
	
	double withdraw(double amount_to_get);
	void deposit(double amount_to_give);
