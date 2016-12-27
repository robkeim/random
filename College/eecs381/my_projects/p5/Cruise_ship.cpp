#include "Cruise_ship.h"

#include <iostream>

#include "Island.h"

using std::cout;
using std::endl;
using std::list;
using std::string;
using std::tr1::shared_ptr;

Cruise_ship::Cruise_ship(const string &name_, Point position_)
	: Ship(name_, position_, 500, 15., 2, 0), cruise_ship_state(NOT_ON_CRUISE), model(Model::get_Instance())
{ }

void Cruise_ship::set_destination_position_and_speed(Point destination_position, double speed)
{
	if (on_cruise())
	{
		cout << get_name() << " canceling current cruise" << endl;
		cruise_ship_state = NOT_ON_CRUISE;
	}
	
	Ship::set_destination_position_and_speed(destination_position, speed);
	
	island_list = model.get_islands();

	list<shared_ptr<Island> >::iterator it = island_list.begin();
	while ((*it)->get_location() != destination_position)
	{
		++it;
	}
	
	cur_destination = final_destination = *it;
	cruise_speed = speed;
	
	cout << get_name() << " will visit " << cur_destination->get_name() << endl;
	cout << get_name() << " cruise will start and end at " << cur_destination->get_name() << endl;
	
	island_list.erase(it);
	
	cruise_ship_state = ON_CRUISE;
	
	return;
}

bool Cruise_ship::on_cruise()
{
	return cruise_ship_state != NOT_ON_CRUISE;
}

void Cruise_ship::update()
{	
	Ship::update();

	switch(cruise_ship_state)
	{
		case ON_CRUISE:
			if (can_dock(cur_destination))
			{
				dock(cur_destination);
				cruise_ship_state = STOPPED_REFUEL;
			}
			break;
		case STOPPED_REFUEL:
			refuel();
			cruise_ship_state = STOPPED_SIGHTSEE;
			break;
		case STOPPED_SIGHTSEE:		
			cruise_ship_state = STOPPED_DEPARTURE;
			break;
		case STOPPED_DEPARTURE:
			if (!island_list.size())
			{
				Ship::set_destination_position_and_speed(final_destination->get_location(), cruise_speed);
					cout << get_name() << " will visit " << final_destination->get_name() << endl;
				cruise_ship_state = FINAL_DESTINATION;
				break;
			}
			
			cur_destination = get_next_destination();
			Ship::set_destination_position_and_speed(cur_destination->get_location(), cruise_speed);
				cout << get_name() << " will visit " << cur_destination->get_name() << endl;
			cruise_ship_state = ON_CRUISE;
			break;
		case FINAL_DESTINATION:
			if (can_dock(final_destination))
			{
				dock(final_destination);
				
				cout << get_name() << " cruise is over at " << final_destination->get_name() << endl;
				
				cruise_ship_state = NOT_ON_CRUISE;
			}
			break;
		default:
			// Do nothing
			break;
	}
	
	return;
}

shared_ptr<Island> Cruise_ship::get_next_destination()
{
	list<shared_ptr<Island> >::iterator it = island_list.begin();
	Point cur_location = get_location();
	
	shared_ptr<Island> next_destination = *it;
	double min_distance = cartesian_distance(cur_location, (*it)->get_location());
	
	for (++it; it != island_list.end(); ++it)
	{
		double tmp_distance = cartesian_distance(cur_location, (*it)->get_location());
		
		if (tmp_distance < min_distance)
		{
			min_distance = tmp_distance;
			next_destination = *it;
		}
	}
	
	island_list.remove(next_destination);
	
	return next_destination;
}

void Cruise_ship::describe() const
{
	cout << "\nCruise_ship ";
	
	Ship::describe();
	
	switch (cruise_ship_state)
	{
		case ON_CRUISE:
				cout << "On cruise to " << cur_destination->get_name() << endl;
			break;
		case FINAL_DESTINATION:
				cout << "On cruise to " << final_destination->get_name() << endl;
				break;
		case STOPPED_REFUEL:
		case STOPPED_SIGHTSEE:
		case STOPPED_DEPARTURE:
			cout << "Waiting during cruise at " << cur_destination->get_name() << endl;
		default:
			// Do nothing
			break;
	}
	
	return;
}
		
