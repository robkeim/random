using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day22Tests
    {
        const string sampleInput = "510\n10\n10";
        const string myInput = "11991\n6\n797";

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(114, Day22.Part1(sampleInput));
            Assert.AreEqual(5622, Day22.Part1(myInput));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(0, Day22.Part2(sampleInput));
            Assert.AreEqual(0, Day22.Part2(myInput));
        }
    }
}
