namespace GameOfLife
{
    public abstract class Cell
    {
        public abstract bool LivesInNextIteration(int numAliveNeighbors);
    }
}
