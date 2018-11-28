namespace BE_webapp.Controllers
{
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Model;
    using Microsoft.AspNetCore.Mvc;

    public class BibleComparerController : Controller
    {
        private readonly BibleContext bibleContext;

        public BibleComparerController(BibleContext bibleContext) => this.bibleContext = bibleContext;

        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new BibleViewInfo());
        }

        // GET: BibleComparer
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(BibleViewInfo bibleViewInfo)
        {
            if (bibleViewInfo != null)
            {
                bibleViewInfo.CleanSelf();
            }
            else
            {
                bibleViewInfo = new BibleViewInfo();
            }

            return this.View(bibleViewInfo);
        }
    }
}