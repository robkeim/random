using System;
using System.Collections.Generic;
using System.IO;

namespace MowerSimulator
{
    public static class AutoMower
    {
        // NOTE: The complexity of the overall algorithm is as follows:
        // Space: O(1) Only a constant amount of space is required in executing this
        //        algorithm because the input file is streamed line by line and not
        //        read all at once. Each mower is processed and then completed so no
        //        information needs to be stored about previous mowers once the output
        //        is written to the console.
        //
        // Time:  O(MOW * MOV) For each MOWer the algorithm iterates through all of the
        //        MOVes.
        public static void Run(string fileName)
        {
            var lawn = Parsing.ParseLawn(ReadFileLineByLine(fileName));

            foreach (var mower in lawn.Mowers)
            {
                var finalPosition = mower.GetFinalPosition(lawn.MaxSize);
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
