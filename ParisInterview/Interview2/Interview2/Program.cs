namespace Interview2
{
    using System;

    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("T = " + RegexMatch.IsMatch("a", "a"));
            Console.WriteLine("F = " + RegexMatch.IsMatch("a", "b"));

            Console.WriteLine("T = " + RegexMatch.IsMatch("?", "a"));
            Console.WriteLine("F = " + RegexMatch.IsMatch("?", ""));

            Console.WriteLine("T = " + RegexMatch.IsMatch("a?b*c", "adbc"));
            Console.WriteLine("T = " + RegexMatch.IsMatch("a?b*c", "adbblablac"));
            Console.WriteLine("F = " + RegexMatch.IsMatch("a?b*c", "adbdca"));
            Console.WriteLine("F = " + RegexMatch.IsMatch("a?b*c", "abdca"));

            int[,] matrix = new[,]
                                {
                                    { 0,  1,  3,  5},
                                    { 1,  2,  4,  7},
                                    { 3,  6,  7,  8},
                                    { 6,  7,  8,  9},
                                    {10, 11, 12, 13}
                                };

            //// TODO (rkeim): Figure out bug in binary search logic where there is an out of bounds exception
            //// when the element is larger than the largest element in the array
            Console.WriteLine("T = " + SortedMatrix.FindValue(matrix, 22));

            Console.ReadLine();
        }
    }
}