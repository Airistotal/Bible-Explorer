namespace BE.Infrastructure.Contexts
{
    using System;
    using BE.Infrastructure.Model;
    using BE.Infrastructure.Model.SpecificVerses;
    using Microsoft.EntityFrameworkCore;

    public class BibleContext : DbContext
    {
        public DbSet<ASVBibleVerse> ASV { get; set; }

        public DbSet<BBEBibleVerse> BBE { get; set; }

        public DbSet<DARBYBibleVerse> DARBY { get; set; }

        public DbSet<KJVBibleVerse> KJV { get; set; }

        public DbSet<WBTBibleVerse> WBT { get; set; }

        public DbSet<WEBBibleVerse> WEB { get; set; }

        public DbSet<YLTBibleVerse> YLT { get; set; }

        public DbSet<BibleVersion> BibleVersions { get; set; }

        public override int SaveChanges()
        {
            throw new InvalidOperationException("This context is read-only.");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BibleVersion>(entity =>
            {
                entity.ToTable("bible_version_key");
            });

            modelBuilder.Entity<ASVBibleVerse>(entity =>
            {
                entity.ToTable("t_asv");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<BBEBibleVerse>(entity =>
            {
                entity.ToTable("t_bbe");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<DARBYBibleVerse>(entity =>
            {
                entity.ToTable("t_dby");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<KJVBibleVerse>(entity =>
            {
                entity.ToTable("t_dby");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<WBTBibleVerse>(entity =>
            {
                entity.ToTable("t_dby");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<WEBBibleVerse>(entity =>
            {
                entity.ToTable("t_dby");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<YLTBibleVerse>(entity =>
            {
                entity.ToTable("t_dby");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\sqlexpress;Database=bibex_db;Trusted_Connection=True;");
            }
        }
    }
}
