using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class CellEqualityTests
    {
        [TestMethod]
        public void Two_Live_Cells_With_The_Same_Reference_Are_Equal()
        {
            // Arrange
            var cell = new LiveCell();

            // Act
            // Assert
            Assert.AreEqual(cell, cell);
        }

        [TestMethod]
        public void Two_Live_Cells_With_Different_References_Are_Equal()
        {
            // Arrange
            var cell1 = new LiveCell();
            var cell2 = new LiveCell();

            // Act
            // Assert
            Assert.AreEqual(cell1, cell2);
        }

        [TestMethod]
        public void Two_Dead_Cells_With_The_Same_Reference_Are_Equal()
        {
            // Arrange
            var cell = new DeadCell();

            // Act
            // Assert
            Assert.AreEqual(cell, cell);
        }

        [TestMethod]
        public void Two_Dead_Cells_With_Different_References_Are_Equal()
        {
            // Arrange
            var cell1 = new DeadCell();
            var cell2 = new DeadCell();

            // Act
            // Assert
            Assert.AreEqual(cell1, cell2);
        }

        [TestMethod]
        public void Live_And_Dead_Cells_Are_Not_Equal()
        {
            // Arrange
            var liveCell = new LiveCell();
            var deadCell = new DeadCell();

            // Act
            // Assert
            Assert.AreNotEqual(liveCell, deadCell);
        }
    }
}
