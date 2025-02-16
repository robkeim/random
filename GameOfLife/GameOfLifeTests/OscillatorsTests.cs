﻿using System;
using GameOfLife;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GameOfLifeTests
{
    [TestClass]
    public class OscillatorsTests
    {
        [TestMethod]
        public void Blinker1()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "...",
                "XXX",
                "...");
            var expected = Utils.CreateWorld(
                ".X.",
                ".X.",
                ".X.");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Blinker2()
        {
            // Arrange
            var world = Utils.CreateWorld(
                ".X.",
                ".X.",
                ".X.");
            
            var expected = Utils.CreateWorld(
                "...",
                "XXX",
                "...");
            
            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Toad1()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "....",
                ".XXX",
                "XXX.",
                "....");
            var expected = Utils.CreateWorld(
                "..X.",
                "X..X",
                "X..X",
                ".X..");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Toad2()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "..X.",
                "X..X",
                "X..X",
                ".X..");
            var expected = Utils.CreateWorld(
                "....",
                ".XXX",
                "XXX.",
                "....");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Beacon1()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "XX..",
                "X...",
                "...X",
                "..XX");
            var expected = Utils.CreateWorld(
                "XX..",
                "XX..",
                "..XX",
                "..XX");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Beacon2()
        {
            // Arrange
            var world = Utils.CreateWorld(
                "XX..",
                "XX..",
                "..XX",
                "..XX");
            var expected = Utils.CreateWorld(
                "XX..",
                "X...",
                "...X",
                "..XX");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Pulsar1()
        {
            // Arrange
            var world = Utils.CreateWorld(
                ".................",
                ".................",
                "....XXX...XXX....",
                ".................",
                "..X....X.X....X..",
                "..X....X.X....X..",
                "..X....X.X....X..",
                "....XXX...XXX....",
                ".................",
                "....XXX...XXX....",
                "..X....X.X....X..",
                "..X....X.X....X..",
                "..X....X.X....X..",
                ".................",
                "....XXX...XXX....",
                ".................",
                ".................");
            var expected = Utils.CreateWorld(
                ".................",
                ".....X.....X.....",
                ".....X.....X.....",
                ".....XX...XX.....",
                ".................",
                ".XXX..XX.XX..XXX.",
                "...X.X.X.X.X.X...",
                ".....XX...XX.....",
                ".................",
                ".....XX...XX.....",
                "...X.X.X.X.X.X...",
                ".XXX..XX.XX..XXX.",
                ".................",
                ".....XX...XX.....",
                ".....X.....X.....",
                ".....X.....X.....",
                ".................");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Pulsar2()
        {
            // Arrange
            var world = Utils.CreateWorld(
                ".................",
                ".....X.....X.....",
                ".....X.....X.....",
                ".....XX...XX.....",
                ".................",
                ".XXX..XX.XX..XXX.",
                "...X.X.X.X.X.X...",
                ".....XX...XX.....",
                ".................",
                ".....XX...XX.....",
                "...X.X.X.X.X.X...",
                ".XXX..XX.XX..XXX.",
                ".................",
                ".....XX...XX.....",
                ".....X.....X.....",
                ".....X.....X.....",
                ".................");
            var expected = Utils.CreateWorld(
                ".................",
                ".................",
                "....XX.....XX....",
                ".....XX...XX.....",
                "..X..X.X.X.X..X..",
                "..XXX.XX.XX.XXX..",
                "...X.X.X.X.X.X...",
                "....XXX...XXX....",
                ".................",
                "....XXX...XXX....",
                "...X.X.X.X.X.X...",
                "..XXX.XX.XX.XXX..",
                "..X..X.X.X.X..X..",
                ".....XX...XX.....",
                "....XX.....XX....",
                ".................",
                ".................");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Pulsar3()
        {
            // Arrange
            var world = Utils.CreateWorld(
                ".................",
                ".................",
                "....XX.....XX....",
                ".....XX...XX.....",
                "..X..X.X.X.X..X..",
                "..XXX.XX.XX.XXX..",
                "...X.X.X.X.X.X...",
                "....XXX...XXX....",
                ".................",
                "....XXX...XXX....",
                "...X.X.X.X.X.X...",
                "..XXX.XX.XX.XXX..",
                "..X..X.X.X.X..X..",
                ".....XX...XX.....",
                "....XX.....XX....",
                ".................",
                ".................");
            var expected = Utils.CreateWorld(
                ".................",
                ".................",
                "....XXX...XXX....",
                ".................",
                "..X....X.X....X..",
                "..X....X.X....X..",
                "..X....X.X....X..",
                "....XXX...XXX....",
                ".................",
                "....XXX...XXX....",
                "..X....X.X....X..",
                "..X....X.X....X..",
                "..X....X.X....X..",
                ".................",
                "....XXX...XXX....",
                ".................",
                ".................");

            // Act
            var actual = world.GetNextIteration();

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
