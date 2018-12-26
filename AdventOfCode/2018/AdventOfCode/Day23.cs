using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day23
    {
        public static int Part1(string input)
        {
            var coordinates = new List<Coordinate>();

            var maxRadius = int.MinValue;
            Coordinate maxCoordinate = null;

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var coordinate = Coordinate.Parse(line);
                coordinates.Add(coordinate);

                if (coordinate.Radius > maxRadius)
                {
                    maxRadius = coordinate.Radius;
                    maxCoordinate = coordinate;
                }
            }

            var numInRange = 0;

            foreach (var coordinate in coordinates)
            {
                if (coordinate.Distance(maxCoordinate) <= maxRadius)
                {
                    numInRange++;
                }
            }
            
            return numInRange;
        }

        private class Coordinate
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            public int Z { get; private set; }
            public int Radius { get; private set; }

            public static Coordinate Parse(string line)
            {
                var match = Regex.Match(line, @"pos=<(-?\d+),(-?\d+),(-?\d+)>, r=(-?\d+)");

                return new Coordinate
                {
                    X = int.Parse(match.Groups[1].ToString()),
                    Y = int.Parse(match.Groups[2].ToString()),
                    Z = int.Parse(match.Groups[3].ToString()),
                    Radius = int.Parse(match.Groups[4].ToString()),
                };
            }

            public int Distance(Coordinate coord)
            {
                return Math.Abs(X - coord.X) + Math.Abs(Y - coord.Y) + Math.Abs(Z - coord.Z);
            }
        }

        public static int Part2(string input)
        {
            return -1;
        }
    }
}
