using MowerSimulator;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class PresentationTests
    {
        [Test]
        public void PrintPosition_WhenFacingNorth_ReturnsN()
        {
            // Arrange
            var position = new Position(
                new Coordinate(0, 1),
                Orientation.North
            );

            // Act
            var result = Presentation.PrintPosition(position);

            // Assert
            Assert.AreEqual("0 1 N", result);
        }

        [Test]
        public void PrintPosition_WhenFacingSouth_ReturnsS()
        {
            // Arrange
            var position = new Position(
                new Coordinate(0, 1),
                Orientation.South
            );

            // Act
            var result = Presentation.PrintPosition(position);

            // Assert
            Assert.AreEqual("0 1 S", result);
        }

        [Test]
        public void PrintPosition_WhenFacingEast_ReturnsE()
        {
            // Arrange
            var position = new Position(
                new Coordinate(0, 1),
                Orientation.East
            );

            // Act
            var result = Presentation.PrintPosition(position);

            // Assert
            Assert.AreEqual("0 1 E", result);
        }

        [Test]
        public void PrintPosition_WhenFacingWest_ReturnsW()
        {
            // Arrange
            var position = new Position(
                new Coordinate(0, 1),
                Orientation.West
            );

            // Act
            var result = Presentation.PrintPosition(position);

            // Assert
            Assert.AreEqual("0 1 W", result);
        }
    }
}