using System;
using System.Diagnostics;
using System.IO;

namespace WhoDidIt
{
    class Program
    {
        static void Main(string[] args)
        {
            if (Debugger.IsAttached)
            {
                args = new[] { "./sampleInput.txt" };
            }

            if (args.Length == 0 || args.Length > 1)
            {
                Console.WriteLine("Usage: WhoDidIt.exe <input file>");
                Console.WriteLine();
                Console.WriteLine("Input file lines have the following format:");
                Console.WriteLine("true|false userId");
                Console.WriteLine("ex: true 7b8db7a3-d7a1-4a94-82b3-4693a89045ae");
                return;
            }

            var inputFile = args[0];

            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"Cannot find file: {inputFile}. Please provide a valid file");
                return;
            }

            var experimentAggregator = new ExperimentAggregator();

            foreach (var line in File.ReadAllLines(inputFile))
            {
                var splitLine = line.Split(" \t".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                bool bugReproduced;

                if (splitLine.Length != 2 || !bool.TryParse(splitLine[0], out bugReproduced))
                {
                    Console.WriteLine($"Line not correct formatted, could not determine if the bug reproduced: {line}");
                    return;
                }

                var experiments = ExperimentParser.GetBExperimentsForUserId(splitLine[1]);

                Console.WriteLine($"UserId: {splitLine[1]}\n{string.Join(" ", experiments)}\n");
                experimentAggregator.AddExperimentRun(bugReproduced, experiments);
            }

            experimentAggregator.PrintStatistics();
            
            if (Debugger.IsAttached)
            {
                Console.WriteLine("done!");
                Console.ReadLine();
            }
        }
    }
}