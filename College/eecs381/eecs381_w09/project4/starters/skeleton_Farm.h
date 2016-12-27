/*
A Farm is a Structure that when updated, increments the amount of food on hand
by the production rate amount.
Food can be withdrawn, but no provision is made for depositing any.
*/

/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/

public:
	Farm (const std::string& in_name, Point in_location);
	~Farm();
		
	// returns the specified amount, or the remaining amount, whichever is less,
	// and deducts that amount from the amount on hand
	virtual double withdraw(double amount_to_get);

	//	update adds the production amount to the stored amount
	virtual void update();

	// output information about the current state
	virtual void describe() const;
	
