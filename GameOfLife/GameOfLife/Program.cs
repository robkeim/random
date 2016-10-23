using System;
using System.Threading;

namespace GameOfLife
{
    // This is a solution to Conway's Game of Life:
    // https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life
    public class Program
    {
        public static void Main(string[] args)
        {
            var world = Utils.CreateWorld(
                ".X.",
                "..X",
                "XXX");

            while (true)
            {
                Console.Clear();
                Console.WriteLine(Utils.GetPrintableState(world));

                world = world.GetNextIteration();
                Thread.Sleep(TimeSpan.FromMilliseconds(250));
            }
        }
    }
}
