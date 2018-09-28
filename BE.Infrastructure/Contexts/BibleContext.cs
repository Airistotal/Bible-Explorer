namespace BE.Infrastructure.Contexts
{
    using BE.Infrastructure.Model;
    using Microsoft.EntityFrameworkCore;

    class BibleContext : DbContext
    {
        public DbSet<Bible> ASV { get; set; }
        public DbSet<Bible> BBE { get; set; }
        public DbSet<Bible> DARBY { get; set; }
        public DbSet<Bible> KJV { get; set; }
        public DbSet<Bible> WBT { get; set; }
        public DbSet<Bible> WEB { get; set; }
        public DbSet<Bible> YLT { get; set; }

        public DbSet<BibleVersion> BibleVersions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\sqlexpress;Database=bibex_db;Trusted_Connection=True;");
        }
    }
}
