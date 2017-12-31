using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BE_webapp.Business;

namespace BE_webapp.Tests
{
    [TestClass]
    public class TextDiffTests
    {
        [TestMethod]
        public void TestCompareText()
        {
            string a = "a b c";
            string b = "a b d";

            Dictionary<Tuple<int, int>, int> result;

            result = TextDiff.compareText(a, b);

            foreach( Tuple<int, int> key in result.Keys)
            {
                Assert.AreEqual(key, new Tuple<int, int>(0,0));
                Assert.AreEqual(result[key], 2);
            }

            a = "a b c d";
            b = "b c e a";
            result.Clear();

            result = TextDiff.compareText(a, b);

            foreach (Tuple<int, int> key in result.Keys)
            {
                Assert.AreEqual(key, new Tuple<int, int>(1, 0));
                Assert.AreEqual(result[key], 2);
            }

            a = "a b c d e f";
            b = "b c d e b d e f a b";
            result.Clear();

            result = TextDiff.compareText(a, b);

            foreach (Tuple<int, int> key in result.Keys)
            {
                Assert.AreEqual(key, new Tuple<int, int>(1, 0));
                Assert.AreEqual(result[key], 4);
            }

            a = "a b c d e f";
            b = "a b l d e f";
            result.Clear();

            result = TextDiff.compareText(a, b);

            Assert.IsTrue(result.ContainsKey(new Tuple<int, int>(0, 0)));
            Assert.IsTrue(result.ContainsKey(new Tuple<int, int>(3, 3)));
            Assert.AreEqual(result[new Tuple<int, int>(3, 3)], 3);
            Assert.AreEqual(result[new Tuple<int, int>(0, 0)], 2);
        }
    }
}
