using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class WorldTests
    {
        [TestMethod]
        public void Two_Worlds_With_The_Same_Reference_Are_Equal()
        {
            // Arrange
            var world = new World();

            // Act
            // Assert
            Assert.AreEqual(world, world);
        }

        [TestMethod]
        public void Two_Worlds_With_Different_References_Are_Equal()
        {
            // Arrange
            var world1 = new World();
            var world2 = new World();

            // Act
            // Assert
            Assert.AreEqual(world1, world2);
        }
    }
}
