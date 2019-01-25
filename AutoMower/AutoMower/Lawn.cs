using System.Collections.Generic;

namespace MowerSimulator
{
    public class Lawn
    {
        public Coordinate MaxSize { get; }

        public IEnumerable<Mower> Mowers { get; }

        public Lawn(Coordinate maxSize, IEnumerable<Mower> mowers)
        {
            MaxSize = maxSize;
            Mowers = mowers;
        }
    }
}
