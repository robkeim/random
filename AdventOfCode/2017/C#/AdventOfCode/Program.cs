using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Program
    {
        public static void Main(string[] args)
        {
            // Part 1: 125 (after 5 iterations)
            // Part 2: 1782917 (after 18 iterations)

            var numIterations = 18;
            //var rawRules = @"../.# => ##./#../...@.#./..#/### => #..#/..../..../#..#";
            var rawRules = @"../.. => .../.../###@#./.. => .../.#./.##@##/.. => .#./.#./...@.#/#. => ###/..#/.##@##/#. => ..#/###/#..@##/## => ..#/#../##.@.../.../... => .##./##../..##/.##.@#../.../... => ##../.#.#/..#./###.@.#./.../... => ##.#/#.#./.#../..##@##./.../... => ...#/##.#/.#.#/#.##@#.#/.../... => ..#./#.../###./...#@###/.../... => #.#./...#/#.#./###.@.#./#../... => ...#/###./.##./...#@##./#../... => ###./####/###./..##@..#/#../... => ####/#.../####/#.##@#.#/#../... => #.##/.#.#/##.#/###.@.##/#../... => ..../.#../.#.#/.##.@###/#../... => ..##/##.#/..##/.###@.../.#./... => ###./..##/.#../#..#@#../.#./... => ###./.#../#.../#...@.#./.#./... => ####/..#./.##./##..@##./.#./... => .#../#.#./###./###.@#.#/.#./... => ####/.##./##.#/.###@###/.#./... => #.#./..##/.##./#...@.#./##./... => ####/#.##/####/..#.@##./##./... => #.../.#../..../#.##@..#/##./... => #..#/..##/#.../####@#.#/##./... => ###./##../..##/#...@.##/##./... => ..../#.##/.###/#.#.@###/##./... => .#../##.#/.#../##..@.../#.#/... => ...#/.###/.##./###.@#../#.#/... => ###./##../#.#./.##.@.#./#.#/... => ..#./.#../.##./.###@##./#.#/... => #.../#.../.##./.#..@#.#/#.#/... => .##./..##/.###/#...@###/#.#/... => ..../####/###./....@.../###/... => #.##/.#.#/#.##/...#@#../###/... => #.../#.#./.#../#...@.#./###/... => ...#/###./.##./.#.#@##./###/... => ##../####/###./#.##@#.#/###/... => ...#/###./##.#/.#.#@###/###/... => #.#./##.#/..../.##.@..#/.../#.. => ...#/..#./..#./##..@#.#/.../#.. => ..#./#.##/#.#./#.##@.##/.../#.. => ####/####/#.##/#...@###/.../#.. => ###./..#./###./.#..@.##/#../#.. => ...#/####/..../###.@###/#../#.. => ##.#/.#../##.#/...#@..#/.#./#.. => ###./#.##/...#/##..@#.#/.#./#.. => #.../..#./..#./#.##@.##/.#./#.. => ##.#/...#/#.#./.###@###/.#./#.. => .#../..##/#.#./..#.@.##/##./#.. => #.../#.#./.###/#...@###/##./#.. => .##./.#../.#.#/.###@#../..#/#.. => ###./#..#/#.../##.#@.#./..#/#.. => #.#./#..#/#.../.###@##./..#/#.. => ...#/..##/..#./####@#.#/..#/#.. => ####/#..#/###./#.#.@.##/..#/#.. => ..#./..#./..../.##.@###/..#/#.. => ...#/#..#/#.#./....@#../#.#/#.. => ..##/.#.#/.###/.##.@.#./#.#/#.. => ..../##.#/..##/#..#@##./#.#/#.. => ..#./..##/#..#/#..#@..#/#.#/#.. => ..#./#.../#.#./##..@#.#/#.#/#.. => ##.#/..##/.###/...#@.##/#.#/#.. => #.##/.##./##../#.#.@###/#.#/#.. => ####/##.#/#..#/#.#.@#../.##/#.. => ..##/#.#./####/####@.#./.##/#.. => ##../###./####/....@##./.##/#.. => .###/####/..#./...#@#.#/.##/#.. => ###./##../##../#.##@.##/.##/#.. => ##../.###/####/.#.#@###/.##/#.. => ##../.##./#.../..#.@#../###/#.. => #.#./.#.#/#.../....@.#./###/#.. => .##./##../...#/##..@##./###/#.. => #.#./..../.##./##.#@..#/###/#.. => ...#/...#/##.#/...#@#.#/###/#.. => .##./.###/#..#/.##.@.##/###/#.. => ####/..##/#.../####@###/###/#.. => ...#/####/..#./.###@.#./#.#/.#. => .##./#.##/.##./.###@##./#.#/.#. => ..##/.#../##.#/###.@#.#/#.#/.#. => .#../..../.#.#/#...@###/#.#/.#. => ###./..#./..../#.#.@.#./###/.#. => #..#/.#../#.../..##@##./###/.#. => .##./...#/.###/....@#.#/###/.#. => .###/###./#.#./.#.#@###/###/.#. => #.##/.#.#/#.#./.##.@#.#/..#/##. => .###/..../####/####@###/..#/##. => #.##/###./..##/.##.@.##/#.#/##. => ..../...#/#..#/..##@###/#.#/##. => #.##/.#../.#../....@#.#/.##/##. => ..##/..##/#.../#..#@###/.##/##. => ##.#/#.../#.##/..##@.##/###/##. => ...#/..#./##../#.##@###/###/##. => #.##/#..#/..#./...#@#.#/.../#.# => ##.#/.#../##.#/.##.@###/.../#.# => #.#./..##/.#.#/##.#@###/#../#.# => ..#./#.##/...#/.###@#.#/.#./#.# => .###/#.##/#..#/#.##@###/.#./#.# => ..../..#./###./..#.@###/##./#.# => .###/##../..##/####@#.#/#.#/#.# => #.#./####/.#../.##.@###/#.#/#.# => ####/..../..##/#...@#.#/###/#.# => #.../.##./#.../...#@###/###/#.# => .#.#/...#/..../..##@###/#.#/### => .#../#.##/#.##/.###@###/###/### => #.../.#.#/#..#/#.##";

            var rules = ParseRules(rawRules);
            var grid = new[]
            {
                ".#.",
                "..#",
                "###"
            };

            for (int i = 0; i < numIterations; i++)
            {
                grid = RunIteration(grid, rules);

                var numOn = grid
                .Select(line => line.Count(c => c == '#'))
                .Sum();

                PrintGrid(grid);

                Console.WriteLine($"Num on after {i + 1} iterations: {numOn}");
            }

            Console.WriteLine("done!");
            Console.ReadLine();
        }

        private static Dictionary<string, string> ParseRules(string rules)
        {
            var result = new Dictionary<string, string>();

            foreach (var rawRule in rules.Split("@".ToCharArray()))
            {
                var match = Regex.Match(rawRule, "(.+) => (.+)");
                var pattern = match.Groups[1].ToString();
                var replacement = match.Groups[2].ToString();

                result[pattern] = replacement;
                result[Rotate90(pattern)] = replacement;
                result[Rotate180(pattern)] = replacement;
                result[Rotate270(pattern)] = replacement;

                var flippedVertically = FlipVertically(pattern);
                result[flippedVertically] = replacement;
                result[Rotate90(flippedVertically)] = replacement;
                result[Rotate180(flippedVertically)] = replacement;
                result[Rotate270(flippedVertically)] = replacement;

                var flippedHorintally = FlipHorizontally(pattern);
                result[flippedHorintally] = replacement;
                result[Rotate90(flippedHorintally)] = replacement;
                result[Rotate180(flippedHorintally)] = replacement;
                result[Rotate270(flippedHorintally)] = replacement;
            }

            return result;
        }

        private static string Rotate90(string s)
        {
            s = s.Replace("/", string.Empty);

            if (s.Length == 4)
            {
                // 0 1 => 2 0
                // 2 3 => 3 1
                return $"{s[2]}{s[0]}/{s[3]}{s[1]}";
            }
            else if (s.Length == 9)
            {
                // 0 1 2    6 3 0
                // 3 4 5 => 7 4 1
                // 6 7 8    8 5 2
                return $"{s[6]}{s[3]}{s[0]}/{s[7]}{s[4]}{s[1]}/{s[8]}{s[5]}{s[2]}";
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }

        private static string Rotate180(string s)
        {
            s = s.Replace("/", string.Empty);

            if (s.Length == 4)
            {
                // 0 1 => 3 2
                // 2 3 => 1 0
                return $"{s[3]}{s[2]}/{s[1]}{s[0]}";
            }
            else if (s.Length == 9)
            {
                // 0 1 2    8 7 6
                // 3 4 5 => 5 4 3
                // 6 7 8    2 1 0
                return $"{s[8]}{s[7]}{s[6]}/{s[5]}{s[4]}{s[3]}/{s[2]}{s[1]}{s[0]}";
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }

        private static string Rotate270(string s)
        {
            s = s.Replace("/", string.Empty);

            if (s.Length == 4)
            {
                // 0 1 => 1 3
                // 2 3 => 0 2
                return $"{s[1]}{s[3]}/{s[0]}{s[2]}";
            }
            else if (s.Length == 9)
            {
                // 0 1 2    2 5 8
                // 3 4 5 => 1 4 7
                // 6 7 8    0 3 6
                return $"{s[2]}{s[5]}{s[8]}/{s[1]}{s[4]}{s[7]}/{s[0]}{s[3]}{s[6]}";
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }

        private static string FlipVertically(string s)
        {
            s = s.Replace("/", string.Empty);

            if (s.Length == 4)
            {
                // 0 1 => 2 3
                // 2 3 => 0 1
                return $"{s[2]}{s[3]}/{s[0]}{s[1]}";
            }
            else if (s.Length == 9)
            {
                // 0 1 2    6 7 8
                // 3 4 5 => 3 4 5
                // 6 7 8    0 1 2
                return $"{s[6]}{s[7]}{s[8]}/{s[3]}{s[4]}{s[5]}/{s[0]}{s[1]}{s[2]}";
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }

        private static string FlipHorizontally(string s)
        {
            s = s.Replace("/", string.Empty);

            if (s.Length == 4)
            {
                // 0 1 => 1 0
                // 2 3 => 3 2
                return $"{s[1]}{s[0]}/{s[3]}{s[2]}";
            }
            else if (s.Length == 9)
            {
                // 0 1 2    2 1 0
                // 3 4 5 => 5 4 3
                // 6 7 8    8 7 6
                return $"{s[2]}{s[1]}{s[0]}/{s[5]}{s[4]}{s[3]}/{s[8]}{s[7]}{s[6]}";
            }
            else
            {
                throw new Exception("Invalid input");
            }
        }

        private static string[] RunIteration(string[] grid, Dictionary<string, string> rules)
        {
            var result = new List<string>();
            var subSquareSize = grid.Length % 2 == 0 ? 2 : 3;

            for (int y = 0; y < grid.Length; y += subSquareSize)
            {
                var resultRows = new string[subSquareSize + 1];

                for (int i = 0; i < resultRows.Length; i++)
                {
                    resultRows[i] = string.Empty;
                }

                for (int x = 0; x < grid.Length; x += subSquareSize)
                {
                    var rows = new List<string>();

                    for (int i = 0; i < subSquareSize; i++)
                    {
                        rows.Add(grid[y + i].Substring(x, subSquareSize));
                    }

                    var line = string.Join("/", rows);
                    var replacement = rules[line].Split("/".ToCharArray());

                    for (int i = 0; i < replacement.Length; i++)
                    {
                        resultRows[i] += replacement[i];
                    }
                }

                result.AddRange(resultRows);
            }

            return result.ToArray();
        }

        private static void PrintGrid(string[] grid)
        {
            foreach (var line in grid)
            {
                Console.WriteLine(line);
            }
        }
    }
}
