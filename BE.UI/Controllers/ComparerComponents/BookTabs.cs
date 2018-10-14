namespace BE.UI.Controllers.ComparerComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class BookTabs : ViewComponent
    {
        private readonly BibleContext bibleContext;

        public BookTabs(BibleContext bibleContext)
        {
            this.bibleContext = bibleContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(BibleViewInfo bibleViewInfo)
        {
            List<BibleBookAbbreviation> abbrevs = await this.GetBibleAbbrevsAsync();
            return this.View(
                new Tuple<List<BibleBookAbbreviation>, BibleViewInfo>(
                    abbrevs,
                    bibleViewInfo));
        }

        private Task<List<BibleBookAbbreviation>> GetBibleAbbrevsAsync()
        {
            return this.bibleContext.BibleBookAbbreviations.
                Where(e => e.IsPrimaryAbbreviation).ToListAsync();
        }
    }
}
