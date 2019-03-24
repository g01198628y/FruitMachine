using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FruitMachine
{
    [TestClass]
    public class FruitMachineResultTest
    {
        [TestMethod]
        public void FruitMachine_Three_Wild_Should_Return_100()
        {
            var score = new CalScore();
            string[] reel = new string[] { "Wild", "Star", "Bell", "Shell", "Seven", "Cherry", "Bar", "King", "Queen", "Jack" };
            List<string[]> reels = new List<string[]> { reel, reel, reel };
            int[] spins = new int[] { 0, 0, 0 };
            Assert.AreEqual(100, score.ShowResult(reels, spins));
        }

        [TestMethod]
        public void FruitMachine_Three_Jack_Should_Return_10()
        {
            var score = new CalScore();
            string[] reel = new string[] { "Wild", "Star", "Bell", "Shell", "Seven", "Cherry", "Bar", "King", "Queen", "Jack" };
            List<string[]> reels = new List<string[]> { reel, reel, reel };
            int[] spins = new int[] { 9, 9, 9 };
            Assert.AreEqual(10, score.ShowResult(reels, spins));
        }

        [TestMethod]
        public void FruitMachine_Three_Cherry_Should_Return_50()
        {
            var score = new CalScore();
            string[] reel = new string[] { "Wild", "Star", "Bell", "Shell", "Seven", "Cherry", "Bar", "King", "Queen", "Jack" };
            List<string[]> reels = new List<string[]> { reel, reel, reel };
            int[] spins = new int[] { 5, 5, 5 };
            Assert.AreEqual(50, score.ShowResult(reels, spins));
        }
    }
}