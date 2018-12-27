using System.Collections.Generic;

namespace AdventOfCode
{
    public static class Day22
    {
        public static int Part1(string input)
        {
            var split = input.Split("\n".ToCharArray());
            var depth = int.Parse(split[0]);
            var targetX = int.Parse(split[1]);
            var targetY = int.Parse(split[2]);

            var erosionLevels = new Dictionary<string, int>();
            var geologicIndexes = new Dictionary<string, int>();

            for (int x = 0; x <= targetX; x++)
            {
                for (int y = 0; y <= targetY; y++)
                {
                    var coord = $"{x}_{y}";

                    if ((x == 0 && y == 0) || (x == targetX && y == targetY))
                    {
                        geologicIndexes[coord] = 0;
                    }
                    else if (y == 0)
                    {
                        geologicIndexes[coord] = x * 16807;
                    }
                    else if (x == 0)
                    {
                        geologicIndexes[coord] = y * 48271;
                    }
                    else
                    {
                        geologicIndexes[coord] = erosionLevels[$"{x - 1}_{y}"] * erosionLevels[$"{x}_{y - 1}"];
                    }

                    erosionLevels[coord] = (geologicIndexes[coord] + depth) % 20183;
                }
            }

            var result = 0;

            for (int x = 0; x <= targetX; x++)
            {
                for (int y = 0; y <= targetY; y++)
                {
                    result += erosionLevels[$"{x}_{y}"] % 3;
                }
            }

            return result;
        }

        public static int Part2(string input)
        {
            return -1;
        }
    }
}
