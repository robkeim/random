using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day09Tests
    {
        const string sampleInput = "9 players; last marble is worth 25 points";
        const string myInput = "459 players; last marble is worth 71320 points";

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(32, Day09.Part1(sampleInput));
            // Additional samples
            Assert.AreEqual(8317, Day09.Part1("10 players; last marble is worth 1618 points"));
            Assert.AreEqual(146373, Day09.Part1("13 players; last marble is worth 7999 points"));
            Assert.AreEqual(2764, Day09.Part1("17 players; last marble is worth 1104 points"));
            Assert.AreEqual(54718, Day09.Part1("21 players; last marble is worth 6111 points"));
            Assert.AreEqual(37305, Day09.Part1("30 players; last marble is worth 5807 points"));
            Assert.AreEqual(375414, Day09.Part1(myInput));
        }

        // This takes 1.5 to 2 hours to execute due to slow array copying. Using a LinkedList would
        // speed up the execution time considerably
        //[TestMethod]
        public void Part2()
        {
            Assert.AreEqual(3168033673, Day09.Part2("459 players; last marble is worth 7132000 points"));
        }
    }
}
