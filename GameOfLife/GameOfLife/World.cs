using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class World: IEquatable<World>
    {
        private readonly HashSet<Position> _liveCells = new HashSet<Position>();

        private int minX = int.MaxValue;
        private int maxX = int.MinValue;
        private int minY = int.MaxValue;
        private int maxY = int.MinValue;

        public void AddLiveCell(Position position)
        {
            if (!_liveCells.Contains(position))
            {
                _liveCells.Add(position);

                minX = Math.Min(minX, position.X);
                maxX = Math.Max(maxX, position.X);
                minY = Math.Min(minY, position.Y);
                maxY = Math.Max(maxY, position.Y);
            }
        }

        public Cell[,] GetState()
        {
            if (_liveCells.Count == 0)
            {
                return new Cell[0, 0];
            }

            int xSize = maxX - minX + 1;
            int ySize = maxY - minY + 1;

            var result = new Cell[ySize, xSize];

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    var position = new Position(minX + i, minY + j);
                    
                    result[j, i] = _liveCells.Contains(position)
                        ? (Cell)new LiveCell()
                        : new DeadCell();
                }
            }

            return result;
        }

        public bool Equals(World other)
        {
            return other != null
                && _liveCells.Count == other._liveCells.Count
                && _liveCells.All(p => other._liveCells.Contains(p));
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as World);
        }

        public override int GetHashCode()
        {
            return _liveCells?.GetHashCode() ?? 0;
        }
    }
}
