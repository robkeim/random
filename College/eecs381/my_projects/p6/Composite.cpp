#include "Composite.h"

#include "Geometry.h"
#include "Ship.h"
#include "Utility.h"

using std::cout;
using std::string;
using std::tr1::shared_ptr;

Composite::Composite(const string& name_)
    : name(name_)
{ }

void Composite::set_destination_position_and_speed(Point destination_position, double speed)
{
	// Remove the bad pointers so that when group is iterated through, we 
	// know that all the pointers will be valid when we iterate through the
	// group and apply the operation
	remove_invalid_pointers();
    for (Group_it_t it = group.begin(); it != group.end(); ++it)
	{
        shared_ptr<Component> ptr = (it->second).lock();
		try
		{
			ptr->set_destination_position_and_speed(destination_position, speed);
		}
		catch (Error&)
		{
			cout << ptr->get_name() << " cannot move to that destination with that speed\n";
		}
	}
	
	return;
}

void Composite::set_course_and_speed(double course, double speed)
{
    remove_invalid_pointers();
    for (Group_it_t it = group.begin(); it != group.end(); ++it)
	{
        shared_ptr<Component> ptr = (it->second).lock();
        try
		{
			ptr->set_course_and_speed(course, speed);
		}
		catch (Error&)
		{
			cout << ptr->get_name() << " cannot move on that course at that speed\n";
		}
	}

	return;
}

void Composite::stop()
{
	remove_invalid_pointers();
    for (Group_it_t it = group.begin(); it != group.end(); ++it)
	{
        shared_ptr<Component> ptr = (it->second).lock();
		try
		{
			ptr->stop();
		}
		catch (Error&)
		{
			cout << ptr->get_name() << " cannot stop\n";
		}
	}

	return;
}

// Add object to the group
void Composite::add(shared_ptr<Component> component)
{
	string name = component->get_name();
    
    if (group.find(name) != group.end())
	{
		throw Error("Object already in group!");
	}

	group[name] = component;

	return;
}

// Remove object from the group
void Composite::remove(shared_ptr<Component> component)
{
	string name = component->get_name();
   
    Group_it_t it = group.find(name); 
    if (it == group.end())
	{
		throw Error("Object not in group!");
	}

	group.erase(it);

	return;
}

// Remove any deleted objects from the container to ensure that all of
// the pointers are valid when group operations are performed on them
void Composite::remove_invalid_pointers()
{
	// Increment through the data structure inside the loop so that the
	// iterators can be erased in place
	for (Group_it_t it = group.begin(); it != group.end(); )
	{
        shared_ptr<Component> ptr = (it->second).lock();
        if (!ptr)
        {
            group.erase(it++);
        }
        else
        {
            ++it;
        }    
	}
    
    return;
}

// Return a pointer to the group object given it's name
shared_ptr<Component> Composite::get_child_by_name(const string& name)
{
    remove_invalid_pointers();
    Group_it_t it = group.find(name);

    if (it == group.end())
    {
        throw Error("Object does not have a child with the given name");
    }

    return (it->second).lock(); 
}

// Return the name of the group
string Composite::get_name() const
{
    return name;
}

