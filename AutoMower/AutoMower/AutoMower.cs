using System;
using System.IO;

namespace MowerSimulator
{
    public static class AutoMower
    {
        // NOTE: The complexity of the overall algorithm is as follows:
        // Space: O(N) where N is the number of mowers
        // TODO rkeim: finish complexity
        public static void Run(string fileName)
        {
            // NOTE: This method reads all of the lines in the input file and stores them in
            // memory. This would be problematic for *very* large input files where all of the
            // mowers could not be stored in memory. To get around this limitation the input
            // file could be opened as a stream and read one mower at a time.
            var lines = File.ReadAllLines(fileName);

            var lawn = Parsing.ParseLawn(lines);

            foreach (var mower in lawn.Mowers)
            {
                var finalPosition = mower.GetFinalPosition(lawn.MaxSize);
                Console.WriteLine(Presentation.PrintPosition(finalPosition));
            }
        }
    }
}
