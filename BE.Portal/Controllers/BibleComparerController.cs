using BE_webapp.Business;
using BE_webapp.Models;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;

namespace BE_webapp.Controllers
{
    public class BibleComparerController : Controller
    {
        // GET: BibleComparer
        public ActionResult Index(int mBible = 1, List<int> cBibles = null, int B = 1, int C = 1)
        {
            return View(new BibleChapterMeta() { mBible = mBible, cBibles = cBibles, B = B, C = C });
        }

        public ActionResult GetPage(BibleChapterMeta bibleChapterMeta)
        {
            return PartialView("BiblePage", bibleChapterMeta);
        }

        public ActionResult ComparedContent(BibleChapterMeta bibleChapterMeta)
        {
            int mainBibleID = bibleChapterMeta.mBible;
            int book = bibleChapterMeta.B;
            int chapter = bibleChapterMeta.C;
            IEnumerable<BibleVerse> bibleChapter = new bibex_dbEntities().usp_GetChapter(mainBibleID, book, chapter).AsEnumerable();

            List<ComparedBibleVerse> bibleChapterCompared = new List<ComparedBibleVerse>();
            foreach (BibleVerse bibleVerse in bibleChapter)
            {
                int verse = bibleVerse.v;
                ComparedBibleVerse compared = new ComparedBibleVerse(new TextDiff(), bibleVerse);

                foreach (int bibleID in bibleChapterMeta.cBibles)
                {
                    BibleVerse currBibleVerse = new bibex_dbEntities().usp_GetVerse(bibleID, book, chapter, verse).First();
                    compared.AddComparison(currBibleVerse);
                }

                bibleChapterCompared.Add(compared);
            }

            return PartialView("PagedContent", bibleChapterCompared);
        }
    }
}