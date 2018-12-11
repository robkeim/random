using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day11Tests
    {
        const int sampleInput = 18;
        const int myInput = 9445;

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(4, Day11.PowerLevel(3, 5, 8));
            Assert.AreEqual(-5, Day11.PowerLevel(122, 79, 57));
            Assert.AreEqual(0, Day11.PowerLevel(217, 196, 39));
            Assert.AreEqual(4, Day11.PowerLevel(101, 153, 71));

            Assert.AreEqual("33,45", Day11.Part1(sampleInput));
            Assert.AreEqual("21,61", Day11.Part1(42));
            Assert.AreEqual("233,36", Day11.Part1(myInput));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(-1, Day11.Part2(sampleInput));
            Assert.AreEqual(-1, Day11.Part2(myInput));
        }
    }
}
