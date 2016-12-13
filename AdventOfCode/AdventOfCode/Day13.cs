using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    // --- Day 13: A Maze of Twisty Little Cubicles ---
    //
    // You arrive at the first floor of this new building to discover a much less welcoming environment than the shiny atrium of the last one.Instead, you are in
    // a maze of twisty little cubicles, all alike.
    //
    // Every location in this area is addressed by a pair of non-negative integers (x, y). Each such coordinate is either a wall or an open space.You can't move
    // diagonally. The cube maze starts at 0,0 and seems to extend infinitely toward positive x and y; negative values are invalid, as they represent a location
    // outside the building. You are in a small waiting area at 1,1.
    //
    // While it seems chaotic, a nearby morale-boosting poster explains, the layout is actually quite logical.You can determine whether a given x,y coordinate will
    // be a wall or an open space using a simple system:
    // Find x* x + 3* x + 2* x* y + y + y* y.
    // Add the office designer's favorite number (your puzzle input).
    // Find the binary representation of that sum; count the number of bits that are 1.
    // If the number of bits that are 1 is even, it's an open space.
    // If the number of bits that are 1 is odd, it's a wall.
    //
    // For example, if the office designer's favorite number were 10, drawing walls as # and open spaces as ., the corner of the building containing 0,0 would look
    // like this:
    //   0123456789
    // 0 .#.####.##
    // 1 ..#..#...#
    // 2 #....##...
    // 3 ###.#.###.
    // 4 .##..#..#.
    // 5 ..##....#.
    // 6 #...##.###
    //
    // Now, suppose you wanted to reach 7,4. The shortest route you could take is marked as O:
    //   0123456789
    // 0 .#.####.##
    // 1 .O#..#...#
    // 2 #OOO.##...
    // 3 ###O#.###.
    // 4 .##OO#OO#.
    // 5 ..##OOO.#.
    // 6 #...##.###
    // Thus, reaching 7,4 would take a minimum of 11 steps(starting from your current location, 1,1).
    //
    // What is the fewest number of steps required for you to reach 31,39?
    // 1352
    // Answer: 90
    //
    // --- Part Two ---
    //
    // How many locations(distinct x, y coordinates, including your starting location) can you reach in at most 50 steps?
    // Answer: XXX
    public static class Day13
    {
        public static int FindShortestPath(int favNumber, int x, int y)
        {
            var target = new Coordinate(x, y);
            var itemsToProcess = new Queue<Tuple<Coordinate, int>>();
            var visitedLocations = new Dictionary<Coordinate, int>();
            var tuple = CreateCoordinateAndDepth(1, 1, 0);
            visitedLocations[tuple.Item1] = 0;
            itemsToProcess.Enqueue(tuple);

            while (itemsToProcess.Count != 0)
            {
                var item = itemsToProcess.Dequeue();
                var curPos = item.Item1;
                var depth = item.Item2;
                
                if (target.Equals(curPos))
                {
                    return depth;
                }

                foreach (var neighbor in curPos.GetNeighbors())
                {
                    if (IsOpenSpace(neighbor, favNumber))
                    {
                        int curDepth;
                        if (!visitedLocations.TryGetValue(neighbor, out curDepth))
                        {
                            curDepth = int.MaxValue;
                        }

                        if (depth + 1 < curDepth)
                        {
                            visitedLocations[neighbor] = depth + 1;
                            itemsToProcess.Enqueue(CreateCoordinateAndDepth(neighbor, depth + 1));
                        }
                    }
                }
            }

            return -1;
        }
        
        private static Tuple<Coordinate, int> CreateCoordinateAndDepth(int x, int y, int depth)
        {
            return CreateCoordinateAndDepth(new Coordinate(x, y), depth);
        }

        private static Tuple<Coordinate, int> CreateCoordinateAndDepth(Coordinate coord, int depth)
        {
            return new Tuple<Coordinate, int>(coord, depth);
        }

        private static void PrintBoard(int favNumber, int xMax, int yMax, Dictionary<Coordinate, int> visitedLocations)
        {
            Console.WriteLine();

            for (int y = 0; y <= yMax; y++)
            {
                for (int x = 0; x <= xMax; x++)
                {
                    var coord = new Coordinate(x, y);

                    var result = !IsOpenSpace(coord, favNumber)
                        ? "#"
                        : visitedLocations.ContainsKey(new Coordinate(x, y))
                            ? "0"
                            : ".";

                    Console.Write(result);
                }
                Console.WriteLine();
            }
        }

        private static bool IsOpenSpace(Coordinate coord, int favNumber)
        {
            var number =
                favNumber
                + coord.X * coord.X
                + 3 * coord.X
                + 2 * coord.X * coord.Y
                + coord.Y
                + coord.Y * coord.Y;

            var binary = Convert.ToString(number, 2);
            
            return binary.Count(c => c == '1') % 2 == 0;
        }

        private struct Coordinate
        {
            public int X { get; }
            public int Y { get; }

            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public List<Coordinate> GetNeighbors()
            {
                var result = new List<Coordinate>();

                result.Add(new Coordinate(X + 1, Y));
                result.Add(new Coordinate(X, Y + 1));

                if (X >= 1)
                {
                    result.Add(new Coordinate(X - 1, Y));
                }

                if (Y >= 1)
                {
                    result.Add(new Coordinate(X, Y - 1));
                }

                return result;
            }

            public override bool Equals(object obj)
            {
                if (obj is Coordinate)
                {
                    var castObj = (Coordinate)obj;

                    return X == castObj.X && Y == castObj.Y;
                }

                return false;
            }

            public override string ToString()
            {
                return $"({X}, {Y})";
            }
        }
    }
}
