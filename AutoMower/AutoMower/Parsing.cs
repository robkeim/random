using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AutoMower
{
    public static class Parsing
    {
        // NOTE: For this exercise I chose to implement a very strict parsing under the
        // assumption that we have good control over the input file and can ensure it's valid.
        // Errors in parsing result in the program halting execution. I could have added
        // additional logic to try to parse incorrect input (trim whitespace, more flexibility
        // with whitespace tokenization, ignore invalid tokens, etc), but I felt the additional
        // complexity it would add to the solution was worth it without first understanding
        // more about how this program is going to be used.
        //
        // One disadvantage of this strict parsing is that one wrong configuration causes the
        // entire program to stop executing. Imagine a file with many well configured mowers,
        // and one incorrectly configured one. I could instead run only the well configured
        // mowers, but this may or may not be the desired behavior depending on how the program
        // is going to be used.
        public static Lawn ParseLawn(IEnumerable<string> lines)
        {
            lines = lines ?? throw new ArgumentNullException(nameof(lines));

            var enumerator = lines.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new ArgumentException(nameof(lines), "Missing top right coordinate for lawn");
            }

            var topRight = ParseTopRight(enumerator.Current);

            var mowers = new List<Mower>();

            while (enumerator.MoveNext())
            {
                var initialPosition = ParsePosition(enumerator.Current);

                if (!enumerator.MoveNext())
                {
                    throw new ArgumentException(nameof(lines), "Missing moves for mower");
                }

                var moves = ParseMoves(enumerator.Current);

                mowers.Add(new Mower(initialPosition, moves));
            }

            return new Lawn(topRight, mowers);
        }

        private static Coordinate ParseTopRight(string input)
        {
            var match = Regex.Match(input, @"^(\d+) (\d+)$", RegexOptions.Compiled);

            if (!match.Success)
            {
                throw new ArgumentException(nameof(input), $"Invalid format: {input}");
            }

            return new Coordinate(
                int.Parse(match.Groups[1].ToString()),
                int.Parse(match.Groups[2].ToString())
            );
        }

        public static Position ParsePosition(string input)
        {
            var match = Regex.Match(input, @"^(\d+) (\d+) ([NSEW])$", RegexOptions.Compiled);

            if (!match.Success)
            {
                throw new ArgumentException(nameof(input), $"Invalid format: {input}");
            }

            var coordinate = new Coordinate(
                int.Parse(match.Groups[1].ToString()),
                int.Parse(match.Groups[2].ToString())
            );

            return new Position(
                coordinate,
                ParseOrientation(match.Groups[3].ToString())
            );
        }

        private static Orientation ParseOrientation(string input)
        {
            switch (input)
            {
                case "N":
                    return Orientation.North;
                case "S":
                    return Orientation.South;
                case "E":
                    return Orientation.East;
                case "W":
                    return Orientation.West;
                default:
                    throw new ArgumentOutOfRangeException(nameof(input), $"Invalid value: {input}");
            };
        }

        public static IEnumerable<Move> ParseMoves(string input)
        {
            var match = Regex.Match(input, @"^[LRF]+$", RegexOptions.Compiled);

            if (!match.Success)
            {
                throw new ArgumentException(nameof(input), $"Invalid format: {input}");
            }

            return match.Groups[0].ToString().Select(m => ParseMove(m));
        }

        private static Move ParseMove(char input)
        {
            switch (input)
            {
                case 'L':
                    return Move.Left;
                case 'R':
                    return Move.Right;
                case 'F':
                    return Move.Forward;
                default:
                    throw new ArgumentOutOfRangeException(nameof(input), $"Invalid value: {input}");
            }
        }
    }
}
