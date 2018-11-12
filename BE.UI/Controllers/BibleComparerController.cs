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

        [HttpGet]
        public ActionResult Index()
        {
            return this.View(new BibleViewInfo());
        }

        // GET: BibleComparer
        [HttpPost]
        public ActionResult Index(BibleViewInfo bibleInfo)
        {
            if (bibleInfo != null)
            {
                bibleInfo.CleanSelf();
            }
            else
            {
                bibleInfo = new BibleViewInfo();
            }

            return this.View(bibleInfo);
        }
    }
}