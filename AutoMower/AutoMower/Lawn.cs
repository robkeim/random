using System.Collections.Generic;

namespace AutoMower
{
    public class Lawn
    {
        public Coordinate TopRight { get; }

        public IEnumerable<Mower> Mowers { get; }

        public Lawn(Coordinate topRight, IEnumerable<Mower> mowers)
        {
            TopRight = topRight;
            Mowers = mowers;
        }
    }
}
