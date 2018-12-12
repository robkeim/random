using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class Day12
    {
        public static int Part1(string initialState, string inputRules)
        {
            const int numGenerations = 20;
            var growsPlant = new HashSet<string>();

            foreach (var line in inputRules.Split("\n".ToCharArray()))
            {
                var split = line.Split(" =>".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                if (split[1] == "#")
                {
                    growsPlant.Add(split[0]);
                }
            }

            for (int generation = 0; generation < numGenerations; generation++)
            {
                var newState = new StringBuilder();
                
                initialState = $"....{initialState}....";

                for (int i = 2; i < initialState.Length - 2; i++)
                {
                    var toReplace = initialState.Substring(i - 2, 5);
                    newState.Append(growsPlant.Contains(toReplace) ? "#" : ".");
                }

                initialState = newState.ToString();
            }

            var result = 0;

            for (int i = 0; i < initialState.Length; i++)
            {
                if (initialState[i] == '#')
                {
                    result += (i - 2 * numGenerations);
                }
            }

            return result;
        }

        public static int Part2(string initialState, string inputRules)
        {
            return -1;
        }
    }
}
