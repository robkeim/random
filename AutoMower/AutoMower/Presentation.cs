using System;

namespace MowerSimulator
{
    public static class Presentation
    {
        public static string PrintPosition(Position position)
        {
            return $"{position.Coordinate.X} {position.Coordinate.Y} {PrintOrientation(position.Orientation)}";
        }

        private static char PrintOrientation(Orientation orientation)
        {
            switch (orientation)
            {
                case Orientation.North:
                    return 'N';
                case Orientation.South:
                    return 'S';
                case Orientation.East:
                    return 'E';
                case Orientation.West:
                    return 'W';
                default:
                    throw new ArgumentOutOfRangeException(nameof(orientation), $"Invalid value: {orientation}");
            }
        }
    }
}
