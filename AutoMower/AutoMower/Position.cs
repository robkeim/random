using System;

namespace AutoMower
{
    public class Position : IEquatable<Position>
    {
        public Coordinate Coordinate { get; }

        public Orientation Orientation { get; }

        public Position(Coordinate coordinate, Orientation orientation)
        {
            Coordinate = coordinate;
            Orientation = orientation;
        }

        public bool Equals(Position other) => Coordinate.Equals(other.Coordinate) && Orientation == other.Orientation;
    }
}
