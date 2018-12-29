using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day14Tests
    {
        const int sampleInput = 9;
        const int myInput = 306281;

        // This test takes a couple of minutes to run
        //[TestMethod]
        public void Part1()
        {
            Assert.AreEqual("5158916779", Day14.Part1(sampleInput));
            Assert.AreEqual("0124515891", Day14.Part1(5));
            Assert.AreEqual("9251071085", Day14.Part1(18));
            Assert.AreEqual("5941429882", Day14.Part1(2018));
            Assert.AreEqual("3718110721", Day14.Part1(myInput));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(9, Day14.Part2(51589));
            //Assert.AreEqual(5, Day14.Part2(01245));
            Assert.AreEqual(18, Day14.Part2(92510));
            Assert.AreEqual(2018, Day14.Part2(59414));
            Assert.AreEqual(20298300, Day14.Part2(myInput));
        }
    }
}
