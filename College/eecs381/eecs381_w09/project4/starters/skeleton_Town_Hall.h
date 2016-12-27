/* 
A Town_Hall is a structure that provides for depositing and withdrawing food,
but does no updating.
*/

/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/


public:
	Town_Hall (const std::string& in_name, Point in_location);
	
	~Town_Hall();
	
	// deposit adds in the supplied amount
	virtual void deposit(double deposit_amount);

	// returns the specified amount, or the remaining amount, whichever is less,
	// and deducts that amount from the amount on hand
	virtual double withdraw(double amount_to_obtain);

	// output information about the current state
	virtual void describe() const;
