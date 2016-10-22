using System;

namespace GameOfLife
{
    public abstract class Cell: IEquatable<Cell>
    {
        public abstract bool LivesInNextIteration(int numAliveNeighbors);
        public abstract bool Equals(Cell other);
    }
}
