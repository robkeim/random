#ifndef COMPOSITE_H
#define COMPOSITE_H

#include <map>
#include <string>

#include "Component.h"

class Ship;

// The composite class manages the group in the composite design pattern. It
// manages a container of objects, and for each of the supported group
// operations it performs the group operation on each of the elements in its
// container.
class Composite : public Component
{
public:
	// Each group is created with a name
    Composite(const std::string& name_);
    virtual ~Composite() { }

	// Operations that can be performed on the group.  Each of the group
	// operations is applied to each of the elements in the group.  If an
	// error occurs when attempting to apply the function to a member of the
	// group, a message is displayed saying that the specific object does not
	// support that functionality.
	virtual void set_destination_position_and_speed(Point destination_position, double speed);
	virtual void set_course_and_speed(double course, double speed);
	virtual void stop();
	
	// Add objects to the group
	virtual void add(std::tr1::shared_ptr<Component> component);
	// Remove objects from the group	
	virtual void remove(std::tr1::shared_ptr<Component> component);
	// Return a pointer to the group object given it's name
	virtual std::tr1::shared_ptr<Component> get_child_by_name(const std::string& name);
	
	// Return the name of the group
    std::string get_name() const;

private:
	// Remove any deleted objects from the container to ensure that all of
	// the pointers are valid when group operations are performed on them
    void remove_invalid_pointers();

	typedef std::map<std::string, std::tr1::weak_ptr<Component> > Group_t;
	typedef Group_t::iterator Group_it_t;
	Group_t group;

    std::string name;
};

#endif

