namespace BE.Infrastructure.Integration.Tests
{
    using System;
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Model.SpecificVerses;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class BibleContextTests
    {
        private BibleContext bibleContext;

        [TestInitialize]
        public void Start()
        {
            this.bibleContext = new BibleContext();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Not allowed to save!")]
        public void When_SaveChange_ThrowException()
        {
            // Arrange
            this.bibleContext.ASV.Add(new ASVBibleVerse());

            // Act
            this.bibleContext.SaveChanges();
        }
    }
}
