using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Day01
    {
        public static int Part1(string input)
        {
            var result = 0;

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                result += int.Parse(line);
            }

            return result;
        }

        public static int Part2(string input)
        {
            var result = 0;
            var seen = new HashSet<int>();
            var index = 0;
            var lines = input.Split("\n".ToCharArray());

            while (true)
            {
                result += int.Parse(lines[index]);

                if (!seen.Add(result))
                {
                    return result;
                }

                index++;
                index %= lines.Length;
            }
        }
    }
}
