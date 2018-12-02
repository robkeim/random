using System;
using System.Linq;

namespace AdventOfCode
{
    public static class Day02
    {
        public static int Part1(string input)
        {
            var lines = input.Split("\n".ToCharArray());

            var numDouble = 0;
            var numTriple = 0;

            foreach (var line in lines)
            {
                var sorted = line.GroupBy(c => c).ToArray();

                if (sorted.Any(g => g.Count() == 2))
                {
                    numDouble++;
                }

                if (sorted.Any(g => g.Count() == 3))
                {
                    numTriple++;
                }
            }

            return numDouble * numTriple;
        }

        public static string Part2(string input)
        {
            var lines = input.Split("\n".ToCharArray());

            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    if (HammingDistance(lines[i], lines[j]) == 1)
                    {
                        var result = string.Empty;

                        for (int k = 0; k < lines[i].Length; k++)
                        {
                            if (lines[i][k] == lines[j][k])
                            {
                                result += lines[i][k];
                            }
                        }

                        return result;
                    }
                }
            }

            throw new Exception("No result found");
        }

        private static int HammingDistance(string first, string second)
        {
            return first
                .Zip(second, (f, s) => f != s)
                .Count(v => v == true);
        }
    }
}
