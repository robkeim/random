using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day12Tests
    {
        const string sampleInput = "#..#.#..##......###...###";
        const string sampleInputRules = "...## => #\n..#.. => #\n.#... => #\n.#.#. => #\n.#.## => #\n.##.. => #\n.#### => #\n#.#.# => #\n#.### => #\n##.#. => #\n##.## => #\n###.. => #\n###.# => #\n####. => #";
        const string myInput = "#.#####.##.###...#...#.####..#..#.#....##.###.##...#####.#..##.#..##..#..#.#.#.#....#.####....#..#";
        const string myInputRules = "#.#.. => .\n..### => .\n...## => .\n.#### => #\n.###. => #\n#.... => .\n#.#.# => .\n###.. => #\n#..#. => .\n##### => #\n.##.# => #\n.#... => .\n##.## => #\n#...# => #\n.#.## => .\n##..# => .\n..... => .\n.#.#. => #\n#.### => #\n....# => .\n...#. => #\n..#.# => #\n##... => #\n####. => #\n#..## => #\n##.#. => #\n###.# => .\n#.##. => .\n..#.. => #\n.#..# => .\n..##. => .\n.##.. => #";

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual(325, Day12.Part1(sampleInput, sampleInputRules));
            Assert.AreEqual(4200, Day12.Part1(myInput, myInputRules));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(-1, Day12.Part2(sampleInput, sampleInputRules));
            Assert.AreEqual(-1, Day12.Part2(myInput, myInputRules));
        }
    }
}
