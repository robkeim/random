using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void TwoPositionsSameReferenceAreEqual()
        {
            // Arrange
            var position = new Position(0, 0);

            // Act
            // Assert
            Assert.AreEqual(position, position);
        }

        [TestMethod]
        public void TwoPositionsDifferentReferencesAreEqual()
        {
            // Arrange
            var position1 = new Position(0, 0);
            var position2 = new Position(0, 0);

            // Act
            // Assert
            Assert.AreEqual(position1, position2);
        }
    }
}
