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
        public void When_GetValidChapter_FromValidBible_ReturnsListOfVerses()
        {
            // Arrange
            var sut = new BibleService(this.BibleContext);

            // Act
            IList<BibleVerse> chapter = sut.GetChapter(BibleID.ASV, 1);

            // Assert
            Assert.IsNotNull(chapter);
            Assert.IsFalse(chapter.Count != 0);
        }
    }
}
