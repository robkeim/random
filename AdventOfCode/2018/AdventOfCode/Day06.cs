using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day06
    {
        public static int Part1(string input)
        {
            var coords = new List<Tuple<int, int>>();

            // Parse input
            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var split = line
                    .Split(",".ToCharArray())
                    .Select(int.Parse)
                    .ToArray();

                coords.Add(new Tuple<int, int>(split[0], split[1]));
            }

            // Find bounds
            var minX = int.MaxValue;
            var minY = int.MaxValue;
            var maxX = int.MinValue;
            var maxY = int.MinValue;

            foreach (var coord in coords)
            {
                if (coord.Item1 < minX)
                {
                    minX = coord.Item1;
                }
                else if (coord.Item1 > maxX)
                {
                    maxX = coord.Item1;
                }

                if (coord.Item2 < minY)
                {
                    minY = coord.Item2;
                }
                else if (coord.Item2 > maxY)
                {
                    maxY = coord.Item2;
                }
            }

            // Find closest at each point
            var closestAtPoint = new Dictionary<string, string>();

            for (int x = minX; x <= maxX; x++)
            {
                for (int y = minY; y <= maxY; y++)
                {
                    var closestDist = int.MaxValue;
                    var closestValue = string.Empty;
                    var hasTie = false;

                    for (int i = 0; i < coords.Count; i++)
                    {
                        var dist = Math.Abs(coords[i].Item1 - x) + Math.Abs(coords[i].Item2 - y);

                        if (dist < closestDist)
                        {
                            closestDist = dist;
                            closestValue = $"{coords[i].Item1}_{coords[i].Item2}";
                            hasTie = false;
                        }
                        else if (dist == closestDist)
                        {
                            hasTie = true;
                        }
                    }

                    if (!hasTie)
                    {
                        closestAtPoint[$"{x}_{y}"] = closestValue;
                    }
                    else
                    {
                        closestAtPoint[$"{x}_{y}"] = ".";
                    }

                }
            }

            // Determine infinite edges
            var edges = new HashSet<string>();
            edges.Add(".");

            for (int x = minX; x <= maxX; x++)
            {
                edges.Add(closestAtPoint[$"{x}_{minY}"]);
                edges.Add(closestAtPoint[$"{x}_{maxY}"]);
            }

            for (int y = minY; y <= maxY; y++)
            {
                edges.Add(closestAtPoint[$"{minX}_{y}"]);
                edges.Add(closestAtPoint[$"{maxX}_{y}"]);
            }

            // Find largest area
            var areas = closestAtPoint
                .GroupBy(kvp => kvp.Value)
                .OrderByDescending(group => group.Count())
                .ToArray();

            var index = 0;

            while (true)
            {
                var group = areas[index];
                if (!edges.Contains(group.Key))
                {
                    return group.Count();
                }

                index++;
            }
        }

        public static int Part2(string input)
        {
            return -1;
        }
    }
}
