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
    }
}
