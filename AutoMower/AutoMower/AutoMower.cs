using System;
using System.Collections.Generic;
using System.IO;

namespace AutoMower
{
    public static class AutoMower
    {
        // NOTE: The complexity of the overall algorithm is as follows:
        // Space: O(1) Only a constant amount of space is required in executing this
        //        algorithm because the input file is streamed line by line and not
        //        read all at once. Each mower is processed and then completed so no
        //        information needs to be stored about previous mowers once the result
        //        for a given mower is written to the console.
        //
        // Time:  O(MOW * MOV) For each MOWer the algorithm iterates through all of the
        //        MOVes in order to compute the final position of the mower.
        public static void Run(string fileName)
        {
            var lawn = Parsing.ParseLawn(ReadFileLineByLine(fileName));

            // NOTE: This could be sped up by calculating the final mower positions in parallel
            // since calculating a mower's final position is independent of the other mowers. The
            // only tricky part would be to ensure synchronization around displaying the final position
            // results to keep the original order of the mowers.
            foreach (var mower in lawn.Mowers)
            {
                var finalPosition = mower.GetFinalPosition(lawn.TopRight);
                Console.WriteLine(Presentation.PrintPosition(finalPosition));
            }
        }

        private static IEnumerable<string> ReadFileLineByLine(string file)
        {
            string line;
            using (var reader = File.OpenText(file))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
