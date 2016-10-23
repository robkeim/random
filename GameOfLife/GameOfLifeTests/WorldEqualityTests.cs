using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class WorldEqualityTests
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
            var world2 = Utils.CreateWorld(
                "X");

            // Act
            // Assert
            Assert.AreNotEqual(world1, world2);
        }

        [TestMethod]
        public void Worlds_With_One_Cell_But_Different_Positions_Are_Not_Equal()
        {
            // Arrange
            var world1 = Utils.CreateWorld(
                ".X");
            var world2 = Utils.CreateWorld(
                "X");

            // Act
            // Assert
            Assert.AreNotEqual(world1, world2);
        }

        [TestMethod]
        public void Worlds_With_Two_Cells_But_Added_In_Different_Orders_Are_Equal()
        {
            // Arrange
            var world1 = new World();
            world1.AddLiveCell(new Position(0, 0));
            world1.AddLiveCell(new Position(0, 1));
            var world2 = new World();
            world2.AddLiveCell(new Position(0, 1));
            world2.AddLiveCell(new Position(0, 0));

            // Act
            // Assert
            Assert.AreEqual(world1, world2);
        }

        [TestMethod]
        public void Adding_Cell_Multiple_Times_Is_Counted_One_Time()
        {
            // Arrange
            var expected = new World();
            expected.AddLiveCell(new Position(0, 0));
            var actual = new World();

            // Act
            actual.AddLiveCell(new Position(0, 0));
            actual.AddLiveCell(new Position(0, 0));
            
            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
