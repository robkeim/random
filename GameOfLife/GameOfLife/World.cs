using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    public class World: IEquatable<World>
    {
        public World()
        {
        }

        private World(int minX, int maxX, int minY, int maxY)
        {
            _minX = minX;
            _maxX = maxX;
            _minY = minY;
            _maxY = maxY;
        }

        private readonly HashSet<Position> _liveCells = new HashSet<Position>();

        private int _minX = int.MaxValue;
        private int _maxX = int.MinValue;
        private int _minY = int.MaxValue;
        private int _maxY = int.MinValue;

        public void AddLiveCell(Position position)
        {
            AddCell(position, new LiveCell());
        }

        public void AddCell(Position position, Cell cell)
        {
            if (!_liveCells.Contains(position))
            {
                if (cell.GetType() == typeof(LiveCell))
                {
                    _liveCells.Add(position);
                }
                
                _minX = Math.Min(_minX, position.X);
                _maxX = Math.Max(_maxX, position.X);
                _minY = Math.Min(_minY, position.Y);
                _maxY = Math.Max(_maxY, position.Y);
            }
        }

        public Cell[,] GetCurrentState()
        {
            if (_minX == int.MaxValue && _maxX == int.MinValue && _minY == int.MaxValue && _maxY == int.MinValue)
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

        public World GetNextIteration()
        {
            var positionsToCheck = new HashSet<Position>();

            foreach (var liveCell in _liveCells)
            {
                positionsToCheck.Add(liveCell);

                foreach (var neighbor in liveCell.GetNeighbors())
                {
                    positionsToCheck.Add(neighbor);
                }
            }

            var result = new World(_minX, _maxX, _minY, _maxY);

            foreach (var position in positionsToCheck)
            {
                var cellAtPosition = _liveCells.Contains(position)
                    ? (Cell)new LiveCell()
                    : new DeadCell();

                if (cellAtPosition.LivesInNextIteration(GetLiveNeighborsForPosition(position)))
                {
                    result.AddLiveCell(position);
                }
            }

            return result;
        }

        private int GetLiveNeighborsForPosition(Position position)
        {
            return position.GetNeighbors().Count(n => _liveCells.Contains(n));
        }

        public bool Equals(World other)
        {
            return other != null
                && _liveCells.Count == other._liveCells.Count
                && _liveCells.All(p => other._liveCells.Contains(p))
                && _minX == other._minX
                && _maxX == other._maxX
                && _minY == other._minY
                && _maxY == other._maxY;
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
