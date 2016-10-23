using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class StillLifeTests
    {
        [TestMethod]
        public void Block()
        {
            // Arrange
            var world = TestUtils.CreateWorld(
                "XX",
                "XX");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(world, actual);
        }

        [TestMethod]
        public void Beehive()
        {
            // Arrange
            var world = TestUtils.CreateWorld(
                ".XX.",
                "X..X",
                ".XX.");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(world, actual);
        }

        [TestMethod]
        public void Loaf()
        {
            // Arrange
            var world = TestUtils.CreateWorld(
                ".XX.",
                "X..X",
                ".X.X",
                "..X.");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(world, actual);
        }

        [TestMethod]
        public void Boat()
        {
            // Arrange
            var world = TestUtils.CreateWorld(
                "XX.",
                "X.X",
                ".X.");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(world, actual);
        }
    }
}
