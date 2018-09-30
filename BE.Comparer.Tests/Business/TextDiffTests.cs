using BE.Comparer.Business;
using BE.Comparer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BE_webapp.Tests
{
    [TestClass]
    public class TextDiffTests
    {
        [TestMethod]
        public void TestCompareTextModify()
        {
            string orig = "a b c";
            string other = "a b d";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 1);
            Assert.IsTrue(result.Differences[0].index_from == 2);
            Assert.IsTrue(result.Differences[0].index_to == 3);
            Assert.IsTrue(result.Differences[0].index_from_orig == 2);
            Assert.IsTrue(result.Differences[0].index_to_orig == 3);
        }

        [TestMethod]
        public void TestCompareTextDelete()
        {
            string orig = "a b c";
            string other = "b c";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 1);
            Assert.IsTrue(result.Differences[0].index_from_orig == 0);
            Assert.IsTrue(result.Differences[0].index_to_orig == 1);
            Assert.IsTrue(result.Differences[0].index_from == 0);
            Assert.IsTrue(result.Differences[0].index_to == 0);
        }

        [TestMethod]
        public void TestCompareTextAdd()
        {
            string orig = "b d";
            string other = "b d e";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 1);
            Assert.IsTrue(result.Differences[0].index_from_orig == 2);
            Assert.IsTrue(result.Differences[0].index_to_orig == 2);
            Assert.IsTrue(result.Differences[0].index_from == 2);
            Assert.IsTrue(result.Differences[0].index_to == 3);
        }

        [TestMethod]
        public void TestCompareTextComplex()
        {
            string orig = "a b c d e f a d";
            string other = "a b l d e a d h";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 3);
            Assert.IsTrue(result.Differences[0].index_from == 2);
            Assert.IsTrue(result.Differences[0].index_to == 3);
            Assert.IsTrue(result.Differences[0].index_from_orig == 2);
            Assert.IsTrue(result.Differences[0].index_to_orig == 3);

            Assert.IsTrue(result.Differences[1].index_from == 5);
            Assert.IsTrue(result.Differences[1].index_to == 5);
            Assert.IsTrue(result.Differences[1].index_from_orig == 5);
            Assert.IsTrue(result.Differences[1].index_to_orig == 6);

            Assert.IsTrue(result.Differences[2].index_from == 7);
            Assert.IsTrue(result.Differences[2].index_to == 8);
            Assert.IsTrue(result.Differences[2].index_from_orig == 8);
            Assert.IsTrue(result.Differences[2].index_to_orig == 8);
        }

        [TestMethod]
        public void TestCompareTextSwap()
        {
            string orig = "a b c d e f";
            string other = "d e f a b c";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 2);
            Assert.IsTrue(result.Differences[0].index_from_orig == 0);
            Assert.IsTrue(result.Differences[0].index_to_orig == 0);
            Assert.IsTrue(result.Differences[0].index_from == 0);
            Assert.IsTrue(result.Differences[0].index_to == 3);

            Assert.IsTrue(result.Differences[1].index_from_orig == 3);
            Assert.IsTrue(result.Differences[1].index_to_orig == 6);
            Assert.IsTrue(result.Differences[1].index_from == 6);
            Assert.IsTrue(result.Differences[1].index_to == 6);
        }
    }
}
