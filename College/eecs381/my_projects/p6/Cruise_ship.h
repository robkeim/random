#ifndef CRUISE_SHIP_H
#define CRUISE_SHIP_H

#include <string>
#include <list>
#include <tr1/memory>

#include "Model.h"
#include "Ship.h"

// The cruise ship class behaves like a Ship, until it's destination position
// and speed are set in which case it starts on a cruise.  A cruise consists
// of touring all of the islands and returning to the original Island at the
// end.  At each Island, there is a stop for refueling and sightseeing, and
// then the next Island is determined by the closest unvisited unlike, where
// ties are broken alphabetically.
class Cruise_ship : public Ship
{
public:
	Cruise_ship(const std::string& name_, Point position_);
		
	virtual void set_destination_position_and_speed(Point destination_position, double speed);
	bool on_cruise() const;
		
	virtual void update();
	virtual void describe() const;

private:
	enum Cruise_ship_state_e
	{
		NOT_ON_CRUISE,
		ON_CRUISE,
		STOPPED_REFUEL,
		STOPPED_SIGHTSEE,
		STOPPED_DEPARTURE,
		FINAL_DESTINATION
	} cruise_ship_state;

	Model &model;
	
	// A list of islands that still need to be visited on the cruise
	typedef std::list<std::tr1::shared_ptr<Island> > Island_list_t;
	typedef Island_list_t::iterator Island_list_it_t;
	Island_list_t island_list;
	
	// The current destination and final destination for the cruise
	std::tr1::shared_ptr<Island> cur_destination;
	std::tr1::shared_ptr<Island> final_destination;
	
	double cruise_speed;
		
	// After reaching a destination, this returns the next destination for the
	// cruise
	std::tr1::shared_ptr<Island> get_next_destination();

	// no copy, assignment
	Cruise_ship(const Cruise_ship&);
	Cruise_ship& operator= (const Cruise_ship&);
};

#endif
