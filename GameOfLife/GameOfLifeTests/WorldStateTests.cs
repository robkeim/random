using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class WorldStateTests
    {
        [TestMethod]
        public void Empty_World_Returns_Empty_State()
        {
            // Arrange
            var world = new World();
            var expected = new Cell[0, 0];

            // Act
            var actual = world.GetCurrentState();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_One_Live_Cell_Returns_One_By_One_State()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "X");
            var expected = Utils.CreateWorldState(
                "X");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_Horizontal_Returns_State()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "XX");
            var expected = Utils.CreateWorldState(
                "XX");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_Vertical_Returns_State()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "X",
                "X");
            var expected = Utils.CreateWorldState(
                "X",
                "X");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_By_Two_Live_Cells_Vertical_Returns_State()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "XX",
                "XX");
            var expected = Utils.CreateWorldState(
                "XX",
                "XX");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_Separated_By_Space_Horizontal_Returns_State()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "X.X");
            var expected = Utils.CreateWorldState(
                "X.X");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_Separated_By_Space_Verical_Returns_State()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "X",
                ".",
                "X");
            var expected = Utils.CreateWorldState(
                "X",
                ".",
                "X");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_In_Opposite_Corners_Returns_State()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "X..",
                "...",
                "..X");
            var expected = Utils.CreateWorldState(
                "X..",
                "...",
                "..X");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_In_Opposite_Corners_And_Negative_Positions_Returns_State()
        {
            // Arrange
            var world = new World();
            world.AddLiveCell(new Position(-1, -1));
            world.AddLiveCell(new Position(1, 1));
            var expected = Utils.CreateWorldState(
                "X..",
                "...",
                "..X");

            // Act
            var actual = world.GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }
    }
}
