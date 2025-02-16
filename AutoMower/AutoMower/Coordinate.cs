﻿using System.Diagnostics;

namespace AutoMower
{
    [DebuggerDisplay("({X}, {Y})")]
    public struct Coordinate
    {
        public int X { get; }

        public int Y { get; }

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
