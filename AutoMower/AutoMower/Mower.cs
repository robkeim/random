using System;
using System.Collections.Generic;

namespace AutoMower
{
    public class Mower
    {
        private Position _initialPosition;
        private IEnumerable<Move> _moves;

        public Mower(Position initialPosition, IEnumerable<Move> moves)
        {
            _initialPosition = initialPosition;
            _moves = moves;
        }

        public Position GetFinalPosition(Coordinate topRightOfLawn)
        {
            var position = _initialPosition;

            foreach (var move in _moves)
            {
                switch (move)
                {
                    case Move.Left:
                        position = MoveLeft(position);
                        break;
                    case Move.Right:
                        position = MoveRight(position);
                        break;
                    case Move.Forward:
                        position = MoveForward(position, topRightOfLawn);
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid move: {move}");
                }
            }
            
            return position;
        }

        private Position MoveLeft(Position position)
        {
            Orientation newOrientation;

            switch (position.Orientation)
            {
                case Orientation.North:
                    newOrientation = Orientation.West;
                    break;
                case Orientation.South:
                    newOrientation = Orientation.East;
                    break;
                case Orientation.East:
                    newOrientation = Orientation.North;
                    break;
                case Orientation.West:
                    newOrientation = Orientation.South;
                    break;
                default:
                    throw new InvalidOperationException($"Invalid orientation: {position.Orientation}");
            }

            return new Position(position.Coordinate, newOrientation);
        }

        private Position MoveRight(Position position)
        {
            Orientation newOrientation;

            switch (position.Orientation)
            {
                case Orientation.North:
                    newOrientation = Orientation.East;
                    break;
                case Orientation.South:
                    newOrientation = Orientation.West;
                    break;
                case Orientation.East:
                    newOrientation = Orientation.South;
                    break;
                case Orientation.West:
                    newOrientation = Orientation.North;
                    break;
                default:
                    throw new InvalidOperationException($"Invalid orientation: {position.Orientation}");
            }

            return new Position(position.Coordinate, newOrientation);
        }

        private Position MoveForward(Position position, Coordinate topRightOfLawn)
        {
            var newX = position.Coordinate.X;
            var newY = position.Coordinate.Y;

            switch (position.Orientation)
            {
                case Orientation.North:
                    if (position.Coordinate.Y + 1 <= topRightOfLawn.Y)
                    {
                        newY++;
                    }
                    break;
                case Orientation.South:
                    if (position.Coordinate.Y - 1 >= 0)
                    {
                        newY--;
                    }
                    break;
                case Orientation.East:
                    if (position.Coordinate.X + 1 <= topRightOfLawn.X)
                    {
                        newX++;
                    }
                    break;
                case Orientation.West:
                    if (position.Coordinate.X - 1 >= 0)
                    {
                        newX--;
                    }
                    break;
                default:
                    throw new InvalidOperationException($"Invalid orientation: {position.Orientation}");
            }

            return new Position(
                new Coordinate(newX, newY),
                position.Orientation);
        }
    }
}
