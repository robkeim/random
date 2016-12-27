using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalWriting
{
    public class FindCheeseRecursive
    {
        // Search through a previously initialized maze to find the cheese and print "Success!" upon successfully finding the cheese
        public void FindCheese(IMaze maze)
        {
            if (maze == null)
            {
                throw new ArgumentNullException("The maze cannot be null");
            }

            if (!FindCheese(maze, new HashSet<Point>(), new Point { X = 0, Y = 0 }))
            {
                Console.WriteLine("There was no cheese in the maze or the mouse couldn't reach the cheese");
            }
        }

        // Search through the maze and return true when the cheese has been located
        private bool FindCheese(IMaze maze, HashSet<Point> visitedLocations, Point currentLocation)
        {
            if (visitedLocations.Contains(currentLocation))
            {
                return false;
            }

            visitedLocations.Add(currentLocation);

            if (maze.Success())
            {
                Console.WriteLine("Success!");
                return true;
            }

            // Try moving left
            if (maze.Move(Direction.Left))
            {
                Point newLocation = new Point { X = currentLocation.X - 1, Y = currentLocation.Y };
                if (FindCheese(maze, visitedLocations, newLocation))
                {
                    return true;
                }

                maze.Move(Direction.Right);
            }

            // Try moving down
            if (maze.Move(Direction.Down))
            {
                Point newLocation = new Point { X = currentLocation.X, Y = currentLocation.Y - 1 };
                if (FindCheese(maze, visitedLocations, newLocation))
                {
                    return true;
                }

                maze.Move(Direction.Up);
            }

            // Try moving right
            if (maze.Move(Direction.Right))
            {
                Point newLocation = new Point { X = currentLocation.X + 1, Y = currentLocation.Y };
                if (FindCheese(maze, visitedLocations, newLocation))
                {
                    return true;
                }

                maze.Move(Direction.Left);
            }

            // Try moving up
            if (maze.Move(Direction.Up))
            {
                Point newLocation = new Point { X = currentLocation.X, Y = currentLocation.Y + 1 };
                if (FindCheese(maze, visitedLocations, newLocation))
                {
                    return true;
                }

                maze.Move(Direction.Down);
            }

            return false;
        }

        public struct Point
        {
            public int X;
            public int Y;
        }
    }
}
