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

        public IList<BibleVerse> GetChapter(BibleID bibleID, int chapter)
        {
            IList<BibleVerse> bible = null;

            switch (bibleID)
            {
                case BibleID.ASV:
                    bible = (IList<BibleVerse>)this.bibleContext.ASV;
                    break;
                case BibleID.BBE:
                    bible = (IList<BibleVerse>)this.bibleContext.BBE;
                    break;
                case BibleID.DARBY:
                    bible = (IList<BibleVerse>)this.bibleContext.DARBY;
                    break;
                case BibleID.KJV:
                    bible = (IList<BibleVerse>)this.bibleContext.KJV;
                    break;
                case BibleID.WBT:
                    bible = (IList<BibleVerse>)this.bibleContext.WBT;
                    break;
                case BibleID.WEB:
                    bible = (IList<BibleVerse>)this.bibleContext.WEB;
                    break;
                case BibleID.YLT:
                    bible = (IList<BibleVerse>)this.bibleContext.YLT;
                    break;
                case BibleID.INVALID:
                    throw new ArgumentException("Bible ID INVALID in GetChapter");
            }

            return bible.Where(x => x.Chapter == chapter).ToList();
        }

        public BibleChapterInfo GetChapterInfo(int chapter)
        {

        }
    }
}
