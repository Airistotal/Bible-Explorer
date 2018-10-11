namespace BE.Infrastructure.Tests
{
    using System;
    using BE.Infrastructure.Context;
    using Microsoft.EntityFrameworkCore;

    public class DbInMemoryTest
    {
        public DbInMemoryTest()
        {
            this.ConfigureContext();
            this.FillTestData();
        }

        public BibleContext BibleContext { get; internal set; }

        private void ConfigureContext()
        {
            var dbname = "bibex_db_" + Guid.NewGuid();
            var options = new DbContextOptionsBuilder<BibleContext>()
                .UseInMemoryDatabase(databaseName: dbname)
                .Options;

            this.BibleContext = new BibleContext(options);
        }

        private void FillTestData()
        {
        }
    }
}