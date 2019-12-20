namespace BB.Infrastructure.Context
{
    using System;
    using BB.Infrastructure.Model;
    using BB.Infrastructure.Model.SpecificVerses;
    using Microsoft.EntityFrameworkCore;

    public class BibleContext : DbContext
    {
        public BibleContext()
        {
        }

        public BibleContext(DbContextOptions<BibleContext> options)
            : base(options)
        {
        }

        public DbSet<ASVBibleVerse> ASV { get; set; }

        public DbSet<BBEBibleVerse> BBE { get; set; }

        public DbSet<DARBYBibleVerse> DARBY { get; set; }

        public DbSet<KJVBibleVerse> KJV { get; set; }

        public DbSet<WBTBibleVerse> WBT { get; set; }

        public DbSet<WEBBibleVerse> WEB { get; set; }

        public DbSet<YLTBibleVerse> YLT { get; set; }

        public DbSet<BibleVersion> BibleVersions { get; set; }

        public DbSet<BibleBook> BibleBooks { get; set; }

        public DbSet<BibleBookAbbreviation> BibleBookAbbreviations { get; set; }

        public DbSet<BibleBookGenre> BibleBookGenres { get; set; }

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

            modelBuilder.Entity<BibleBook>(entity =>
            {
                entity.ToTable("key_english");
                entity.Property(e => e.Id).HasColumnName("b");
                entity.Property(e => e.GenreID).HasColumnName("g");
                entity.Property(e => e.Name).HasColumnName("n");
                entity.Property(e => e.T).HasColumnName("t");
                entity.Ignore(e => e.Testament);
            });

            modelBuilder.Entity<BibleBookAbbreviation>(entity =>
            {
                entity.ToTable("key_abbreviations_english");
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Abbreviation).HasColumnName("a");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.IsPrimaryAbbreviation).HasColumnName("p");
            });

            modelBuilder.Entity<BibleBookGenre>(entity =>
            {
                entity.ToTable("key_genre_english");
                entity.Property(e => e.Id).HasColumnName("g");
                entity.Property(e => e.GenreName).HasColumnName("n");
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
                entity.ToTable("t_kjv");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<WBTBibleVerse>(entity =>
            {
                entity.ToTable("t_wbt");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<WEBBibleVerse>(entity =>
            {
                entity.ToTable("t_web");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });

            modelBuilder.Entity<YLTBibleVerse>(entity =>
            {
                entity.ToTable("t_ylt");
                entity.Property(e => e.Book).HasColumnName("b");
                entity.Property(e => e.Chapter).HasColumnName("c");
                entity.Property(e => e.Verse).HasColumnName("v");
                entity.Property(e => e.Text).HasColumnName("t");
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=localhost;Database=BB;Trusted_Connection=True;");
            }
        }
    }
}
