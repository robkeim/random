using System.Collections.Generic;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void TwoPositionsWithTheSameReferenceAreEqual()
        {
            // Arrange
            var position = new Position(0, 0);

            // Act
            // Assert
            Assert.AreEqual(position, position);
        }

        [TestMethod]
        public void TwoPositionsWithDifferentReferencesAreEqual()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(0, 0);

            // Act
            // Assert
            Assert.AreEqual(position1, position2);
        }

        [TestMethod]
        public void TwoDifferentPositionsAreNotEqual()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(0, 1);

            // Act
            // Assert
            Assert.AreNotEqual(position1, position2);
        }

        [TestMethod]
        public void GetNeighborsReturnsEightResults()
        {
            // Arrange
            var position = new Position(0, 0);
            var expected = 8;

            // Act
            var actual = position.GetNeighbors();

            // Assert
            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void GetNeighborsReturnsCorrectPositions()
        {
            // Arrange
            var position = new Position(0, 0);
            var expected = new List<Position>
            {
                new Position(-1, -1),
                new Position(-1, 0),
                new Position(-1, 1),
                new Position(0, -1),
                new Position(0, 1),
                new Position(1, -1),
                new Position(1, 0),
                new Position(1, 1),
            };

            // Act
            var actual = position.GetNeighbors();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}
