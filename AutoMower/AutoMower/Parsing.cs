using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MowerSimulator
{
    public static class Parsing
    {
        // TODO rkeim: explain choice of strict parsing
        // TODO rkeim: talk about possibility to skip invalid mowers instead of halting the program
        public static Lawn ParseLawn(IEnumerable<string> lines)
        {
            lines = lines ?? throw new ArgumentNullException(nameof(lines));

            var enumerator = lines.GetEnumerator();

            if (!enumerator.MoveNext())
            {
                throw new ArgumentException(nameof(lines), "Missing max size for lawn");
            }

            var maxSize = ParseSize(enumerator.Current);

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

            return new Lawn(maxSize, mowers);
        }

        private static Coordinate ParseSize(string input)
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
