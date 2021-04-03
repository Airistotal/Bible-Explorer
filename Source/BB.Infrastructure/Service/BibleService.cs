namespace BB.Infrastructure.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BB.Infrastructure.Context;
    using BB.Infrastructure.Model;

    public class BibleService : IBibleService
    {
        private readonly BibleContext bibleContext;

        public BibleService(BibleContext bibleContext)
        {
            this.bibleContext = bibleContext;
        }

        public List<BibleVerse> GetBookChapterVerses(BibleID bibleID, int book, int chapter)
        {
            return (from verse in this.GetBible(bibleID)
                   where verse.Book == book &&
                         verse.Chapter == chapter
                   select verse).ToList();
        }

        public int GetNumberOfChaptersForBookInBible(BibleID bibleID, int book)
        {
            return (from verse in this.GetBible(bibleID)
                    where verse.Book == book
                    select verse.Chapter).Max();
        }

        public List<BibleVersion> GetBibleVersions()
        {
            return this.bibleContext.BibleVersions.ToList();
        }

        public BibleBook GetBookInfo(int bookNum)
        {
            return this.bibleContext.BibleBooks.Find(bookNum);
        }

        public List<BibleBookAbbreviation> GetBibleBooks()
        {
            var primaryAbbrevs = (from bookAbbrev in this.bibleContext.BibleBookAbbreviations
                                  where bookAbbrev.IsPrimaryAbbreviation
                                  orderby bookAbbrev.Abbreviation.Length ascending
                                  select bookAbbrev).ToList();

            return (from abbrevs in primaryAbbrevs
                    group abbrevs by abbrevs.Book into grp
                    select grp.First()).ToList();
        }

        private IQueryable<BibleVerse> GetBible(BibleID bibleID)
        {
            IQueryable<BibleVerse> bible = null;

            switch (bibleID)
            {
                case BibleID.ASV:
                    bible = this.bibleContext.ASV.Cast<BibleVerse>();
                    break;
                case BibleID.BBE:
                    bible = this.bibleContext.BBE.Cast<BibleVerse>();
                    break;
                case BibleID.DARBY:
                    bible = this.bibleContext.DARBY.Cast<BibleVerse>();
                    break;
                case BibleID.KJV:
                    bible = this.bibleContext.KJV.Cast<BibleVerse>();
                    break;
                case BibleID.WBT:
                    bible = this.bibleContext.WBT.Cast<BibleVerse>();
                    break;
                case BibleID.WEB:
                    bible = this.bibleContext.WEB.Cast<BibleVerse>();
                    break;
                case BibleID.YLT:
                    bible = this.bibleContext.YLT.Cast<BibleVerse>();
                    break;
                case BibleID.INVALID:
                    throw new ArgumentException("Bible ID INVALID in GetChapter");
            }

            return bible;
        }
    }
}
