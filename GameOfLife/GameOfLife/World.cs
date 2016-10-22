using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class World: IEquatable<World>
    {
        private readonly HashSet<Position> _liveCells = new HashSet<Position>();

        private int _minX = int.MaxValue;
        private int _maxX = int.MinValue;
        private int _minY = int.MaxValue;
        private int _maxY = int.MinValue;

        public void AddLiveCell(Position position)
        {
            if (!_liveCells.Contains(position))
            {
                _liveCells.Add(position);

                _minX = Math.Min(_minX, position.X);
                _maxX = Math.Max(_maxX, position.X);
                _minY = Math.Min(_minY, position.Y);
                _maxY = Math.Max(_maxY, position.Y);
            }
        }

        public Cell[,] GetState()
        {
            if (_liveCells.Count == 0)
            {
                return new Cell[0, 0];
            }

            int xSize = _maxX - _minX + 1;
            int ySize = _maxY - _minY + 1;

            var result = new Cell[ySize, xSize];

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    var position = new Position(_minX + i, _minY + j);
                    
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
