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
            var actual = world
                .GetNextIteration()
                .GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void Dead_Cell_With_Three_Neighbors_Changes_State_In_Next_Iteration()
        {
            // Arrange
            var world = TestUtils.CreateWorld(
                "X..",
                "X.X");
            var expected = TestUtils.CreateExpectedWorldState(
                ".X.",
                ".X.");

            // Act
            var actual = world
                .GetNextIteration()
                .GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }

        [TestMethod]
        public void World_Expands_In_Next_Iteration()
        {
            // Arrange
            var world = TestUtils.CreateWorld(
                "XXX");
            var expected = TestUtils.CreateExpectedWorldState(
                ".X.",
                ".X.",
                ".X.");

            // Act
            var actual = world
                .GetNextIteration()
                .GetCurrentState();

            // Assert
            TestUtils.AssertWorldStatesAreEqual(expected, actual);
        }
    }
}
