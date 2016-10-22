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
            var expected = new Cell[1, 1];
            expected[0, 0] = new LiveCell();

            // Act
            var actual = world.GetState();

            // Assert
            AssertWorldStatesAreEqual(expected, actual);
        }

        public static void AssertWorldStatesAreEqual(Cell[,] expected, Cell[,] actual)
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
