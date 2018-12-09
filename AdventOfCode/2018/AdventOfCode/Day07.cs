using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day07
    {
        public static string Part1(string input)
        {
            var regex = new Regex("Step ([A-Z]) must be finished before step ([A-Z]) can begin.", RegexOptions.Compiled);

            var steps = new HashSet<string>();
            var blockedBy = new Dictionary<string, List<string>>();

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var match = regex.Match(line);

                var first = match.Groups[1].ToString();
                var second = match.Groups[2].ToString();

                steps.Add(first);
                steps.Add(second);

                if (!blockedBy.ContainsKey(second))
                {
                    blockedBy[second] = new List<string>();
                }

                blockedBy[second].Add(first);
            }

            var result = new List<string>();
            var nodesToProcess = new List<string>();

            foreach (var step in steps)
            {
                if (!blockedBy.ContainsKey(step))
                {
                    nodesToProcess.Add(step);
                }
            }

            while (nodesToProcess.Count > 0)
            {
                var node = nodesToProcess.OrderBy(n => n).First();
                nodesToProcess.Remove(node);
                result.Add(node);

                var toRemove = new List<string>();

                foreach (var blocked in blockedBy)
                {
                    if (blocked.Value.Contains(node))
                    {
                        blocked.Value.Remove(node);

                        if (blocked.Value.Count == 0)
                        {
                            toRemove.Add(blocked.Key);
                            nodesToProcess.Add(blocked.Key);
                        }
                    }
                }

                foreach (var item in toRemove)
                {
                    blockedBy.Remove(item);
                }
            }

            return string.Join("", result);
        }

        public static int Part2(string input)
        {
            return -1;
        }
    }
}
