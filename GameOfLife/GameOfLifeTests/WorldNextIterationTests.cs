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
            var world = Utils.CreateWorld(
                "X");
            var expected = Utils.CreateWorldState(
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
            var world = Utils.CreateWorld(
                "X..",
                "X.X");
            var expected = Utils.CreateWorldState(
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
            var world = Utils.CreateWorld(
                "XXX");
            var expected = Utils.CreateWorldState(
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
