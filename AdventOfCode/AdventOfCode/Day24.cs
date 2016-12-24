using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/24
    //
    // --- Day 24: Air Duct Spelunking ---
    //
    // You've finally met your match; the doors that provide access to the roof are locked tight, and all of the controls and related electronics are inaccessible. You simply
    // can't reach them.
    //
    // The robot that cleans the air ducts, however, can.
    //
    // It's not a very fast little robot, but you reconfigure it to be able to interface with some of the exposed wires that have been routed through the HVAC system. If you
    // can direct it to each of those locations, you should be able to bypass the security controls.
    //
    // You extract the duct layout for this area from some blueprints you acquired and create a map with the relevant locations marked (your puzzle input). 0 is your current
    // location, from which the cleaning robot embarks; the other numbers are (in no particular order) the locations the robot needs to visit at least once each. Walls are
    // marked as #, and open passages are marked as .. Numbers behave like open passages.
    //
    // For example, suppose you have a map like the following:
    // ###########
    // #0.1.....2#
    // #.#######.#
    // #4.......3#
    // ###########
    //
    // To reach all of the points of interest as quickly as possible, you would have the robot take the following path:
    // - 0 to 4 (2 steps)
    // - 4 to 1 (4 steps; it can't move diagonally)
    // - 1 to 2 (6 steps)
    // - 2 to 3 (2 steps)
    //
    // Since the robot isn't very fast, you need to find it the shortest route. This path is the fewest steps (in the above example, a total of 14) required to start at 0 and
    // then visit every other location at least once.
    //
    // Given your actual map, and starting from location 0, what is the fewest number of steps required to visit every non-0 number marked on the map at least once?
    // #######################################################################################################################################################################################\n#.....#.....#.....#...#...#.#...#...............................#.#.......#.#...#...........#...............#.#...#.....#.....#.....#...................#.......#.#.......#.....#...#.#\n###.#.#.###.#.#.#.#.###.#.#.###.#.#.#.#.###.###.#.#.#.#.#.#.###.#.#.###.#.#.#####.###.#.###.#.###.###.#.#.###.#.###.#.#.#.###.###.###.#.#.#.#.#.#.#.#.#.#.#.###.#.#.#.#.#.#.#.#.#.###.#\n#.#......4#.#.....#...#...#.#...........#...#.#.#...#.#...#...#.#.........#.#.#.........#...........#.............#.....#...#...#.#...#...#...#....3#.....#.....#.............#.......#\n#.#####.###.#.#####.#.#.#.#.#.#####.###.###.#.#.###.#.#####.#.#.#.###.#.#.#.#.#.#.#######.#.#.#.#.###.#####.###.#.#.###.###.#.#.#.#.#####.###.###.#.#.###.#.#####.#####.###.#####.###.#\n#.#...#.....#.#.#...#.#.#.#.#...........#...#...#.....#...#...#.........#.............#.....#...#...#.........#.#...#.......#.....#.....#...#...#...........#.....#.....#...#...#.#...#\n###.#.#.###.#.#.#.#.#.#.#.#.#####.###.###.#.#.###.#######.#.#.###.#.#.###.#######.###.#.#######.#.#####.#.###.#.#####.#.#.#.#.###.#.###.#.#.#.###.###.#.#.###.#.#.###.#.#.#.#.#.###.#.#\n#...#.......#.#.#.........#...#.....#.#.....#...#...........#.#...#.....#.#.......................#.......#...#.............#...#...#...#.........#.....#.#...#...#.#.....#.....#.#...#\n#.#.###.#.#.#.#.#.#####.###.#.#.###.#.#.#.#.###.###.#####.#.#####.#.###.#.#.#.#.###.#.###.#.#####.#.###.#.#.#.#.#.###.#.#.#.#.#.#####.#.#########.#####.#.#.#.#.#.#.#.###.#####.#.#.###\n#...#...#.#.#...........#...#...#...#.........#.#...#.....#...#...#.#...#.....#.....#.....#...#...........#.#.#...#...#.....#.....#.#.......#...........#.#.......#...#.....#...#.#.#.#\n#.#.###.#.#.#.#.###.#.###.#.#.#.###.###.#.###.#.#.#.#.#.#.#.#.#.###.#.###.#.#####.#.#.#.#.#####.#.###.###.#.#.#.#####.#####.#.#.#.#.###.#.#.#.#########.#.###.#.###.#.#.#.#.#.#.#.###.#\n#...#...#...#.#...#...#.......#...............#.#...#.........#...#.#...#...#.............#.#...#.#.......#.....#...#...#.....#.#...#.........#6..#...#.....#.#.....#...#...#...#.....#\n#.#.#.#######.#.#.#.#.#.#####.#.#####.#.#.#.###.#.#.#.#.#.#.#.#.#######.#.###.#.###.#####.#.#.#.#.#.#.#####.#######.#.###.#.#.#.#.#.#.#.###.#####.#.#.###.#####.#####.#.#####.#.#.#.###\n#.....#.....#...#.#...#.#...........#...#...#.......#.....#.#.#...#.#.....#.....#.....#.......#.....#.#...#.#...#.......#.........#...#.......#...#.#.....#...#.....#.#.#.#.#.....#...#\n###.#.###.#.#.###.#.#.#.#.###.###.#.###.#.#.#.#.###.#.###.#######.#.#.###.#.#.#.#.#.#.#.#######.###.###.#.#.#.#.#.#.#.###.#.#.#.###.#.###.###.#.#.###.#####.#.#.#.#.#.#.#.#.#.###.#####\n#.#.#.#.....#...#.#...#...#...#.........#.....#.....#.#...#.......#...#.....#...#...#...#...#.....#...#.#...#.........#.....#.....#.......#.....#.#.....#...#...#...#.#.....#.........#\n#.###.#.#.###.#.#.#.#.#.#.###.#####.###.#.###.#.#.#.#.#.#.#.#####.#.#.#.###.#.#.#####.#####.#.#####.#.#.#######.#.#.#.#.#.#.###.#.#.#.#.#.#######.#.###.#.#####.###.###.#.#.#.#.#.###.#\n#.....#.#5#...#...#...#.#.....#.........#.....#.#.......#...#.......#.#.......#.#.#...........#.......#...#.#.....#.#.........#...#.#.....#.......#..0#.........#...#.....#.#.....#.#.#\n#.###.#.###.#.#.#.#.#.#.###.###.#####.#########.#.#.#.#.###.#.#.###.#.#.#.#.#.###.#.###.#####.#.#.#####.#.#.#.#.###.#######.#####.#########.#.#.#.#.#.#.###.#.#.#.#.#.###.#.#####.#.#.#\n#.....#.........#...#...#.........#.....#.......#...#.#.......#.....#.#...#.#.#...#.#.#.....#...............#.#.....#.......#.#.......#.......#.#.....#.#.......#.........#.......#...#\n#.###.#.#.#.###.#.###.#########.#.###.#.#.###.#.#.#####.#.#.#.#####.#.#.#.#.###.#.#.#.#.#.###.#.#.#.#.#.###.#.#.#####.#.#.###.#.###.#.#.#####.#.#.#####.#.#.###.#########.#.#.#######.#\n#.....#.......................#.#.#...#...#.............#.#.....#...........#...#.#...#.........#.#...#.....#...........#.#.#.......#.#...#.......#.........#.................#.#.....#\n#.#.#.#.#.#.#.###.#######.#.#.#.###.#.#.###.#.#.#.#.#.#.#.#.###.#.#.#.#####.#.#.#.#.#.#######.###.#.#.###.#.#######.###.#.#.#####.#.#.#.#.#.#.#.#.#.#.#.#######.#.#####.#.#####.###.#.#\n#...#...#...#.......#.#...#.#.#...#.#...........#.#.......#.....#.......#.......#.............#...#.#.#...#.......#.#.....#...#...#.....#.......#.....#...#.....#.........#.....#.#...#\n#.#.###.#.#.#.###.#.#.#.#.#.#.###.#.###.###.###.#.#.###.#.#.#.#.#.#.###.#####.#.#.#.###.#.#.#.#.#.#.#.#.#.###.###.#.#.#.#.#.#.#.#.#.###.#.#.#.#.###.###.#.#.###.#####.#.#.#.###.#.#.#.#\n#...............#.....#...#...#.....#.........#.....#...#...#.#.........#...........#.#.....#.#.....#...#...#...#.#.........#.....#...#.............#1#...#.#.........#.#...#.......#.#\n#####.###.#.#.###.#.#####.#.#.#.###.#.#.#.#.#.#.#.#.#.#.###.###.###.#.###.#.#.###.###.#.#.#.###.#.#.#.###.###.#.#.#.#.#.#.#.###.#.#.#.#.#####.###.#.#.#.#.#.#######.#.#.#.###.#.#.#.###\n#.#...........#.....#...#...#.#.........#...#...#.......#.....#.#.....#.........#.#...#...#...#.......#...#.......#.#.#...#.......#...#...............#.........#.......#.#...#.....#.#\n#.#.#.#.#.###.#.#.#.#.#.#.#.#.###.###.###.#.#.#.#.#######.#.#.###.#####.###.###.#.#.#.#######.#######.#.###.###.#.#.#.#.#.#.#.#.#.###.###.#.#.#####.#.###.#.#####.#########.###.#.#.#.#\n#...........#.....#.......#...#...#...#7..#.....#.......#.....#...#.....#.......#.#...#...#.....#.......#.#.#.....#...............#...#.#...#.#...#.......#.#.#.#...#...#.#.#.....#.#.#\n###.###.###.#####.#.#########.#####.#.###.#######.###.#.###.#.###.###.#.###.#.#.#.###.#.#.#.#.#.#.#.#.#.#.#.#####.#.###.#.#####.#.#.#.#.###.#.#.#.#.###.#.#.#.#.###.#.#.#.#####.#.#.#.#\n#...#...#.#.......#...........#.......#.....#.#.......#.....#.#...#...#...#...#.......#...#.#.........#.............#...........#.#...#.#...#.#...#...#...#...#...#.#.....#.......#...#\n#.###.#.#.#####.#.###.#.#.#####.#.#.#.#.###.#.#.###.#.#.#.###.#.#.#.#.#######.#.###.###.#.###.#####.#.#.#.###.#.#.#.#####.#.#.#.#####.#.#.###.#.#.#.#.#.###.#.###.###.#.#.#.#.#.#.#.###\n#.#.....#.#.#.........#...........#...............#...#.#.#.#.#...#...#.........#...#...#.#.#.#...#...#.......#.#.....#.....#...#.#...#.#...........#.....#.#.....#...#.........#.....#\n#.#.###.#.#.#.#.#.#.#.#####.#.#.#.#.#.#####.###.###.###.###.###.###.#############.###.###.#.#.#.#.#.#.#.###.#.#.#####.#.#.#######.###.#.#.#.###.#.#.#.#.###.#######.#.#.#.#.###.#####.#\n#...#.#.#.#...#.#...#.....#...........#.#...#.......#...........#...#.....#.#...........#.#.....#...#.#...#...#...............#...#...#.#...#...#...#...#.....#2......#...#.#.#.#.....#\n#######################################################################################################################################################################################
    // Answer: 474
    //
    // --- Part Two ---
    //
    // Of course, if you leave the cleaning robot somewhere weird, someone is bound to notice.
    //
    // What is the fewest number of steps required to start at 0, visit every non-0 number marked on the map at least once, and then return to 0?
    // Answer: 696
    public static class Day24
    {
        public static int FindShortestPath(string input, bool returnToStart)
        {
            var lines = input.Split("\n".ToCharArray());

            int maxX = lines[0].Length;
            int maxY = lines.Length;
            var grid = new char[maxX, maxY];
            var xPos = new int[8];
            var yPos = new int[8];

            for (int y = 0; y < maxY; y++)
            {
                for (int x = 0; x < maxX; x++)
                {
                    var cur = lines[y][x];
                    int val;

                    if (int.TryParse(cur.ToString(), out val))
                    {
                        xPos[val] = x;
                        yPos[val] = y;
                    }

                    grid[x, y] = cur;
                }
            }

            var distances = new int[8, 8];

            for (int start = 0; start < 8; start++)
            {
                for (int dest = start + 1; dest < 8; dest++)
                {
                    var distance = CalculateMinDistance(start, dest, maxX, maxY, xPos, yPos, grid);
                    distances[start, dest] = distance;
                    distances[dest, start] = distance;
                }
            }
            
            var min = int.MaxValue;
            
            foreach (var perm in GetPermutations("1234567"))
            {
                var permutation = "0" + perm; // Also start at 0
                var distance = 0;
                for (int i = 0; i < 7; i++)
                {
                    distance += distances[int.Parse(permutation[i].ToString()), int.Parse(permutation[i + 1].ToString())];
                }

                if (returnToStart)
                {
                    distance += distances[int.Parse(permutation[7].ToString()), int.Parse(permutation[0].ToString())];
                }
                
                min = Math.Min(min, distance);
            }
            
            return min;
        }

        private static int CalculateMinDistance(int start, int dest, int maxX, int maxY, int[] xPos, int[] yPos, char[,] grid)
        {
            var states = new Queue<State>();
            var visited = new HashSet<string>();

            var state = new State(0, xPos[start], yPos[start]);
            states.Enqueue(state);
            visited.Add(state.ToString());

            while (states.Count != 0)
            {
                state = states.Dequeue();

                if (grid[state.X, state.Y] == '#')
                {
                    continue;
                }

                if (state.X == xPos[dest] && state.Y == yPos[dest])
                {
                    return state.NumSteps;
                }
                
                // rename to next
                var next = State.Incremenet(state, xOffset: -1);

                if (state.X > 0 && visited.Add(next.ToString()))
                {
                    states.Enqueue(next);
                }

                next = State.Incremenet(state, xOffset: 1);

                if (state.X < maxX - 1 && visited.Add(next.ToString()))
                {
                    states.Enqueue(next);
                }

                next = State.Incremenet(state, yOffset: -1);

                if (state.Y > 0 && visited.Add(next.ToString()))
                {
                    states.Enqueue(next);
                }

                next = State.Incremenet(state, yOffset: 1);

                if (state.Y < maxX - 1 && visited.Add(next.ToString()))
                {
                    states.Enqueue(next);
                }
            }

            throw new Exception("Unreachable code");
        }
        
        private class State
        {
            public int NumSteps { get; set; }
            public int X { get; set; }
            public int Y { get; set; }

            public State(int numSteps, int x, int y)
            {
                NumSteps = numSteps;
                X = x;
                Y = y;
            }
            
            public static State Incremenet(State state, int xOffset = 0, int yOffset = 0)
            {
                return new State(state.NumSteps + 1, state.X + xOffset, state.Y + yOffset);
            }

            public override string ToString()
            {
                return $"{X}_{Y}";
            }
        }

        private static List<string> GetPermutations(string input)
        {
            return GetPermutations(string.Empty, input);
        }

        private static List<string> GetPermutations(string soFar, string remaining)
        {
            if (remaining.Length == 0)
            {
                return new List<string> { soFar };
            }

            var results = new List<string>();

            for (int i = 0; i < remaining.Length; i++)
            {
                var nextSoFar = $"{soFar}{remaining[i]}";
                var nextRemaining = remaining.Remove(i, 1);

                results.AddRange(GetPermutations(nextSoFar, nextRemaining));
            }

            return results;
        }
    }
}
