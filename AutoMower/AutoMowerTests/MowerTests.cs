using MowerSimulator;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class MowerTests
    {
        [Test]
        public void GetFinalPosition_WithNoMoves_ReturnsOriginalPosition()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.North);

            var moves = new List<Move>();

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(initialPosition, result);
        }
        
        [Test]
        public void GetFinalPosition_MoveForwardFacingNorth_MovesOneSpace()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialOrientation = Orientation.North;
            var initialPosition = new Position(coordinate, initialOrientation);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(new Coordinate(1, 2), result.Coordinate);
            Assert.AreEqual(initialOrientation, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_MoveForwardFacingSouth_MovesOneSpace()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialOrientation = Orientation.South;
            var initialPosition = new Position(coordinate, initialOrientation);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(new Coordinate(1, 0), result.Coordinate);
            Assert.AreEqual(initialOrientation, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_MoveForwardFacingEast_MovesOneSpace()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialOrientation = Orientation.East;
            var initialPosition = new Position(coordinate, initialOrientation);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(new Coordinate(2, 1), result.Coordinate);
            Assert.AreEqual(initialOrientation, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_MoveForwardFacingWest_MovesOneSpace()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialOrientation = Orientation.West;
            var initialPosition = new Position(coordinate, initialOrientation);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(new Coordinate(0, 1), result.Coordinate);
            Assert.AreEqual(initialOrientation, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_MoveForwardAtBorderFacingNorth_ReturnsOriginalPosition()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 2);
            var initialPosition = new Position(coordinate, Orientation.North);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(initialPosition, result);
        }

        [Test]
        public void GetFinalPosition_MoveForwardAtBorderFacingSouth_ReturnsOriginalPosition()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 0);
            var initialPosition = new Position(coordinate, Orientation.South);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(initialPosition, result);
        }

        [Test]
        public void GetFinalPosition_MoveForwardAtBorderFacingEast_ReturnsOriginalPosition()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(2, 1);
            var initialPosition = new Position(coordinate, Orientation.East);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(initialPosition, result);
        }

        [Test]
        public void GetFinalPosition_MoveForwardAtBorderFacingWest_ReturnsOriginalPosition()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(0, 1);
            var initialPosition = new Position(coordinate, Orientation.West);

            var moves = new List<Move>
            {
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(initialPosition, result);
        }
        
        [Test]
        public void GetFinalPosition_TurnLeftFacingNorth_TurnsLeft()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.North);

            var moves = new List<Move>
            {
                Move.Left
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.West, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_TurnLeftFacingSouth_TurnsLeft()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.South);

            var moves = new List<Move>
            {
                Move.Left
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.East, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_TurnLeftFacingEast_TurnsLeft()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.East);

            var moves = new List<Move>
            {
                Move.Left
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.North, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_TurnLeftFacingWest_TurnsLeft()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.West);

            var moves = new List<Move>
            {
                Move.Left
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.South, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_TurnRightFacingNorth_TurnsRight()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.North);

            var moves = new List<Move>
            {
                Move.Right
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.East, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_TurnRightFacingSouth_TurnsRight()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.South);

            var moves = new List<Move>
            {
                Move.Right
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.West, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_TurnRightFacingEast_TurnsRight()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.East);

            var moves = new List<Move>
            {
                Move.Right
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.South, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_TurnRightFacingWest_TurnsRight()
        {
            // Arrange
            var maxLawnSize = new Coordinate(2, 2);
            var coordinate = new Coordinate(1, 1);
            var initialPosition = new Position(coordinate, Orientation.West);

            var moves = new List<Move>
            {
                Move.Right
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(coordinate, result.Coordinate);
            Assert.AreEqual(Orientation.North, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_MultipleLeftTurns_ReturnsCorrectPosition()
        {
            // Arrange
            var maxLawnSize = new Coordinate(5, 5);
            var coordinate = new Coordinate(1, 2);
            var initialPosition = new Position(coordinate, Orientation.North);

            var moves = new List<Move>
            {
                Move.Left,
                Move.Forward,
                Move.Left,
                Move.Forward,
                Move.Left,
                Move.Forward,
                Move.Left,
                Move.Forward,
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(new Coordinate(1, 3), result.Coordinate);
            Assert.AreEqual(Orientation.North, result.Orientation);
        }

        [Test]
        public void GetFinalPosition_MultipleRightTurns_ReturnsCorrectPosition()
        {
            // Arrange
            var maxLawnSize = new Coordinate(5, 5);
            var coordinate = new Coordinate(3, 3);
            var initialPosition = new Position(coordinate, Orientation.East);

            var moves = new List<Move>
            {
                Move.Forward,
                Move.Forward,
                Move.Right,
                Move.Forward,
                Move.Forward,
                Move.Right,
                Move.Forward,
                Move.Right,
                Move.Right,
                Move.Forward
            };

            var mower = new Mower(initialPosition, moves);

            // Act
            var result = mower.GetFinalPosition(maxLawnSize);

            // Assert
            Assert.AreEqual(new Coordinate(5, 1), result.Coordinate);
            Assert.AreEqual(Orientation.East, result.Orientation);
        }
    }
}