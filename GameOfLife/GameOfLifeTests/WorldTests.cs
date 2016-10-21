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

        [TestMethod]
        public void World_With_One_Cell_And_Empty_World_Are_Not_Equal()
        {
            // Arrange
            var world1 = new World();
            var world2 = new World();
            world2.AddLiveCell(new Position(0, 0));

            // Act
            // Assert
            Assert.AreNotEqual(world1, world2);
        }
    }
}
