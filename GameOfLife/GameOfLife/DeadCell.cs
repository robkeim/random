using System;

namespace GameOfLife
{
    public class DeadCell : Cell
    {
        public override bool LivesInNextIteration(int numAliveNeighbors)
        {
            if (numAliveNeighbors < 0 || numAliveNeighbors > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(numAliveNeighbors));
            }

            return numAliveNeighbors == 3;
        }

        public override bool Equals(Cell other)
        {
            return other is DeadCell;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Cell);
        }
    }
}
