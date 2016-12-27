/* 
A Peasant is an Agent that can move food between Structures. It can be commanded to
start_working, whereupon it moves to the source, picks up food, returns to destination,
deposits the food, returns to source.  If picks up zero food at the source, it waits there
and tries again on the next update. 
If commanded to move_to somewhere, it stops working, and goes there.
*/

/* 
*** This skeleton file shows the required public interface for the class, which you may not modify. 
If no protected members are shown, there must be none in your version. 
If any protected or private members are shown here, then your class must also have them and use them as intended.
You must delete this comment and all other comments that start with "***".
*/

public:
	// *** define these in .cpp; initialize with zero amount being carried
	Peasant(const std::string& in_name, Point in_location);

	~Peasant();	

	// implement Peasant behavior
	virtual void update();
	
	// overridden to suspend working behavior
	virtual void move_to(Point dest);
	
	// stop moving and working
	virtual void stop();

	// starts the working process
	// Throws an exception if the source is the same as the destination.
	virtual void start_working(Structure * source_, Structure * destination_);

	// output information about the current state
	virtual void describe() const;
