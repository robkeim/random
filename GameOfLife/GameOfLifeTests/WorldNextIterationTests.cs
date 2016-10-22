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
            // Arrange
            var world = TestUtils.CreateWorld(
                "X");
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
