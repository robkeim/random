using System;
using System.Diagnostics;

namespace AutoMower
{
    [DebuggerDisplay("({X}, {Y})")]
    public class Coordinate : IEquatable<Coordinate>
    {
        public int X { get; }

        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coordinate other) => X == other.X && Y == other.Y;
    }
}
