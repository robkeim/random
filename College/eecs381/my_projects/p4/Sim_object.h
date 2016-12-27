#ifndef SIM_OBJECT_H
#define SIM_OBJECT_H
/* This class provides the interface for all of simulation objects. It also stores the
object's name, and has pure virtual accessor functions for the object's position
and other information.  */

/* *** You may not add any additional classes, structs, functions etc to this file. */
#include <string>
#include <iostream>	// demonstration only
#include "Geometry.h"

class Sim_object {
public:
	Sim_object(const std::string& name_) : 
		name(name_)
		{std::cout << "Sim_object " << name << " constructed" << std::endl;}

	// *** declare the destructor appropriately and use the following function body
	virtual ~Sim_object()
		{std::cout << "Sim_object " << name << " destructed" << std::endl;}
	
	std::string get_name() const
		{return name;}
		
	/*** Interface for derived classes ***/
	virtual Point get_location() const = 0;
	virtual void describe() const = 0;
	virtual void update() = 0;
	
private:
	std::string name;
};


#endif
