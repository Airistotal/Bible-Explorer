namespace BB.Infrastructure.Tests
{
    using System.Collections.Generic;
    using BB.Infrastructure.Model;
    using BB.Infrastructure.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BibleServiceTests : DbInMemoryTest
    {
        [TestMethod]
        public void When_GetValidChapter_FromValidBible_ReturnsListOfVerses()
        {
            // Arrange
            var info = new BibleViewInfo()
            {
                MainBible = BibleID.ASV,
                Book = 1,
                Chapter = 1,
            };

            var sut = new BibleService(this.BibleContext);

            // Act
            IList<BibleVerse> chapter = sut.GetBookChapterVerses(info.MainBible, info.Book, info.Chapter);

            // Assert
            Assert.IsNotNull(chapter);
            Assert.IsFalse(chapter.Count != 0);
        }
    }
}
