/*
Main module. Your project must use this file as-is
*/

#include <iostream>

#include "Model.h"
#include "Controller.h"

using namespace std;

// The main function creates the Model and Controller objects, 
// then tells the Controller to run. The Controller creates the first
// View object and attaches it to the Model.

int main ()
{	
	// Set output to show two decimal places for floating point numbers
//	cout << fixed << setprecision(2) << endl;
	cout.setf(ios::fixed, ios::floatfield);
	cout.precision(2);

	// create a Model object;
	Model model;
	// create the Controller object and go
	Controller controller(model);

	controller.run();
}

