#ifndef CRUISE_SHIP_H
#define CRUISE_SHIP_H

#include <string>
#include <list>
#include <tr1/memory>

#include "Model.h"
#include "Ship.h"

class Cruise_ship : public Ship
{
	public:
		Cruise_ship(const std::string& name_, Point position_);
		
		virtual void set_destination_position_and_speed(Point destination_position, double speed);
		bool on_cruise();
		
		virtual void update();
		virtual void describe() const;

	private:
		enum cruise_ship_state_e
		{
			NOT_ON_CRUISE,
			ON_CRUISE,
			STOPPED_REFUEL,
			STOPPED_SIGHTSEE,
			STOPPED_DEPARTURE,
			FINAL_DESTINATION
		} cruise_ship_state;

		Model &model;
		std::list<std::tr1::shared_ptr<Island> > island_list;
		std::tr1::shared_ptr<Island> cur_destination;
		std::tr1::shared_ptr<Island> final_destination;
		double cruise_speed;
		
		std::tr1::shared_ptr<Island> get_next_destination();

		// no copy, assignment
		Cruise_ship(const Cruise_ship&);
		Cruise_ship& operator= (const Cruise_ship&);
};

#endif
