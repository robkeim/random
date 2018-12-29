using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day25
    {
        public static int Part1(string input)
        {
            var points = input
                .Split("\n".ToCharArray())
                .Select(Point.Parse)
                .ToList();

            var numConstellations = 0;

            while (points.Count > 0)
            {
                numConstellations++;

                var toParse = new List<Point>
                {
                    points.First()
                };

                points.Remove(points.First());

                while (toParse.Count > 0)
                {
                    var point = toParse.First();
                    toParse.Remove(point);

                    var matches = points
                        .Where(p => p.Distance(point) <= 3)
                        .ToList();

                    toParse.AddRange(matches);

                    points = points
                        .Except(matches)
                        .ToList();
                }
            }
            
            return numConstellations;
        }

        private class Point
        {
            public int D1 { get; private set; }
            public int D2 { get; private set; }
            public int D3 { get; private set; }
            public int D4 { get; private set; }

            public static Point Parse(string line)
            {
                var match = Regex.Match(line, @"(-?\d+),(-?\d+),(-?\d+),(-?\d+)");

                return new Point
                {
                    D1 = int.Parse(match.Groups[1].ToString()),
                    D2 = int.Parse(match.Groups[2].ToString()),
                    D3 = int.Parse(match.Groups[3].ToString()),
                    D4 = int.Parse(match.Groups[4].ToString()),
                };
            }

            public int Distance(Point other)
            {
                return Math.Abs(D1 - other.D1)
                    + Math.Abs(D2 - other.D2)
                    + Math.Abs(D3 - other.D3)
                    + Math.Abs(D4 - other.D4);
            }
        }
    }
}
