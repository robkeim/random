using System;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class CellTests
    {
        [TestMethod]
        public void Dead_Cell_Without_Three_Live_Neighbors_Remains_Dead_In_Next_Iteration()
        {
            // Arrange
            var cell = new DeadCell();
            var expected = false;

            foreach (var numLiveNeighbors in new[] {0, 1, 2, 4, 5, 6, 7, 8})
            {
                // Act
                var actual = cell.LivesInNextIteration(numLiveNeighbors);

                // Assert
                Assert.AreEqual(expected, actual, $"Should remain dead for {numLiveNeighbors} live neighbors");
            }
        }

        [TestMethod]
        public void Dead_Cell_With_Three_Live_Neighbors_Remains_Live_In_Next_Iteration()
        {
            // Arrange
            var cell = new DeadCell();
            var expected = true;

            // Act
            var actual = cell.LivesInNextIteration(3);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Dead_Cell_With_Less_Than_Zero_Live_Neighbors_Throws_Exception()
        {
            // Arrange
            var cell = new DeadCell();

            // Act
            cell.LivesInNextIteration(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Dead_Cell_With_More_Than_Eight_Live_Neighbors_Throws_Exception()
        {
            // Arrange
            var cell = new DeadCell();

            // Act
            cell.LivesInNextIteration(9);
        }
        
        [TestMethod]
        public void Live_Cell_Without_Two_Or_Three_Live_Neighbors_Dies_In_Next_Iteration()
        {
            // Arrange
            var cell = new LiveCell();
            var expected = false;

            foreach (var numLiveNeighbors in new[] { 0, 1, 4, 5, 6, 7, 8 })
            {
                // Act
                var actual = cell.LivesInNextIteration(numLiveNeighbors);

                // Assert
                Assert.AreEqual(expected, actual, $"Should die for {numLiveNeighbors} live neighbors");
            }
        }
        
        [TestMethod]
        public void Live_Cell_With_Two_Or_Three_Live_Neighbors_Remains_Live_In_Next_Iteration()
        {
            // Arrange
            var cell = new LiveCell();
            var expected = true;

            foreach (var numLiveNeighbors in new[] { 2, 3 })
            {
                // Act
                var actual = cell.LivesInNextIteration(numLiveNeighbors);

                // Assert
                Assert.AreEqual(expected, actual, $"Should remain living for {numLiveNeighbors} live neighbors");
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Live_Cell_With_Less_Than_Zero_Live_Neighbors_Throws_Exception()
        {
            // Arrange
            var cell = new LiveCell();

            // Act
            cell.LivesInNextIteration(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Live_Cell_With_More_Than_Eight_Live_Neighbors_Throws_Exception()
        {
            // Arrange
            var cell = new LiveCell();

            // Act
            cell.LivesInNextIteration(9);
        }
    }
}
