using System;

namespace AdventOfCode
{
    public static class Day25
    {
        // Answer: 8997277
        public static void Part1()
        {
            var maxRow = 1;
            var curRow = 1;
            var curCol = 1;
            var result = 20151125L;
            var targetRow = 3010;
            var targetCol = 3019;

            while (curRow != targetRow || curCol != targetCol)
            {
                while (curRow >= 1)
                {
                    if (curRow == targetRow && curCol == targetCol)
                    {
                        break;
                    }

                    result = (result * 252533) % 33554393;

                    curRow--;
                    curCol++;
                }

                if (curRow == targetRow && curCol == targetCol)
                {
                    break;
                }

                maxRow++;
                curRow = maxRow;
                curCol = 1;
            }

            Console.WriteLine(result);
        }
    }
}
