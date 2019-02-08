﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using AdventOfCode;

namespace AdventOfCodeTests
{
    [TestClass]
    public class Day07Tests
    {
        const string sampleInput = "Step C must be finished before step A can begin.\nStep C must be finished before step F can begin.\nStep A must be finished before step B can begin.\nStep A must be finished before step D can begin.\nStep B must be finished before step E can begin.\nStep D must be finished before step E can begin.\nStep F must be finished before step E can begin.";
        const string myInput = "Step A must be finished before step N can begin.\nStep P must be finished before step R can begin.\nStep O must be finished before step T can begin.\nStep J must be finished before step U can begin.\nStep M must be finished before step X can begin.\nStep E must be finished before step X can begin.\nStep N must be finished before step T can begin.\nStep W must be finished before step G can begin.\nStep Z must be finished before step D can begin.\nStep F must be finished before step Q can begin.\nStep U must be finished before step L can begin.\nStep I must be finished before step X can begin.\nStep X must be finished before step Y can begin.\nStep D must be finished before step Y can begin.\nStep S must be finished before step K can begin.\nStep C must be finished before step G can begin.\nStep K must be finished before step V can begin.\nStep B must be finished before step R can begin.\nStep Q must be finished before step L can begin.\nStep T must be finished before step H can begin.\nStep H must be finished before step G can begin.\nStep V must be finished before step L can begin.\nStep L must be finished before step R can begin.\nStep G must be finished before step Y can begin.\nStep R must be finished before step Y can begin.\nStep G must be finished before step R can begin.\nStep X must be finished before step V can begin.\nStep V must be finished before step Y can begin.\nStep Z must be finished before step U can begin.\nStep U must be finished before step R can begin.\nStep J must be finished before step Y can begin.\nStep Z must be finished before step C can begin.\nStep O must be finished before step L can begin.\nStep C must be finished before step H can begin.\nStep V must be finished before step G can begin.\nStep F must be finished before step K can begin.\nStep Q must be finished before step G can begin.\nStep S must be finished before step Q can begin.\nStep M must be finished before step G can begin.\nStep T must be finished before step L can begin.\nStep C must be finished before step Q can begin.\nStep T must be finished before step V can begin.\nStep W must be finished before step Z can begin.\nStep C must be finished before step K can begin.\nStep I must be finished before step C can begin.\nStep X must be finished before step Q can begin.\nStep F must be finished before step X can begin.\nStep J must be finished before step S can begin.\nStep I must be finished before step K can begin.\nStep U must be finished before step Q can begin.\nStep I must be finished before step Q can begin.\nStep N must be finished before step H can begin.\nStep A must be finished before step T can begin.\nStep T must be finished before step G can begin.\nStep D must be finished before step T can begin.\nStep A must be finished before step X can begin.\nStep D must be finished before step G can begin.\nStep C must be finished before step T can begin.\nStep W must be finished before step Q can begin.\nStep W must be finished before step K can begin.\nStep V must be finished before step R can begin.\nStep H must be finished before step R can begin.\nStep F must be finished before step H can begin.\nStep F must be finished before step V can begin.\nStep U must be finished before step T can begin.\nStep K must be finished before step H can begin.\nStep B must be finished before step T can begin.\nStep H must be finished before step Y can begin.\nStep J must be finished before step Z can begin.\nStep B must be finished before step Y can begin.\nStep I must be finished before step V can begin.\nStep W must be finished before step V can begin.\nStep Q must be finished before step R can begin.\nStep I must be finished before step S can begin.\nStep E must be finished before step H can begin.\nStep J must be finished before step B can begin.\nStep S must be finished before step G can begin.\nStep E must be finished before step S can begin.\nStep N must be finished before step I can begin.\nStep Z must be finished before step F can begin.\nStep E must be finished before step I can begin.\nStep S must be finished before step B can begin.\nStep D must be finished before step L can begin.\nStep Q must be finished before step T can begin.\nStep Q must be finished before step H can begin.\nStep K must be finished before step Y can begin.\nStep M must be finished before step U can begin.\nStep U must be finished before step K can begin.\nStep W must be finished before step I can begin.\nStep J must be finished before step W can begin.\nStep K must be finished before step T can begin.\nStep P must be finished before step Y can begin.\nStep L must be finished before step G can begin.\nStep K must be finished before step B can begin.\nStep I must be finished before step Y can begin.\nStep U must be finished before step B can begin.\nStep P must be finished before step O can begin.\nStep O must be finished before step W can begin.\nStep O must be finished before step J can begin.\nStep A must be finished before step J can begin.\nStep F must be finished before step G can begin.";

        [TestMethod]
        public void Part1()
        {
            Assert.AreEqual("CABDFE", Day07.Part1(sampleInput));
            Assert.AreEqual("AEMNPOJWISZCDFUKBXQTHVLGRY", Day07.Part1(myInput));
        }

        [TestMethod]
        public void Part2()
        {
            Assert.AreEqual(-1, Day07.Part2(sampleInput));
            Assert.AreEqual(-1, Day07.Part2(myInput));
        }
    }
}