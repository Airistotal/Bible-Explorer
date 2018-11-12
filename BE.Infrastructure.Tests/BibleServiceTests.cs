namespace BE.Infrastructure.Tests
{
    using System.Collections.Generic;
    using BE.Infrastructure.Model;
    using BE.Infrastructure.Service;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BibleServiceTests : DbInMemoryTest
    {
        [TestMethod]
        public async void When_GetValidChapter_FromValidBible_ReturnsListOfVersesAsync()
        {
            // Arrange
            var info = new BibleViewInfo()
            {
                MainBible = BibleID.ASV,
                Book = 1,
                Chapter = 1
            };

            var sut = new BibleService(this.BibleContext);

            // Act
            IList<BibleVerse> chapter = await sut.GetBookChapterVersesAsync(info);

            // Assert
            Assert.IsNotNull(chapter);
            Assert.IsFalse(chapter.Count != 0);
        }
    }
}
