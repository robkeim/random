using System;

namespace GameOfLife
{
    // TODO override GetHashCode
    public class World: IEquatable<World>
    {
        public bool Equals(World other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as World);
        }
    }
}
