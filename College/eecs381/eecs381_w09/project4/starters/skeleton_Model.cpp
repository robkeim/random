
/* *** create the initial structures and agents using the following code in the Model constructor.
Do not change the order of these statements. You should delete this comment. */

	add_structure(create_structure("Rivendale", "Farm", Point(10., 10.)));
	add_structure(create_structure("Sunnybrook", "Farm", Point(0., 30.)));
	add_structure(create_structure("Shire", "Town_Hall", Point(20., 20.)));
	add_structure(create_structure("Paduca", "Town_Hall", Point(30., 30.)));
	
	add_agent(create_agent("Pippin", "Peasant", Point(5., 10.)));
	add_agent(create_agent("Merry", "Peasant", Point(0., 25.)));
	add_agent(create_agent("Zug", "Soldier", Point(20., 30.)));
	add_agent(create_agent("Bug", "Soldier", Point(15., 20.)));

