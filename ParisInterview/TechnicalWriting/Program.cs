using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalWriting
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Maze maze = new Maze();

            maze.Initialize();
            maze.Print();

            FindCheeseRecursive findCheeseRecursive = new FindCheeseRecursive();
            findCheeseRecursive.FindCheese(maze);

            FindCheeseIterative findCheeseIterative = new FindCheeseIterative();
            findCheeseIterative.FindCheese(maze);

            Console.ReadLine();
        }
    }
}
