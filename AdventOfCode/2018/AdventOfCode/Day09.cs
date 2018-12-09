using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public static class Day09
    {
        public static int Part1(string input)
        {
            var regex = new Regex(@"(\d+) players; last marble is worth (\d+) points", RegexOptions.Compiled);

            var match = regex.Match(input);
            var numPlayers = int.Parse(match.Groups[1].ToString());
            var lastMarble = int.Parse(match.Groups[2].ToString());

            var curMarbleIndex = 0;
            var curPlayerIndex = 0;
            var marbles = new List<int>();
            marbles.Insert(0, 0);
            var scores = new int[numPlayers];

            for (int curMarble = 1; curMarble <= lastMarble; curMarble++)
            {
                if (curMarble % 23 != 0)
                {
                    curMarbleIndex = (curMarbleIndex + 2) % marbles.Count;
                    marbles.Insert(curMarbleIndex + 1, curMarble);
                }
                else
                {
                    scores[curPlayerIndex] += curMarble;
                    curMarbleIndex = (curMarbleIndex - 6 + marbles.Count) % marbles.Count;
                    scores[curPlayerIndex] += marbles[curMarbleIndex];
                    marbles.RemoveAt(curMarbleIndex);
                    curMarbleIndex--;
                }

                curPlayerIndex = (curPlayerIndex + 1) % numPlayers;
            }

            return scores
                .OrderByDescending(s => s)
                .First();
        }

        public static int Part2(string input)
        {
            return Part1(input);
        }
    }
}
