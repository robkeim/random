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
                var value = int.Parse(line.Substring(1));

                if (line[0] == '+')
                {
                    result += value;
                }
                else
                {
                    result -= value;
                }
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
                var value = int.Parse(lines[index].Substring(1));

                if (lines[index][0] == '+')
                {
                    result += value;
                }
                else
                {
                    result -= value;
                }

                if (seen.Contains(result))
                {
                    return result;
                }

                seen.Add(result);

                index++;
                index %= lines.Length;
            }
        }
    }
}
