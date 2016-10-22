using System;
using System.Diagnostics;

namespace GameOfLife
{
    [DebuggerDisplay("({X}, {Y})")]
    public class Position : IEquatable<Position>
    {
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }

        public int Y { get; }

        public Position[] GetNeighbors()
        {
            return new[]
            {
                new Position(X - 1, Y - 1), 
                new Position(X - 1, Y), 
                new Position(X - 1, Y + 1), 
                new Position(X, Y - 1), 
                new Position(X, Y + 1), 
                new Position(X + 1, Y - 1), 
                new Position(X + 1, Y), 
                new Position(X + 1, Y + 1),
            };
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Position);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public bool Equals(Position other)
        {
            return other != null && X == other.X && Y == other.Y;
        }
    }
}
