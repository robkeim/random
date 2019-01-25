using MowerSimulator;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    public class ParsingTests
    {
        [Test]
        public void ParsePosition_WithValidCoordinate_ReturnsPosition()
        {
            // Arrange
            var input = "0 1 N";

            // Act
            var result = Parsing.ParsePosition(input);

            // Assert
            Assert.AreEqual(new Coordinate(0, 1), result.Coordinate);
        }

        [Test]
        public void ParsePosition_WithMultidigitCoordinates_ReturnsPosition()
        {
            // Arrange
            var input = "99 100 N";

            // Act
            var result = Parsing.ParsePosition(input);

            // Assert
            Assert.AreEqual(new Coordinate(99, 100), result.Coordinate);
        }

        [Test]
        public void ParsePosition_WithOrientationN_ReturnsNorth()
        {
            // Arrange
            var input = "0 0 N";

            // Act
            var result = Parsing.ParsePosition(input);

            // Assert
            Assert.AreEqual(Orientation.North, result.Orientation);
        }

        [Test]
        public void ParsePosition_WithOrientationS_ReturnsSouth()
        {
            // Arrange
            var input = "0 0 S";

            // Act
            var result = Parsing.ParsePosition(input);

            // Assert
            Assert.AreEqual(Orientation.South, result.Orientation);
        }

        [Test]
        public void ParsePosition_WithOrientationE_ReturnsEast()
        {
            // Arrange
            var input = "0 0 E";

            // Act
            var result = Parsing.ParsePosition(input);

            // Assert
            Assert.AreEqual(Orientation.East, result.Orientation);
        }

        [Test]
        public void ParsePosition_WithOrientationW_ReturnsWest()
        {
            // Arrange
            var input = "0 0 W";

            // Act
            var result = Parsing.ParsePosition(input);

            // Assert
            Assert.AreEqual(Orientation.West, result.Orientation);
        }

        [Test]
        public void ParsePosition_WithInvalidFormat_ThrowsArgumentException()
        {
            // Arrange
            var input = "0 0 X";

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Parsing.ParsePosition(input));
        }

        [Test]
        public void ParseMoves_WithMoveL_ReturnsLeft()
        {
            // Arrange
            var moves = "L";

            // Act
            var result = Parsing.ParseMoves(moves);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Move.Left, result.First());
        }

        [Test]
        public void ParseMoves_WithMoveR_ReturnsRight()
        {
            // Arrange
            var moves = "R";

            // Act
            var result = Parsing.ParseMoves(moves);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Move.Right, result.First());
        }

        [Test]
        public void ParseMoves_WithMoveF_ReturnsForward()
        {
            // Arrange
            var moves = "F";

            // Act
            var result = Parsing.ParseMoves(moves);

            // Assert
            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(Move.Forward, result.First());
        }

        [Test]
        public void ParseMoves_WithMultipleMoves_ReturnsMoves()
        {
            // Arrange
            var moves = "LRF";

            // Act
            var result = Parsing.ParseMoves(moves);

            // Assert
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(Move.Left, result.First());
            Assert.AreEqual(Move.Right, result.Skip(1).First());
            Assert.AreEqual(Move.Forward, result.Skip(2).First());
        }

        [Test]
        public void ParseMoves_WithInvalidFormat_ThrowsArgumentException()
        {
            // Arrange
            var input = "X";

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Parsing.ParseMoves(input));
        }

        [Test]
        public void ParseLawn_WithOnlyLawnSize_ReturnsLawn()
        {
            // Arrange
            var input = new[]
            {
                "0 1"
            };

            // Act
            var result = Parsing.ParseLawn(input);

            // Assert
            Assert.AreEqual(new Coordinate(0, 1), result.MaxSize);
            Assert.AreEqual(0, result.Mowers.Count());
        }

        [Test]
        public void ParseLawn_WithOneMower_ReturnsLawn()
        {
            // Arrange
            var input = new[]
            {
                "2 2",
                "1 1 N",
                "F"
            };

            // Act
            var result = Parsing.ParseLawn(input);

            // Assert
            Assert.AreEqual(1, result.Mowers.Count());
        }

        [Test]
        public void ParseLawn_WithTwoMowers_ReturnsLawn()
        {
            // Arrange
            var input = new[]
            {
                "2 2",
                "1 1 N",
                "F",
                "1 1 S",
                "L"
            };

            // Act
            var result = Parsing.ParseLawn(input);

            // Assert
            Assert.AreEqual(2, result.Mowers.Count());
        }
        
        [Test]
        public void ParseLawn_WithNullInput_ThrowsArgumentNullException()
        {
            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => Parsing.ParseLawn(null));
        }

        [Test]
        public void ParseLawn_WithEmptyInput_ThrowsArgumentException()
        {
            // Arrange
            var input = new string[0];

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Parsing.ParseLawn(input));
        }

        [Test]
        public void ParseLawn_WithMowerHalfDefined_ThrowsArgumentException()
        {
            // Arrange
            var input = new[]
            {
                "1 1",
                "0 0 N"
            };

            // Act
            // Assert
            Assert.Throws<ArgumentException>(() => Parsing.ParseLawn(input));
        }
    }
}