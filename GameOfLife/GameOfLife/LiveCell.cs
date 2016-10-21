using System;

namespace GameOfLife
{
    public class LiveCell : Cell
    {
        public override bool LivesInNextIteration(int numAliveNeighbors)
        {
            if (numAliveNeighbors < 0 || numAliveNeighbors > 8)
            {
                throw new ArgumentOutOfRangeException(nameof(numAliveNeighbors));
            }

            return numAliveNeighbors == 2 || numAliveNeighbors == 3;
        }
    }
}
