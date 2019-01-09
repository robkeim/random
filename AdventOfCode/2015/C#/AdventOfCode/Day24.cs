using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day24
    {
        const string input = "1\n2\n3\n7\n11\n13\n17\n19\n23\n31\n37\n41\n43\n47\n53\n59\n61\n67\n71\n73\n79\n83\n89\n97\n101\n103\n107\n109\n113";

        // Answer: 11846773891
        // Part 2 answer: 80393059
        public static void Part1()
        {
            var packages = input
                .Split("\n".ToCharArray())
                .Select(int.Parse)
                .ToArray();

            var weightPerBin = packages.Sum(p => p) / 3;

            var permutations = new List<List<int>>();

            // I got this idea to do this as a bitwise computation here: https://stackoverflow.com/a/3319597
            for (int num = 0; num < (1 << 30); num++)
            {
                var permutation = new List<int>();
                var sum = 0;

                for (int i = 0; i < 29; i++)
                {
                    if (((num >> i) & 1) == 1)
                    {
                        permutation.Add(packages[i]);
                        sum += packages[i];

                        if (sum > weightPerBin)
                        {
                            break;
                        }
                    }
                }

                if (sum == weightPerBin)
                {
                    permutations.Add(permutation);
                }
            }

            permutations = permutations
                .OrderBy(p => p.Count)
                .ToList();

            var minNumPackages = permutations.First().Count;

            permutations = permutations
                .Where(p => p.Count == minNumPackages)
                .ToList();

            var result = permutations
                .Select(p => GetQuantumEntanglement(p))
                .Min();

            Console.WriteLine(result);
        }

        private static long GetQuantumEntanglement(List<int> values)
        {
            var result = 1L;

            foreach (var value in values)
            {
                result *= value;
            }

            return result;
        }
    }
}
