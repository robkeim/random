using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/1
    //
    // --- Day 1: No Time for a Taxicab ---
    // 
    // Santa's sleigh uses a very high-precision clock to guide its movements, and the clock's oscillator is regulated by stars.Unfortunately, the stars have been
    // stolen...by the Easter Bunny.To save Christmas, Santa needs you to retrieve all fifty stars by December 25th.
    // 
    // Collect stars by solving puzzles.Two puzzles will be made available on each day in the advent calendar; the second puzzle is unlocked when you complete the
    // first.Each puzzle grants one star. Good luck!
    //
    // You're airdropped near Easter Bunny Headquarters in a city somewhere. "Near", unfortunately, is as close as you can get - the instructions on the Easter
    // Bunny Recruiting Document the Elves intercepted start here, and nobody had time to work them out further.
    //
    // The Document indicates that you should start at the given coordinates (where you just landed) and face North.Then, follow the provided sequence: either turn
    // left(L) or right(R) 90 degrees, then walk forward the given number of blocks, ending at a new intersection.
    //
    // There's no time to follow such ridiculous instructions on foot, though, so you take a moment and work out the destination. Given that you can only walk on
    // the street grid of the city, how far is the shortest path to the destination?
    //
    // For example:
    //
    // Following R2, L3 leaves you 2 blocks East and 3 blocks North, or 5 blocks away.
    // R2, R2, R2 leaves you 2 blocks due South of your starting position, which is 2 blocks away.
    // R5, L5, R5, R3 leaves you 12 blocks away.
    // How many blocks away is Easter Bunny HQ?
    // 
    // R3, L2, L2, R4, L1, R2, R3, R4, L2, R4, L2, L5, L1, R5, R2, R2, L1, R4, R1, L5, L3, R4, R3, R1, L1, L5, L4, L2, R5, L3, L4, R3, R1, L3, R1, L3, R3, L4, R2, R5, L190, R2, L3, R47, R4, L3, R78, L1, R3, R190, R4, L3, R4, R2, R5, R3, R4, R3, L1, L4, R3, L4, R1, L4, L5, R3, L3, L4, R1, R2, L4, L3, R3, R3, L2, L5, R1, L4, L1, R5, L5, R1, R5, L4, R2, L2, R1, L5, L4, R4, R4, R3, R2, R3, L1, R4, R5, L2, L5, L4, L1, R4, L4, R4, L4, R1, R5, L1, R1, L5, R5, R1, R1, L3, L1, R4, L1, L4, L4, L3, R1, R4, R1, R1, R2, L5, L2, R4, L1, R3, L5, L2, R5, L4, R5, L5, R3, R4, L3, L3, L2, R2, L5, L5, R3, R4, R3, R4, R3, R1
    // Answer: 262
    //
    // --- Part Two ---
    //
    // Then, you notice the instructions continue on the back of the Recruiting Document.Easter Bunny HQ is actually at the first location you visit twice.
    //
    // For example, if your instructions are R8, R4, R4, R8, the first location you visit twice is 4 blocks away, due East.
    //
    // How many blocks away is the first location you visit twice?
    // Answer: 131
    public static class Day1
    {
        public static int FindDistanceFromHQ(string rawInput)
        {
            var xPos = 0;
            var yPos = 0;
            var heading = Heading.North;

            foreach (var rawDir in rawInput.Split(",".ToCharArray()).Select(ri => ri.Trim()))
            {
                var direction = rawDir[0];
                var distance = int.Parse(rawDir.Substring(1));

                heading = heading.GetNewHeading(direction);

                switch (heading)
                {
                    case Heading.North:
                        yPos += distance;
                        break;
                    case Heading.East:
                        xPos += distance;
                        break;
                    case Heading.South:
                        yPos -= distance;
                        break;
                    case Heading.West:
                        xPos -= distance;
                        break;
                }
            }

            return Math.Abs(xPos) + Math.Abs(yPos);
        }

        public static int FindFirstIntersection(string rawInput)
        {
            var xPos = 0;
            var yPos = 0;
            var heading = Heading.North;
            var visitedPositions = new HashSet<string>();
            var foundIntersection = false;

            foreach (var rawDir in rawInput.Split(",".ToCharArray()).Select(ri => ri.Trim()))
            {
                var direction = rawDir[0];
                var distance = int.Parse(rawDir.Substring(1));

                heading = heading.GetNewHeading(direction);

                for (int i = 0; i < distance; i++)
                {
                    switch (heading)
                    {
                        case Heading.North:
                            yPos++;
                            break;
                        case Heading.East:
                            xPos++;
                            break;
                        case Heading.South:
                            yPos--;
                            break;
                        case Heading.West:
                            xPos--;
                            break;
                    }

                    var curPos = $"{xPos}_{yPos}";
                    if (!visitedPositions.Contains(curPos))
                    {
                        visitedPositions.Add(curPos);
                    }
                    else
                    {
                        foundIntersection = true;
                        break;
                    }
                }

                if (foundIntersection)
                {
                    break;
                }
            }

            return Math.Abs(xPos) + Math.Abs(yPos);
        }

        private static Heading GetNewHeading(this Heading current, char direction)
        {
            var offset = direction == 'R' ? 1 : -1;
            return (Heading)(((int)current + offset + 4) % 4);
        }

        private enum Heading
        {
            North,
            East,
            South,
            West
        }
    }
}
