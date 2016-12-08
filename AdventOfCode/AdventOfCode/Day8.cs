using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    // http://adventofcode.com/2016/day/8
    //
    //     --- Day 8: Two-Factor Authentication ---
    //
    // You come across a door implementing what you can only assume is an implementation of two-factor authentication after a long game of requirements telephone.
    //
    // To get past the door, you first swipe a keycard(no problem; there was one on a nearby desk). Then, it displays a code on a little screen, and you type that
    // code on a keypad.Then, presumably, the door unlocks.
    //
    // Unfortunately, the screen has been smashed.After a few minutes, you've taken everything apart and figured out how it works. Now you just have to work out
    // what the screen would have displayed.
    //
    // The magnetic strip on the card you swiped encodes a series of instructions for the screen; these instructions are your puzzle input.The screen is 50 pixels
    // wide and 6 pixels tall, all of which start off, and is capable of three somewhat peculiar operations:
    // - rect AxB turns on all of the pixels in a rectangle at the top-left of the screen which is A wide and B tall.
    // - rotate row y= A by B shifts all of the pixels in row A (0 is the top row) right by B pixels.Pixels that would fall off the right end appear at the left
    //   end of the row.
    // - rotate column x= A by B shifts all of the pixels in column A (0 is the left column) down by B pixels.Pixels that would fall off the bottom appear at the
    //   top of the column.
    //
    // For example, here is a simple sequence on a smaller screen:
    // rect 3x2 creates a small rectangle in the top-left corner:
    // ###....
    // ###....
    // .......
    //
    // rotate column x= 1 by 1 rotates the second column down by one pixel:
    // #.#....
    // ###....
    // .#.....
    //
    // rotate row y= 0 by 4 rotates the top row right by four pixels:
    // ....#.#
    // ###....
    // .#.....
    //
    // rotate column x= 1 by 1 again rotates the second column down by one pixel, causing the bottom pixel to wrap back to the top:
    // .#..#.#
    // #.#....
    // .#.....
    //
    // As you can see, this display technology is extremely powerful, and will soon dominate the tiny-code-displaying-screen market. That's what the
    // advertisement on the back of the display tries to convince you, anyway.
    //
    // There seems to be an intermediate check of the voltage used by the display: after you swipe your card, if the screen did work, how many pixels should be lit?
    // rect 1x1\nrotate row y=0 by 5\nrect 1x1\nrotate row y=0 by 5\nrect 1x1\nrotate row y=0 by 3\nrect 1x1\nrotate row y=0 by 2\nrect 1x1\nrotate row y=0 by 3\nrect 1x1\nrotate row y=0 by 2\nrect 1x1\nrotate row y=0 by 5\nrect 1x1\nrotate row y=0 by 5\nrect 1x1\nrotate row y=0 by 3\nrect 1x1\nrotate row y=0 by 2\nrect 1x1\nrotate row y=0 by 3\nrect 2x1\nrotate row y=0 by 2\nrect 1x2\nrotate row y=1 by 5\nrotate row y=0 by 3\nrect 1x2\nrotate column x=30 by 1\nrotate column x=25 by 1\nrotate column x=10 by 1\nrotate row y=1 by 5\nrotate row y=0 by 2\nrect 1x2\nrotate row y=0 by 5\nrotate column x=0 by 1\nrect 4x1\nrotate row y=2 by 18\nrotate row y=0 by 5\nrotate column x=0 by 1\nrect 3x1\nrotate row y=2 by 12\nrotate row y=0 by 5\nrotate column x=0 by 1\nrect 4x1\nrotate column x=20 by 1\nrotate row y=2 by 5\nrotate row y=0 by 5\nrotate column x=0 by 1\nrect 4x1\nrotate row y=2 by 15\nrotate row y=0 by 15\nrotate column x=10 by 1\nrotate column x=5 by 1\nrotate column x=0 by 1\nrect 14x1\nrotate column x=37 by 1\nrotate column x=23 by 1\nrotate column x=7 by 2\nrotate row y=3 by 20\nrotate row y=0 by 5\nrotate column x=0 by 1\nrect 4x1\nrotate row y=3 by 5\nrotate row y=2 by 2\nrotate row y=1 by 4\nrotate row y=0 by 4\nrect 1x4\nrotate column x=35 by 3\nrotate column x=18 by 3\nrotate column x=13 by 3\nrotate row y=3 by 5\nrotate row y=2 by 3\nrotate row y=1 by 1\nrotate row y=0 by 1\nrect 1x5\nrotate row y=4 by 20\nrotate row y=3 by 10\nrotate row y=2 by 13\nrotate row y=0 by 10\nrotate column x=5 by 1\nrotate column x=3 by 3\nrotate column x=2 by 1\nrotate column x=1 by 1\nrotate column x=0 by 1\nrect 9x1\nrotate row y=4 by 10\nrotate row y=3 by 10\nrotate row y=1 by 10\nrotate row y=0 by 10\nrotate column x=7 by 2\nrotate column x=5 by 1\nrotate column x=2 by 1\nrotate column x=1 by 1\nrotate column x=0 by 1\nrect 9x1\nrotate row y=4 by 20\nrotate row y=3 by 12\nrotate row y=1 by 15\nrotate row y=0 by 10\nrotate column x=8 by 2\nrotate column x=7 by 1\nrotate column x=6 by 2\nrotate column x=5 by 1\nrotate column x=3 by 1\nrotate column x=2 by 1\nrotate column x=1 by 1\nrotate column x=0 by 1\nrect 9x1\nrotate column x=46 by 2\nrotate column x=43 by 2\nrotate column x=24 by 2\nrotate column x=14 by 3\nrotate row y=5 by 15\nrotate row y=4 by 10\nrotate row y=3 by 3\nrotate row y=2 by 37\nrotate row y=1 by 10\nrotate row y=0 by 5\nrotate column x=0 by 3\nrect 3x3\nrotate row y=5 by 15\nrotate row y=3 by 10\nrotate row y=2 by 10\nrotate row y=0 by 10\nrotate column x=7 by 3\nrotate column x=6 by 3\nrotate column x=5 by 1\nrotate column x=3 by 1\nrotate column x=2 by 1\nrotate column x=1 by 1\nrotate column x=0 by 1\nrect 9x1\nrotate column x=19 by 1\nrotate column x=10 by 3\nrotate column x=5 by 4\nrotate row y=5 by 5\nrotate row y=4 by 5\nrotate row y=3 by 40\nrotate row y=2 by 35\nrotate row y=1 by 15\nrotate row y=0 by 30\nrotate column x=48 by 4\nrotate column x=47 by 3\nrotate column x=46 by 3\nrotate column x=45 by 1\nrotate column x=43 by 1\nrotate column x=42 by 5\nrotate column x=41 by 5\nrotate column x=40 by 1\nrotate column x=33 by 2\nrotate column x=32 by 3\nrotate column x=31 by 2\nrotate column x=28 by 1\nrotate column x=27 by 5\nrotate column x=26 by 5\nrotate column x=25 by 1\nrotate column x=23 by 5\nrotate column x=22 by 5\nrotate column x=21 by 5\nrotate column x=18 by 5\nrotate column x=17 by 5\nrotate column x=16 by 5\nrotate column x=13 by 5\nrotate column x=12 by 5\nrotate column x=11 by 5\nrotate column x=3 by 1\nrotate column x=2 by 5\nrotate column x=1 by 5\nrotate column x=0 by 1
    // Answer: 106
    //
    //     --- Part Two ---
    //
    // You notice that the screen is only capable of displaying capital letters; in the font it uses, each letter is 5 pixels wide and 6 tall.
    //
    // After you swipe your card, what code is the screen trying to display?
    // Answer: CFLELOYFCS
    public static class Day8
    {
        public static int CountPixels(string input)
        {
            var screen = ComputeScreen(input);

            return screen.GetNumLitPixles();
        }

        public static void DisplayScreen(string input)
        {
            var screen = ComputeScreen(input);
            Console.WriteLine(screen);
        }

        private static Screen ComputeScreen(string input)
        {
            var screen = new Screen();
            var rectRegex = new Regex(@"rect (\d+)x(\d+)", RegexOptions.Compiled);
            var rotateRegex = new Regex(@"rotate (row|column) [xy]=(\d+) by (\d+)");

            foreach (var line in input.Split("\n".ToCharArray()))
            {
                var rectMatch = rectRegex.Match(line);
                var rotateMatch = rotateRegex.Match(line);

                if (rectMatch.Success)
                {
                    var width = int.Parse(rectMatch.Groups[1].ToString());
                    var height = int.Parse(rectMatch.Groups[2].ToString());
                    screen.AddRectange(width, height);
                }
                else if (rotateMatch.Success)
                {
                    var rowOrCol = rotateMatch.Groups[1].ToString();
                    var rowOrColNum = int.Parse(rotateMatch.Groups[2].ToString());
                    var by = int.Parse(rotateMatch.Groups[3].ToString());

                    if (rowOrCol == "row")
                    {
                        screen.RotateRow(rowOrColNum, @by);
                    }
                    else if (rowOrCol == "column")
                    {
                        screen.RotateColumn(rowOrColNum, @by);
                    }
                    else
                    {
                        throw new InvalidOperationException($"Not a valid rotation type: {line}");
                    }
                }
                else
                {
                    throw new InvalidOperationException($"Not a valid move: {line}");
                }
            }
            return screen;
        }
    }
    
    public class Screen
    {
        private readonly int _width;
        private readonly int _height;
        private readonly bool[,] _screen;

        public Screen(int width = 50, int height = 6)
        {
            _width = width;
            _height = height;
            _screen = new bool[height, width];
        }

        public void AddRectange(int width, int height)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    _screen[i, j] = true;
                }
            }
        }

        public void RotateRow(int row, int by)
        {
            var curRow = new bool[_width];

            for (int i = 0; i < _width; i++)
            {
                curRow[i] = _screen[row, i];
            }

            for (int i = 0; i < _width; i++)
            {
                _screen[row, (i + by) % _width] = curRow[i];
            }
        }

        public void RotateColumn(int col, int by)
        {
            var curCol = new bool[_height];

            for (int i = 0; i < _height; i++)
            {
                curCol[i] = _screen[i, col];
            }

            for (int i = 0; i < _height; i++)
            {
                _screen[(i + by) % _height, col] = curCol[i];
            }
        }

        public int GetNumLitPixles()
        {
            return _screen
                .Cast<bool>()
                .Count(value => value);
        }

        public override string ToString()
        {
            var result = new StringBuilder();

            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    result.Append(_screen[i, j] ? "#" : ".");
                }

                result.Append("\n");
            }

            return result.ToString();
        }
    }
}
