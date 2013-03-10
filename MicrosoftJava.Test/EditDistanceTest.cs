using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text;

namespace MicrosoftJava.Test
{
    [TestClass]
    public class EditDistanceTest
    {

        private string A = "abcdefg";
        private string B = "efgabcdbc";
        private string givenWord = "aabbac";
        private int expectedEditDistance = 8;
        private int expectedListShortDistances = 4;

        private List<string> wordsList = new List<string>()
        {
            "aabac", // ed from given word = 1
            "aaabba", 
            "aabaac",
            "aabba", // ed from given word = 1
            "aabbc", // ed from given word = 1
            "abbac"  // ed from given word = 1
        };

        [TestMethod]
        public void TestEditDistanceAlgorithm()
        {
            int editDistance = WordCloud.EditDistance.LevenshteinDistance(A, B);
            Assert.IsTrue(editDistance == expectedEditDistance);
        }

        [TestMethod]
        public void TestListOfEditDistances()
        {
            Dictionary<string, int> listShortDistances = WordCloud.EditDistance.GetShortestLevenshtein(givenWord, wordsList);
            Assert.IsTrue(listShortDistances.Count == expectedListShortDistances);

        }
    }
}
