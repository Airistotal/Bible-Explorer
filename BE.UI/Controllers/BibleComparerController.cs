namespace BE_webapp.Controllers
{
    using System.Collections.Generic;
    using BE.Comparer.Business;
    using BE.Comparer.Models;
    using BE.Infrastructure.Model;
    using Microsoft.AspNetCore.Mvc;

    public class BibleComparerController : Controller
    {
        // GET: BibleComparer
        public ActionResult Index(int mBible = 1, List<BibleID> cBibles = null, int book = 1, int chapter = 1)
        {
            return this.View(new BibleChapterInfo()
            {
                MainBible = mBible,
                OtherBibles = cBibles,
                Book = book,
                Chapter = chapter
            });
        }

        public ActionResult GetPage(BibleChapterInfo bibleChapterInfo)
        {
            return this.PartialView("BiblePage", bibleChapterInfo);
        }

        public ActionResult ComparedContent(BibleChapterInfo bibleChapterInfo)
        {
            int mainBibleID = bibleChapterInfo.MainBible;
            int book = bibleChapterInfo.Book;
            int chapter = bibleChapterInfo.Chapter;
            IEnumerable<BibleVerse> bibleChapter = new List<BibleVerse>();

            List<ComparedBibleVerse> bibleChapterCompared = new List<ComparedBibleVerse>();
            foreach (BibleVerse bibleVerse in bibleChapter)
            {
                int verse = bibleVerse.Verse;
                ComparedBibleVerse compared = new ComparedBibleVerse(new TextDiff(), bibleVerse);

                foreach (BibleID bibleID in bibleChapterInfo.OtherBibles)
                {
                    BibleVerse currBibleVerse = new BibleVerse();
                    compared.AddComparison(bibleID, currBibleVerse);
                }

                bibleChapterCompared.Add(compared);
            }

            return this.PartialView("PagedContent", bibleChapterCompared);
        }
    }
}