using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalWriting
{
    public class FindCheeseIterative
    {
        // Search through a previously initialized maze to find the cheese and print "Success!" upon successfully finding the cheese
        public void FindCheese(IMaze maze)
        {
            if (maze == null)
            {
                throw new ArgumentNullException("The maze cannot be null");
            }

            HashSet<Point> visitedLocations = new HashSet<Point>();
            Point currentLocation = new Point { X = 0, Y = 0 };
            visitedLocations.Add(currentLocation);

            Stack<Direction?> path = new Stack<Direction?>();
            path.Push(null);

            bool foundCheese = false;

            while (path.Count > 0)
            {
                if (maze.Success())
                {
                    foundCheese = true;
                    break;
                }

                Direction? top = path.Pop();

                if (maze.Move(Direction.Left))
                {
                    Point newLocation = new Point { X = currentLocation.X - 1, Y = currentLocation.Y };

                    if (!visitedLocations.Contains(newLocation))
                    {
                        currentLocation = newLocation;
                        visitedLocations.Add(newLocation);
                        path.Push(top);
                        path.Push(Direction.Left);
                    }
                    else
                    {
                        maze.Move(Direction.Right);
                    }
                }
                else if (maze.Move(Direction.Down))
                {
                    Point newLocation = new Point { X = currentLocation.X, Y = currentLocation.Y - 1 };

                    if (!visitedLocations.Contains(newLocation))
                    {
                        currentLocation = newLocation;
                        visitedLocations.Add(newLocation);
                        path.Push(top);
                        path.Push(Direction.Down);
                    }
                    else
                    {
                        maze.Move(Direction.Up);
                    }
                }
                else if (maze.Move(Direction.Right))
                {
                    Point newLocation = new Point { X = currentLocation.X + 1, Y = currentLocation.Y };

                    if (!visitedLocations.Contains(newLocation))
                    {
                        currentLocation = newLocation;
                        visitedLocations.Add(newLocation);
                        path.Push(top);
                        path.Push(Direction.Right);
                    }
                    else
                    {
                        maze.Move(Direction.Left);
                    }
                }
                else if (maze.Move(Direction.Up))
                {
                    Point newLocation = new Point { X = currentLocation.X, Y = currentLocation.Y + 1 };

                    if (!visitedLocations.Contains(newLocation))
                    {
                        currentLocation = newLocation;
                        visitedLocations.Add(newLocation);
                        path.Push(top);
                        path.Push(Direction.Up);
                    }
                    else
                    {
                        maze.Move(Direction.Down);
                    }
                }
                else
                {
                    if (top != null)
                    {
                        maze.Move(GetReverseDirection(top.Value));

                        switch (GetReverseDirection(top.Value))
                        {
                            case Direction.Left:
                                currentLocation = new Point { X = currentLocation.X - 1, Y = currentLocation.Y };
                                break;
                            case Direction.Down:
                                currentLocation = new Point { X = currentLocation.X, Y = currentLocation.Y - 1 };
                                break;
                            case Direction.Right:
                                currentLocation = new Point { X = currentLocation.X + 1, Y = currentLocation.Y };
                                break;
                            case Direction.Up:
                                currentLocation = new Point { X = currentLocation.X, Y = currentLocation.Y - 1 };
                                break;
                            default:
                                throw new ArgumentException("Unknown direction encountered");
                                break;
                        }
                    }
                }
            }

            if (foundCheese)
            {
                Console.WriteLine("Success!");
            }
            else
            {
                Console.WriteLine("There was no cheese in the maze or the mouse couldn't reach the cheese");
            }
        }

        public struct Point
        {
            public int X;
            public int Y;
        }

        private Direction GetReverseDirection(Direction direction)
        {
            Direction result;

            switch (direction)
            {
                case Direction.Left:
                    result = Direction.Right;
                    break;
                case Direction.Down:
                    result = Direction.Up;
                    break;
                case Direction.Right:
                    result = Direction.Left;
                    break;
                case Direction.Up:
                    result = Direction.Down;
                    break;
                default:
                    throw new ArgumentException("Unexpected direction encountered");
                    break;
            }

            return result;
        }
    }
}
