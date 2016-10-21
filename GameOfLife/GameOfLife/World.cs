using System;
using System.Collections.Generic;

namespace GameOfLife
{
    // TODO override GetHashCode
    public class World: IEquatable<World>
    {
        private readonly HashSet<Position> _liveCells = new HashSet<Position>();

        public void AddLiveCell(Position position)
        {
            if (!_liveCells.Contains(position))
            {
                _liveCells.Add(position);
            }
        }

        public bool Equals(World other)
        {
            return other != null && _liveCells.Count == other._liveCells.Count;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as World);
        }
    }
}
