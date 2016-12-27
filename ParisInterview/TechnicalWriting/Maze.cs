using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TechnicalWriting
{
    public class Maze : IMaze
    {
        private MazeSquare[,] maze;
        // The x and y position of the mouse
        private int xPosition;
        private int yPosition;

        public int MazeSize { get; set; }

        public void Print()
        {
            for (int i = this.MazeSize - 1; i >= 0; i--)
            {
                for (int j = 0; j < this.MazeSize; j++)
                {
                    char cur;

                    if (j == xPosition && i == yPosition)
                    {
                        cur = 'M';
                    }
                    else if (maze[j, i] == MazeSquare.Cheese)
                    {
                        cur = 'C';
                    }
                    else if (maze[j, i] == MazeSquare.Empty)
                    {
                        cur = '.';
                    }
                    else if (maze[j, i] == MazeSquare.Wall)
                    {
                        cur = '#';
                    }
                    else
                    {
                        throw new ArgumentException("Unknown MazeSquare type encountered");
                    }

                    Console.Write("{0}", cur);
                }

                Console.WriteLine();
            }

            Console.WriteLine();
        }

        public void Initialize()
        {
            this.MazeSize = new Random().Next(1, 100);
            maze = new MazeSquare[this.MazeSize, this.MazeSize];
            Random random = new Random();

            for (int i = 0; i < this.MazeSize; i++)
            {
                for (int j = 0; j < this.MazeSize; j++)
                {
                    // This is the percentage of squares that will be walls
                    maze[i, j] = (random.Next(100) < 20) ? MazeSquare.Wall : MazeSquare.Empty;
                }
            }

            this.xPosition = random.Next(this.MazeSize);
            this.yPosition = random.Next(this.MazeSize);
            maze[this.xPosition, this.yPosition] = MazeSquare.Empty;

            maze[random.Next(this.MazeSize), random.Next(this.MazeSize)] = MazeSquare.Cheese;
        }

        public bool Move(Direction tryMovingMouseInThisDirection)
        {
            int xPositionNew = this.xPosition;
            int yPositionNew = this.yPosition;

            switch (tryMovingMouseInThisDirection)
            {
                case Direction.Left:
                    xPositionNew--;
                    break;
                case Direction.Down:
                    yPositionNew--;
                    break;
                case Direction.Right:
                    xPositionNew++;
                    break;
                case Direction.Up:
                    yPositionNew++;
                    break;
                default:
                    throw new ArgumentException("Unknown direction specified");
                    break;
            }

            if (this.IsLocationInBounds(xPositionNew, yPositionNew) && maze[xPositionNew, yPositionNew] != MazeSquare.Wall)
            {
                this.xPosition = xPositionNew;
                this.yPosition = yPositionNew;

                return true;
            }

            return false;
        }

        public bool Success()
        {
            return maze[this.xPosition, this.yPosition] == MazeSquare.Cheese;
        }

        private bool IsLocationInBounds(int xPosition, int yPosition)
        {
            return xPosition >= 0 && xPosition < this.MazeSize &&
                yPosition >= 0 && yPosition < this.MazeSize;
        }

        private enum MazeSquare
        {
            Empty,
            Wall,
            Cheese
        }
    }

    public interface IMaze
    {
        // Will create a session and a maze.  The maze will be no larger than 100 by 100 units in size.  The mouse and a piece of cheese will be positioned at random locations within the maze.
        void Initialize();

        // Will attempt to move the mouse in one direction.  If the move was successful, returns true.  If there was a wall and the move failed, returns false.
        bool Move(Direction tryMovingMouseInThisDirection);

        // Will return true if the mouse and cheese are at the same location, false otherwise.
        bool Success();
    }

    public enum Direction
    {
        Left = 1, // move left (x-1)
        Down = 2, // move down (y-1)
        Right = 3, // move right (x+1)
        Up = 4  // move up (y+1)
    }
}
