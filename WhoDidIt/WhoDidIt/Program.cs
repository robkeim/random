using System;

namespace WhoDidIt
{
    class Program
    {
        static void Main(string[] args)
        {
            var experimentAggregator = new ExperimentAggregator();

            experimentAggregator.AddExperimentRun(true, new[] { "HALO-123", "TEXT-1331" });
            experimentAggregator.AddExperimentRun(false, new[] { "FAM-123", "TEXT-2323" });
            experimentAggregator.AddExperimentRun(true, new[] { "FAM-123", "TEXT-1331" });
            experimentAggregator.AddExperimentRun(false, new[] { "HALO-123", "CMS-111" });

            experimentAggregator.PrintStatistics();

            Console.WriteLine("done!");
            Console.ReadLine();
        }
    }
}