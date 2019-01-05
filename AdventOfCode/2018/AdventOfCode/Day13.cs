using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public static class Day13
    {
        public static string Part1(string input)
        {
            var lines = input.Split("@".ToCharArray());

            var map = new Dictionary<string, char>();
            var carts = new List<Cart>();

            // Parse map
            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    var character = lines[y][x];
                    var coord = $"{x}_{y}";
                    var cart = new Cart
                    {
                        X = x,
                        Y = y,
                        Last = Turn.Right
                    };

                    switch (character)
                    {
                        case '<':
                            cart.Facing = Direction.Left;
                            carts.Add(cart);
                            map[coord] = '-';
                            break;
                        case '>':
                            cart.Facing = Direction.Right;
                            carts.Add(cart);
                            map[coord] = '-';
                            break;
                        case '^':
                            cart.Facing = Direction.Up;
                            carts.Add(cart);
                            map[coord] = '|';
                            break;
                        case 'v':
                            cart.Facing = Direction.Down;
                            carts.Add(cart);
                            map[coord] = '|';
                            break;
                        default:
                            map[coord] = character;
                            break;
                    }
                }
            }

            // Run simulation
            while (true)
            {
                Print(map, carts);

                carts = carts
                    .OrderBy(c => c.X)
                    .ThenBy(c => c.Y)
                    .ToList();

                var nextCarts = new List<Cart>();

                // Iterate through the carts and update the position and check for collisions
                foreach (var cart in carts)
                {
                    var next = new Cart
                    {
                        X = cart.X,
                        Y = cart.Y,
                        Facing = cart.Facing,
                        Last = cart.Last
                    };

                    switch (map[$"{cart.X}_{cart.Y}"])
                    {
                        case '-':
                            switch (cart.Facing)
                            {
                                case Direction.Left:
                                    next.X--;
                                    break;
                                case Direction.Right:
                                    next.X++;
                                    break;
                                default:
                                    throw new Exception("Invalid direction");
                            }
                            break;
                        case '|':
                            switch (cart.Facing)
                            {
                                case Direction.Up:
                                    next.Y--;
                                    break;
                                case Direction.Down:
                                    next.Y++;
                                    break;
                                default:
                                    throw new Exception("Invalid direction");
                            }
                            break;
                        case '\\':
                            switch (cart.Facing)
                            {
                                case Direction.Up:
                                    next.Facing = Direction.Left;
                                    next.X--;
                                    break;
                                case Direction.Down:
                                    next.Facing = Direction.Right;
                                    next.X++;
                                    break;
                                case Direction.Left:
                                    next.Facing = Direction.Up;
                                    next.Y--;
                                    break;
                                case Direction.Right:
                                    next.Facing = Direction.Down;
                                    next.Y++;
                                    break;
                                default:
                                    throw new Exception("Invalid direction");
                            }
                            break;
                        case '/':
                            switch (cart.Facing)
                            {
                                case Direction.Up:
                                    next.Facing = Direction.Right;
                                    next.X++;
                                    break;
                                case Direction.Down:
                                    next.Facing = Direction.Left;
                                    next.X--;
                                    break;
                                case Direction.Left:
                                    next.Facing = Direction.Down;
                                    next.Y++;
                                    break;
                                case Direction.Right:
                                    next.Facing = Direction.Up;
                                    next.Y--;
                                    break;
                                default:
                                    throw new Exception("Invalid direction");
                            }
                            break;
                        case '+':
                            switch (cart.Last)
                            {
                                case Turn.Straight:
                                    // Turn right
                                    next.Last = Turn.Right;
                                    switch (cart.Facing)
                                    {
                                        case Direction.Up:
                                            next.Facing = Direction.Right;
                                            next.X++;
                                            break;
                                        case Direction.Down:
                                            next.Facing = Direction.Left;
                                            next.X--;
                                            break;
                                        case Direction.Left:
                                            next.Facing = Direction.Up;
                                            next.Y--;
                                            break;
                                        case Direction.Right:
                                            next.Facing = Direction.Down;
                                            next.Y++;
                                            break;
                                        default:
                                            throw new Exception("Invalid direction");
                                    }
                                    break;
                                case Turn.Left:
                                    // Go straight
                                    next.Last = Turn.Straight;
                                    switch (cart.Facing)
                                    {
                                        case Direction.Up:
                                            next.Y--;
                                            break;
                                        case Direction.Down:
                                            next.Y++;
                                            break;
                                        case Direction.Left:
                                            next.X--;
                                            break;
                                        case Direction.Right:
                                            next.X++;
                                            break;
                                        default:
                                            throw new Exception("Invalid direction");
                                    }
                                    break;
                                case Turn.Right:
                                    // Turn left
                                    next.Last = Turn.Left;
                                    switch (cart.Facing)
                                    {
                                        case Direction.Up:
                                            next.Facing = Direction.Left;
                                            next.X--;
                                            break;
                                        case Direction.Down:
                                            next.Facing = Direction.Right;
                                            next.X++;
                                            break;
                                        case Direction.Left:
                                            next.Facing = Direction.Down;
                                            next.Y++;
                                            break;
                                        case Direction.Right:
                                            next.Facing = Direction.Up;
                                            next.Y--;
                                            break;
                                        default:
                                            throw new Exception("Invalid direction");
                                    }
                                    break;
                                default:
                                    throw new Exception("Invalid direction");
                            }
                            break;
                        default:
                            throw new Exception("Invalid path character");
                    }

                    if (carts.Any(c => c.X == next.X && c.Y == next.Y)
                        || nextCarts.Any(c => c.X == next.X && c.Y == next.Y))
                    {
                        return $"{next.X},{next.Y}";
                    }

                    nextCarts.Add(next);
                }

                carts = nextCarts;
            }
        }

        private class Cart
        {
            public int X { get; set; }
            public int Y { get; set; }
            public Direction Facing { get; set; }
            public Turn Last { get; set; }
        }

        private enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        private enum Turn
        {
            Left,
            Straight,
            Right
        }

        private static void Print(Dictionary<string, char> map, List<Cart> carts)
        {
            Console.WriteLine();

            for (int y = 0; y <= 5; y++)
            {
                for (int x = 0; x <= 12; x++)
                {
                    var cart = carts.FirstOrDefault(c => c.X == x && c.Y == y);
                    if (cart != null)
                    {
                        switch (cart.Facing)
                        {
                            case Direction.Up:
                                Console.Write('^');
                                break;
                            case Direction.Down:
                                Console.Write('v');
                                break;
                            case Direction.Left:
                                Console.Write('<');
                                break;
                            case Direction.Right:
                                Console.Write('>');
                                break;
                            default:
                                throw new Exception("Invalid direction");
                        }
                    }
                    else
                    {
                        Console.Write(map[$"{x}_{y}"]);
                    }
                }

                Console.WriteLine();
            }
        }

        public static int Part2(string input)
        {
            return -1;
        }
    }
}
