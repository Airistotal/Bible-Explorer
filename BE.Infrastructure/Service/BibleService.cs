namespace BE.Infrastructure.Service
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Model;

    public class BibleService : IBibleService
    {
        private readonly BibleContext bibleContext;

        public BibleService(BibleContext bibleContext)
        {
            this.bibleContext = bibleContext;
        }

        public IList<BibleVerse> GetBook(BibleID bibleID, int book)
        {
            IList<BibleVerse> bible = null;

            switch (bibleID)
            {
                case BibleID.ASV:
                    bible = this.bibleContext.ASV.Cast<BibleVerse>().ToList();
                    break;
                case BibleID.BBE:
                    bible = this.bibleContext.BBE.Cast<BibleVerse>().ToList();
                    break;
                case BibleID.DARBY:
                    bible = this.bibleContext.DARBY.Cast<BibleVerse>().ToList();
                    break;
                case BibleID.KJV:
                    bible = this.bibleContext.KJV.Cast<BibleVerse>().ToList();
                    break;
                case BibleID.WBT:
                    bible = this.bibleContext.WBT.Cast<BibleVerse>().ToList();
                    break;
                case BibleID.WEB:
                    bible = this.bibleContext.WEB.Cast<BibleVerse>().ToList();
                    break;
                case BibleID.YLT:
                    bible = this.bibleContext.YLT.Cast<BibleVerse>().ToList();
                    break;
                case BibleID.INVALID:
                    throw new ArgumentException("Bible ID INVALID in GetChapter");
            }

            return bible.Where(x => x.Book == book).ToList();
        }

        public BibleBook GetBook(int bookNum)
        {
            return this.bibleContext.BibleBooks.Find(bookNum);
        }
    }
}
