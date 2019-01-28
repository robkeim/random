namespace AutoMower
{
    public struct Position
    {
        public Coordinate Coordinate { get; }

        public Orientation Orientation { get; }

        public Position(Coordinate coordinate, Orientation orientation)
        {
            Coordinate = coordinate;
            Orientation = orientation;
        }
    }
}
