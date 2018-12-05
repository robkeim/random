using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day05
    {
        public static int Part1(string input)
        {
            var matchFound = true;
            var result = input
                .ToCharArray()
                .Select(c => c.ToString())
                .ToList();

            while (matchFound)
            {
                matchFound = false;

                for (int i = 0; i < result.Count - 1; i++)
                {
                    if (result[i].ToLowerInvariant() == result[i + 1].ToLowerInvariant()
                        && result[i] != result[i + 1])
                    {
                        matchFound = true;
                        result.RemoveAt(i);
                        result.RemoveAt(i);
                    }
                }
            }

            return result.Count;
        }

        public static int Part2(string input)
        {
            var minLength = input.Length;

            for (var i = 'A'; i <= 'Z'; i++)
            {
                var charsToReplace = $"[{i}{i.ToString().ToLowerInvariant()}]";
                var removedString = Regex.Replace(input, charsToReplace, string.Empty);
                var length = Part1(removedString);

                if (length < minLength)
                {
                    minLength = length;
                }
            }

            return minLength;
        }
    }
}
