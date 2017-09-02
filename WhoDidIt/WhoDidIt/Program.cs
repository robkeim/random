using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

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
                Console.WriteLine("true|false <whitespace delimited list of experiments");
                Console.WriteLine("ex: true HALO-123 TEXT-456 FAM-789");
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
                var splitLine = line.Split(" \t".ToCharArray());
                bool bugReproduced;

                if (!bool.TryParse(splitLine[0], out bugReproduced))
                {
                    Console.WriteLine($"Line not correct formatted, could not determine if the bug reproduced: {line}");
                    return;
                }

                experimentAggregator.AddExperimentRun(bugReproduced, splitLine.Skip(1));
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