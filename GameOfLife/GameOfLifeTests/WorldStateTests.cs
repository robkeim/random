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
            var actual = world.GetState();

            // Assert
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_One_Live_Cell_Returns_One_By_One_State()
        {
            // Arrange
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            var expected = CreateExpectedWorldState("X");

            // Act
            var actual = world.GetState();

            // Assert
            AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_Horizontal_Returns_State()
        {
            // Arrange
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            world.AddLiveCell(new Position(1, 0));
            var expected = CreateExpectedWorldState(
                "XX");

            // Act
            var actual = world.GetState();

            // Assert
            AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_Live_Cells_Vertical_Returns_State()
        {
            // Arrange
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            world.AddLiveCell(new Position(0, 1));
            var expected = CreateExpectedWorldState(
                "X",
                "X");

            // Act
            var actual = world.GetState();

            // Assert
            AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_With_Two_By_Two_Live_Cells_Vertical_Returns_State()
        {
            // Arrange
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            world.AddLiveCell(new Position(0, 1));
            world.AddLiveCell(new Position(1, 0));
            world.AddLiveCell(new Position(1, 1));
            var expected = CreateExpectedWorldState(
                "XX",
                "XX");

            // Act
            var actual = world.GetState();

            // Assert
            AssertWorldStatesAreEqual(expected, actual);
        }

        private static Cell[,] CreateExpectedWorldState(params string[] lines)
        {
            var xSize = lines[0].Length;
            var ySize = lines.Length;

            var result = new Cell[ySize, xSize];

            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    result[j, i] = lines[j][i] == 'X'
                        ? (Cell)new LiveCell()
                        : new DeadCell();
                }
            }

            return result;
        }

        private static void AssertWorldStatesAreEqual(Cell[,] expected, Cell[,] actual)
        {
            Assert.AreEqual(expected.Rank, actual.Rank);
            Assert.AreEqual(expected.GetLength(0), actual.GetLength(0));
            Assert.AreEqual(expected.GetLength(1), actual.GetLength(1));

            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}
