using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class WorldNextIterationTests
    {
        [TestMethod]
        public void World_With_One_Cell_Dies_In_Next_Iteration()
        {
            // TODO replace all world initializations with helper function

            // Arrange
            var world = new World();
            world.AddLiveCell(new Position(0, 0));
            var expected = TestUtils.CreateExpectedWorldState(
                ".");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual.GetCurrentState());
        }

        // TODO add test to verify that world expands as needed
    }
}
