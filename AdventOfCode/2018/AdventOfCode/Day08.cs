using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day08
    {
        public static int Part1(string input)
        {
            var numbers = input
                .Split(" ".ToCharArray())
                .Select(int.Parse)
                .ToList();

            return CountChecksums(numbers);
        }

        private static int CountChecksums(List<int> numbers)
        {
            var numChildren = numbers.First();
            numbers.RemoveAt(0);

            var numMetadata = numbers.First();
            numbers.RemoveAt(0);

            var result = 0;

            for (int i = 0; i < numChildren; i++)
            {
                result += CountChecksums(numbers);
            }

            for (int i = 0; i < numMetadata; i++)
            {
                result += numbers.First();
                numbers.RemoveAt(0);
            }

            return result;
        }

        public static int Part2(string input)
        {
            return -1;
        }
    }
}
