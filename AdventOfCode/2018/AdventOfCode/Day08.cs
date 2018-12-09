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
            var numbers = input
                .Split(" ".ToCharArray())
                .Select(int.Parse)
                .ToList();
            
            return Score(numbers);
        }

        private static int Score(List<int> numbers)
        {
            var numChildren = numbers.First();
            numbers.RemoveAt(0);

            var numMetadata = numbers.First();
            numbers.RemoveAt(0);

            var childrenScores = new int[numChildren];
            
            for (int i = 0; i < numChildren; i++)
            {
                childrenScores[i] = Score(numbers);
            }

            var metadata = new int[numMetadata];

            for (int i = 0; i < numMetadata; i++)
            {
                metadata[i] = numbers.First();
                numbers.RemoveAt(0);
            }

            var result = 0;

            foreach (var value in metadata)
            {
                if (numChildren != 0)
                {
                    if (value != 0 && value <= numChildren)
                    {
                        result += childrenScores[value - 1];
                    }
                }
                else
                {
                    result += value;
                }
            }
            
            return result;
        }
    }
}
