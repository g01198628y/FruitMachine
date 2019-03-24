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

        [TestMethod]
        public void FruitMachine_No_Matching_Items_Should_Return_0()
        {
            var score = new CalScore();
            string[] reel1 = new string[] { "Wild", "Star", "Bell", "Shell", "Seven", "Cherry", "Bar", "King", "Queen", "Jack" };
            string[] reel2 = new string[] { "Bar", "Wild", "Queen", "Bell", "King", "Seven", "Cherry", "Jack", "Star", "Shell" };
            string[] reel3 = new string[] { "Bell", "King", "Wild", "Bar", "Seven", "Jack", "Shell", "Cherry", "Queen", "Star" };
            List<string[]> reels = new List<string[]> { reel1, reel2, reel3 };
            int[] spins = new int[] { 5, 4, 3 };
            Assert.AreEqual(0, score.ShowResult(reels, spins));
        }

        [TestMethod]
        public void FruitMachine_Two_Bell_One_Wild_Should_Return_16()
        {
            var score = new CalScore();
            string[] reel1 = new string[] { "Wild", "Star", "Bell", "Shell", "Seven", "Cherry", "Bar", "King", "Queen", "Jack" };
            string[] reel2 = new string[] { "Bar", "Wild", "Queen", "Bell", "King", "Seven", "Cherry", "Jack", "Star", "Shell" };
            string[] reel3 = new string[] { "Bell", "King", "Wild", "Bar", "Seven", "Jack", "Shell", "Cherry", "Queen", "Star" };
            List<string[]> reels = new List<string[]> { reel1, reel2, reel3 };
            int[] spins = new int[] { 2, 3, 2 };
            Assert.AreEqual(16, score.ShowResult(reels, spins));
        }
    }
}