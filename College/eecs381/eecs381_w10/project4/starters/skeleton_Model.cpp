
/* create some islands and ships using the following code in the Model constructor.
Do not change the order of these statements. You should delete this comment. */

	add_island(new Island("Exxon", Point(10, 10), 1000, 200));
	add_island(new Island("Shell", Point(0, 30), 1000, 200));
	add_island(new Island("Bermuda", Point(20, 20)));
	
	add_ship(create_ship("Ajax", "Cruiser", Point (15, 15)));
	add_ship(create_ship("Xerxes", "Cruiser", Point (25, 25)));
	add_ship(create_ship("Valdez", "Tanker", Point (30, 30)));
	cout << "Model constructed" << endl;

