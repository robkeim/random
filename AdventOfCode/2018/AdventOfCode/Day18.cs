using System;

namespace AdventOfCode
{
    public static class Day18
    {
        public static int Part1(string input)
        {
            var lines = input.Split("\n".ToCharArray());
            var gridSize = lines.Length;

            var grid = new char[gridSize, gridSize];
            
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    grid[i, j] = lines[i][j];
                }
            }

            for (int iteration = 0; iteration < 10; iteration++)
            {
                var next = new char[gridSize, gridSize];

                for (int x = 0; x < gridSize; x++)
                {
                    for (int y = 0; y < gridSize; y++)
                    {
                        var numAdjacentTrees = 0;
                        var numAdjacentLumberyards = 0;

                        for (int dx = -1; dx <= 1; dx++)
                        {
                            for (int dy = -1; dy <= 1; dy++)
                            {
                                if ((dx == 0 && dy == 0)
                                    || x + dx < 0
                                    || y + dy < 0
                                    || x + dx >= gridSize
                                    || y + dy >= gridSize)
                                {
                                    continue;
                                }

                                switch (grid[x + dx, y + dy])
                                {
                                    case '|':
                                        numAdjacentTrees++;
                                        break;
                                    case '#':
                                        numAdjacentLumberyards++;
                                        break;
                                }
                            }
                        }

                        char nextValue;

                        switch (grid[x, y])
                        {
                            case '.':
                                nextValue = numAdjacentTrees >= 3 ? '|' : '.';
                                break;
                            case '|':
                                nextValue = numAdjacentLumberyards >= 3 ? '#' : '|';
                                break;
                            case '#':
                                nextValue = numAdjacentLumberyards >= 1 && numAdjacentTrees >= 1 ? '#' : '.';
                                break;
                            default:
                                throw new Exception("Invalid character");
                        }

                        next[x, y] = nextValue;
                    }
                }

                grid = next;
            }

            var numTrees = 0;
            var numLumberyards = 0;

            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (grid[i, j] == '|')
                    {
                        numTrees++;
                    }
                    else if (grid[i, j] == '#')
                    {
                        numLumberyards++;
                    }
                }
            }

            return numTrees * numLumberyards;
        }

        public static int Part2(string input)
        {
            return 0;
        }
    }
}
