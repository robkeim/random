using System.Collections.Generic;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void Two_Positions_With_The_Same_Reference_Are_Equal()
        {
            // Arrange
            var position = new Position(0, 0);

            // Act
            // Assert
            Assert.AreEqual(position, position);
        }

        [TestMethod]
        public void Two_Positions_With_Different_References_Are_Equal()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(0, 0);

            // Act
            // Assert
            Assert.AreEqual(position1, position2);
        }

        [TestMethod]
        public void Two_Different_Positions_Are_Not_Equal()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(0, 1);

            // Act
            // Assert
            Assert.AreNotEqual(position1, position2);
        }

        [TestMethod]
        public void Get_Neighbors_Returns_Eight_Results()
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
        public void Get_Neighbors_Returns_Correct_Positions()
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
