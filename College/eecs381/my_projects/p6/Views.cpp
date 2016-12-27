#include "Views.h"

#include <cmath>
#include <iomanip>
#include <iostream>
#include <list>
#include <vector>

#include "Navigation.h"
#include "Ship.h"
#include "Utility.h"

using std::cout;
using std::endl;
using std::list;
using std::map;
using std::setprecision;
using std::setw;
using std::string;
using std::vector;
using std::tr1::shared_ptr;

Bridge_View::Bridge_View(shared_ptr<Ship> ship) :
	ship_name(ship->get_name()), ownship(ship)
{ }

// Save the supplied name and location for future use in a draw() call
// If the name is already present,the new location replaces the previous one.
void Bridge_View::update_location(const string& name, Point location)
{
	objects[name] = location;
	
	return;
}
	
// Remove the name and its location; no error if the name is not present.
void Bridge_View::update_remove(const string& name)
{
	objects.erase(name);

	return;
}
	
// Discard the saved information - drawing will show only a empty pattern
void Bridge_View::clear()
{
	objects.clear();
	
	return;
}

// Draw the current view
void Bridge_View::draw()
{
	if (ownship->is_afloat())
	{
		cout << "Bridge view from " << ownship->get_name() << " position " << ownship->get_location() << " heading " << ownship->get_course();
		
		for (int i = 0; i < 2; i++)
		{
			cout << "\n     ";
			for (int j = 0; j < 19; j++)
			{
				cout << ". ";
			}	 
		}
		
		vector<string> positions(19, ". ");
		
		Point cur_point = ownship->get_location();
		
		// Code to calculate position of each object
		Objects_it_t it;
		for (it = objects.begin(); it != objects.end(); ++it)
		{
			double distance = cartesian_distance(cur_point, it->second);
		
			if (distance < 0.005 || distance > 20.0)
			{
				continue;
			}
			Compass_position position(cur_point, it->second);
			double bearing = position.bearing;
			bearing -= ownship->get_course();
			
			string name = it->first;
			name = name.substr(0, 2);
			
			int coord = get_subscripts(bearing);
			if (coord == -1)
			{
				continue;
			}
			if (positions[coord] != ". ")
			{
				positions[coord] = "**";
			}
			else
			{
				positions[coord] = name;
			}
		}
		
		cout << "\n     ";
		for (int i = 0; i < 19; i++)
		{
			cout << setw(2) << positions[i];
		}
	}
	else
	{
		cout << "Bridge view from " << ownship->get_name() << " sunk at " << ownship->get_location();
		
		for (int i = 0; i < 3; i++)
		{
			cout << "\n     ";
			for (int j = 0; j < 19; j++)
			{
				cout << "w-";
			}	 
		}		
	}
	
	cout << endl;
	for (int i = -90; i <= 90; i += 30)
	{
		cout << setw(6) << i;
	}	
	cout << "  " << endl;
	
	return;
}

// Return the subscript for the specified bearing
int Bridge_View::get_subscripts(double bearing)
{
	if (bearing < -180)
	{
		bearing += 360;
	}
	
	if (bearing > 180)
	{
		bearing -= 360;
	}
	
	if (bearing < -90 || bearing >= 100)
	{
		return -1;
	}
			
	return static_cast<int>(floor((bearing + 90.) / 10.));
}

// maximum number of rows and columns supported by map view
const int map_view_maxsize = 30;	

// default constructor sets the default size, scale, and origin
Map_View::Map_View() :
	size(25), scale(2.0), origin(-10, -10)
{ } 

// Save the supplied name and location for future use in a draw() call
// If the name is already present,the new location replaces the previous one.
void Map_View::update_location(const string& name, Point location)
{
	objects[name] = location;
	
	return;
}
	
// Remove the name and its location; no error if the name is not present.
void Map_View::update_remove(const string& name)
{
	objects.erase(name);

	return;
}
	
// Discard the saved information - drawing will show only a empty pattern
void Map_View::clear()
{
	objects.clear();
	
	return;
}

// prints out the current map
void Map_View::draw()
{
	int x, y;

	vector<vector<string> > grid(size, vector<string>(size, ". "));
	list<string> outside_grid;

	Objects_it_t it;
	for (it = objects.begin(); it != objects.end(); ++it)
	{
		if (!get_subscripts(y, x, it->second))
		{
			outside_grid.push_back(it->first);
		}
		else
		{		
			if (grid[x][y] != ". ")
			{
				grid[x][y] = "* ";
			}
			else
			{
				grid[x][y] = it->first;
			}
		}
	}
	
	cout << "Display size: " << size << ", scale: " << scale << ", origin: " << origin << endl;

	if (outside_grid.size())
	{
		list<string>::iterator it = outside_grid.begin();
		cout << *it;
		
		for (++it; it != outside_grid.end(); ++it)
		{
			cout << ", " << *it;
		}
		
		cout << " outside the map" << endl;
	}
	
	int old_precision = cout.precision();
	cout.precision(0);

    string ship_name;

	for (x = size - 1; x >= 0; x--)
	{
		if (!(x % 3))
		{
			cout << setw(4) << origin.y + scale * x;
			cout << " ";
		}
		else
		{
			cout << setw(5) << " ";
		}
		for (y = 0; y < size; y++)
		{
			ship_name = grid[x][y].substr(0, 2);
            if (ship_name.length() == 1)
            {
                ship_name += " ";
            }
            cout << setw(2) << ship_name;
		}
		cout << endl;
	}
	
	for (y = 0; y < size; y += 3)
	{
		cout << setw(6) << origin.x + scale * y;
	}
	
	cout << endl;
	
	cout.precision(old_precision);
	
	return;
}

/* *** Use this function to calculate the subscripts for the cell. */

/* *** This code assumes the specified private member variables. */

// Calculate the cell subscripts corresponding to the supplied location parameter, 
// using the current size, scale, and origin of the display. 
// This function assumes that origin is a  member variable of type Point, 
// scale is a double value, and size is an integer for the number of rows/columns 
// currently being used for the grid.
// Return true if the location is within the grid, false if not
bool Map_View::get_subscripts(int &ix, int &iy, Point location)
{
	// adjust with origin and scale
	Cartesian_vector subscripts = (location - origin) / scale;
	// truncate coordinates to integer after taking the floor
	// floor function will produce integer smaller than even for negative values, 
	// so - 0.05 => -1., which will be outside the array.
	ix = int(floor(subscripts.delta_x));
	iy = int(floor(subscripts.delta_y));
	// if out of range, return false
	if ((ix < 0) || (ix >= size) || (iy < 0) || (iy >= size)) {
		return false;
		}
	else
		return true;
}

// modify the display parameters
// if the size is out of bounds will throw Error("New map size is too big!")
// or Error("New map size is too small!")
void Map_View::set_size(int size_)
{
	if (size_ <= 6)
	{
		throw Error("New map size is too small!");
	}
	
	if (size_ > 30)
	{
		throw Error("New map size is too big!");
	}

	size = size_;

	return;
}
	
// If scale is not postive, will throw Error("New map scale must be positive!");
void Map_View::set_scale(double scale_)
{
	if (scale_ <= 0.0)
	{
		throw Error("New map scale must be positive!");
	}
	
	scale = scale_;
	
	return;
}

// any values are legal for the origin
void Map_View::set_origin(Point origin_)
{
	origin = origin_;
	
	return;
}
	
// set the parameters to the default values
void Map_View::set_defaults()
{
	size = 25;
	scale = 2.0;
	origin = Point(-10, -10);
}

