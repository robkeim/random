#include "Component.h"

#include "Utility.h"

using std::tr1::shared_ptr;
using std::string;

// Add objects to the group
void Component::add(shared_ptr<Component>)
{
	throw Error("Cannot add to group!");
}

// Remove objects from the group	
void Component::remove(shared_ptr<Component>)
{
	throw Error("Cannot remove from group!");
}

// Return a pointer to the group object given it's name
shared_ptr<Component> Component::get_child_by_name(const string&)
{
    throw Error("Component has no child!");
}

