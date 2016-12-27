/* Collections contain a name and a container of members,
represented as pointers to Records.
Collection objects manage their own Record container. 
The container of Records is not available to clients.

*/

/* *** NOTE: If after a function header is a comment "fill this in" remove the comment and replace
it with the proper code here in the header file.  All other functions should be defined
in the .cpp file. 
Comments starting with "***" are instructions to you - remove them from your finished code.
Remove this comment too. */

public:
	// Construct a collection with the specified name and no members
	Collection(const String& name_)
	/*fill this in*/
	
	// Construct a Collection from an input file stream in save format, using the record list,
	// restoring all the Record information. 
	// Record list is needed to resolve references to record members.
	// No check made for whether the Collection already exists or not.
	// Throw Error exception if invalid data discovered in file.
	// Where appropriate, input is read directly into member variables.
	 Collection(std::ifstream& is, const Ordered_list<Record *>& library);
	
	// Accessors
	String get_name() const
		{return name;}
		
	// Add the Record, throw exception if there is already a Record with the same title.
	void add_member(Record * record_ptr);
	// Return true if there are no members; false otherwise
	bool empty() const
		{/*fill this in*/}
	// Return true if the record is present, false if not.
	bool is_member_present(Record * record_ptr) const;
	// Remove the specified Record, throw exception if the record was not found.
	void remove_member(Record * record_ptr);
	// discard all members
	void clear()
		{/*fill this in*/}

	// Write a Collections's data to a stream in save format, with endl as specified.
	void save(std::ostream& os) const;

	// This operator defines the order relation between Collections, based just on the name
	bool operator< (const Collection& rhs) const
		{/*fill this in*/}
	
	/* *** fill in a friend declaration for the output operator */
		
private:
	/* *** the member information must be kept in a container of Record * - name is your choice */
	Ordered_list<Record *> 

	/* *** other private members are your choice */
};

// Print the Collection data
std::ostream& operator<< (std::ostream& os, const Collection& Collection);
