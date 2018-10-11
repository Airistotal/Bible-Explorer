namespace BE_webapp.Controllers
{
    using System.Collections.Generic;
    using BE.Comparer.Business;
    using BE.Comparer.Model;
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Model;
    using Microsoft.AspNetCore.Mvc;

    public class BibleComparerController : Controller
    {
        private readonly BibleContext bibleContext;

        public BibleComparerController(BibleContext bibleContext) => this.bibleContext = bibleContext;

        // GET: BibleComparer
        public ActionResult Index(BibleViewInfo bibleInfo = null)
        {
            return this.View(bibleInfo ?? new BibleViewInfo()
            {
                MainBible = BibleID.ASV,
                OtherBibles = null,
                Book = 1,
                Chapter = 1
            });
        }

        public ActionResult GetPage(BibleViewInfo bibleChapterInfo)
        {
            return this.PartialView("BiblePage", bibleChapterInfo);
        }

        public ActionResult ComparedContent(BibleViewInfo bibleChapterInfo)
        {
            BibleID mainBibleID = bibleChapterInfo.MainBible;
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