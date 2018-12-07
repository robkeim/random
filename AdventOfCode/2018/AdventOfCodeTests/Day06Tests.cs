using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day06Tests
    {
        const string sampleInput = "1, 1\n1, 6\n8, 3\n3, 4\n5, 5\n8, 9";
        const string myInput = "315, 342\n59, 106\n44, 207\n52, 81\n139, 207\n93, 135\n152, 187\n271, 47\n223, 342\n50, 255\n332, 68\n322, 64\n250, 72\n165, 209\n129, 350\n139, 118\n282, 129\n311, 264\n216, 246\n134, 42\n66, 151\n263, 199\n222, 169\n236, 212\n320, 178\n202, 288\n273, 190\n83, 153\n88, 156\n284, 305\n131, 90\n152, 88\n358, 346\n272, 248\n317, 122\n166, 179\n301, 307\n156, 128\n261, 290\n268, 312\n89, 53\n324, 173\n353, 177\n91, 69\n303, 164\n40, 221\n146, 344\n61, 314\n319, 224\n98, 143";

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(17, Day06.Part1(sampleInput));
            Assert.AreEqual(4290, Day06.Part1(myInput));
        }

        //[TestMethod]
        public void Part2()
        {
            Assert.AreEqual(-1, Day06.Part2(sampleInput));
            Assert.AreEqual(-1, Day06.Part2(myInput));
        }
    }
}
