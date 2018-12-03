using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day03
    {
        public static int Part1(string input)
        {
            var lineRegex = new Regex(@" @ (\d+),(\d+): (\d+)x(\d+)$", RegexOptions.Compiled);
            var squares = new HashSet<string>();
            var duplicates = new HashSet<string>();

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var match = lineRegex.Match(line);

                if (!match.Success)
                {
                    throw new ArgumentException($"Invalid line format: {line}");
                }

                var x = int.Parse(match.Groups[1].ToString());
                var y = int.Parse(match.Groups[2].ToString());
                var width = int.Parse(match.Groups[3].ToString());
                var height = int.Parse(match.Groups[4].ToString());

                for (int dx = 0; dx < width; dx++)
                {
                    for (int dy = 0; dy < height; dy++)
                    {
                        var coordinate = $"{x + dx}_{y + dy}";
                        if (!squares.Add(coordinate))
                        {
                            duplicates.Add(coordinate);
                        }
                    }
                }
            }

            return duplicates.Count;
        }

        public static int Part2(string input)
        {
            var lineRegex = new Regex(@"#(\d+) @ (\d+),(\d+): (\d+)x(\d+)$", RegexOptions.Compiled);
            var squares = new Dictionary<string, int>();
            var duplicates = new HashSet<int>();

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var match = lineRegex.Match(line);

                if (!match.Success)
                {
                    throw new ArgumentException($"Invalid line format: {line}");
                }

                var claimNumber = int.Parse(match.Groups[1].ToString());
                var x = int.Parse(match.Groups[2].ToString());
                var y = int.Parse(match.Groups[3].ToString());
                var width = int.Parse(match.Groups[4].ToString());
                var height = int.Parse(match.Groups[5].ToString());

                for (int dx = 0; dx < width; dx++)
                {
                    for (int dy = 0; dy < height; dy++)
                    {
                        var coordinate = $"{x + dx}_{y + dy}";
                        if (squares.ContainsKey(coordinate))
                        {
                            duplicates.Add(claimNumber);
                            duplicates.Add(squares[coordinate]);
                        }
                        else
                        {
                            squares[coordinate] = claimNumber;
                        }
                    }
                }
            }

            int result = 1;

            while (duplicates.Contains(result))
            {
                result++;
            }

            return result;
        }
    }
}
