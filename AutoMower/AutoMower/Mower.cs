using System;
using System.Collections.Generic;

namespace MowerSimulator
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

        public Position GetFinalPosition(Coordinate maxLawnSize)
        {
            var position = _initialPosition;

            foreach (var move in _moves)
            {
                switch (move)
                {
                    case Move.Left:
                        position = MoveLeft(position, maxLawnSize);
                        break;
                    case Move.Right:
                        position = MoveRight(position, maxLawnSize);
                        break;
                    case Move.Forward:
                        position = MoveForward(position, maxLawnSize);
                        break;
                    default:
                        throw new InvalidOperationException($"Invalid move: {move}");
                }
            }
            
            return position;
        }

        private Position MoveLeft(Position position, Coordinate maxLawnSize)
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

        private Position MoveRight(Position position, Coordinate maxLawnSize)
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

        private Position MoveForward(Position position, Coordinate maxLawnSize)
        {
            var newX = position.Coordinate.X;
            var newY = position.Coordinate.Y;

            switch (position.Orientation)
            {
                case Orientation.North:
                    if (position.Coordinate.Y + 1 <= maxLawnSize.Y)
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
                    if (position.Coordinate.X + 1 <= maxLawnSize.X)
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
