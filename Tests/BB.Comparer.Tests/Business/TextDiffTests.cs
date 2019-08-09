namespace BB_webapp.Tests
{
    using BB.Comparer.Business;
    using BB.Comparer.Model;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.IsTrue(result.Differences[0].IndexFrom == 2);
            Assert.IsTrue(result.Differences[0].IndexTo == 3);
            Assert.IsTrue(result.Differences[0].IndexFromOrig == 2);
            Assert.IsTrue(result.Differences[0].IndexToOrig == 3);
        }

        [TestMethod]
        public void TestCompareTextDelete()
        {
            string orig = "a b c";
            string other = "b c";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 1);
            Assert.IsTrue(result.Differences[0].IndexFromOrig == 0);
            Assert.IsTrue(result.Differences[0].IndexToOrig == 1);
            Assert.IsTrue(result.Differences[0].IndexFrom == 0);
            Assert.IsTrue(result.Differences[0].IndexTo == 0);
        }

        [TestMethod]
        public void TestCompareTextAdd()
        {
            string orig = "b d";
            string other = "b d e";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 1);
            Assert.IsTrue(result.Differences[0].IndexFromOrig == 2);
            Assert.IsTrue(result.Differences[0].IndexToOrig == 2);
            Assert.IsTrue(result.Differences[0].IndexFrom == 2);
            Assert.IsTrue(result.Differences[0].IndexTo == 3);
        }

        [TestMethod]
        public void TestCompareTextComplex()
        {
            string orig = "a b c d e f a d";
            string other = "a b l d e a d h";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 3);
            Assert.IsTrue(result.Differences[0].IndexFrom == 2);
            Assert.IsTrue(result.Differences[0].IndexTo == 3);
            Assert.IsTrue(result.Differences[0].IndexFromOrig == 2);
            Assert.IsTrue(result.Differences[0].IndexToOrig == 3);

            Assert.IsTrue(result.Differences[1].IndexFrom == 5);
            Assert.IsTrue(result.Differences[1].IndexTo == 5);
            Assert.IsTrue(result.Differences[1].IndexFromOrig == 5);
            Assert.IsTrue(result.Differences[1].IndexToOrig == 6);

            Assert.IsTrue(result.Differences[2].IndexFrom == 7);
            Assert.IsTrue(result.Differences[2].IndexTo == 8);
            Assert.IsTrue(result.Differences[2].IndexFromOrig == 8);
            Assert.IsTrue(result.Differences[2].IndexToOrig == 8);
        }

        [TestMethod]
        public void TestCompareTextSwap()
        {
            string orig = "a b c d e f";
            string other = "d e f a b c";

            TextDiff sut = new TextDiff();

            CompareResult result = sut.GetTextDifferences(orig, other);

            Assert.IsTrue(result.Differences.Count == 2);
            Assert.IsTrue(result.Differences[0].IndexFromOrig == 0);
            Assert.IsTrue(result.Differences[0].IndexToOrig == 0);
            Assert.IsTrue(result.Differences[0].IndexFrom == 0);
            Assert.IsTrue(result.Differences[0].IndexTo == 3);

            Assert.IsTrue(result.Differences[1].IndexFromOrig == 3);
            Assert.IsTrue(result.Differences[1].IndexToOrig == 6);
            Assert.IsTrue(result.Differences[1].IndexFrom == 6);
            Assert.IsTrue(result.Differences[1].IndexTo == 6);
        }
    }
}
