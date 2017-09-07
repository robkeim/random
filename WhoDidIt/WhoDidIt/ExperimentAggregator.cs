using System;
using System.Collections.Generic;
using System.Linq;

namespace WhoDidIt
{
    public class ExperimentAggregator
    {
        Dictionary<string, int> _experiments = new Dictionary<string, int>();

        public void AddExperimentRun(bool bugReproduced, IEnumerable<string> experimentsToAdd)
        {
            foreach (var experiment in experimentsToAdd)
            {
                if (!_experiments.ContainsKey(experiment))
                {
                    _experiments[experiment] = bugReproduced ? 1 : -1;
                }
                else
                {
                    if (bugReproduced)
                    {
                        _experiments[experiment]++;
                    }
                    else
                    {
                        _experiments[experiment]--;
                    }
                }
            }
        }

        public void PrintStatistics()
        {
            var entries = _experiments.GroupBy(exp => exp.Value)
                  .ToDictionary(grp => grp.Key, g => g.Select(kvp => kvp.Key).OrderBy(v => v).ToArray())
                  .OrderByDescending(kvp => kvp.Key)
                  .ToArray();
            
            Console.WriteLine("Final summary:");

            foreach (var entry in entries)
            {
                Console.WriteLine($"{entry.Key}\t{string.Join(", ", entry.Value)}");
            }
        }
    }
}
