#ifndef COMPONENT_H
#define COMPONENT_H

#include <tr1/memory>

class Point;

// The component class implements an interface for grouping objects.  Objects
// that inherit from the Component class are groupable and they can be added
// and removed from the group.  There are a set of operations that can be
// performed on all of the objects in a group.
class Component
{
public:	
	virtual ~Component() { }
    
    // Operations that can be performed on the group.
	virtual void set_destination_position_and_speed(Point destination_position, double speed) = 0;	
	virtual void set_course_and_speed(double course, double speed) = 0;
	virtual void stop() = 0;
	    
	// Operations that manage the group (fat interface functions)
	// Add objects to the group
	virtual void add(std::tr1::shared_ptr<Component>);
	// Remove objects from the group
	virtual void remove(std::tr1::shared_ptr<Component>);
	// Return a pointer to the group object given it's name
    virtual std::tr1::shared_ptr<Component> get_child_by_name(const std::string& name);
    
    virtual std::string get_name() const = 0;
};

#endif

