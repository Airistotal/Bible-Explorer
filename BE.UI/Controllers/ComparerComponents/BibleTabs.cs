namespace BE.UI.Controllers.ComparerComponents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BE.Infrastructure.Context;
    using BE.Infrastructure.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class BibleTabs : ViewComponent
    {
        private readonly BibleContext bibleContext;

        public BibleTabs(BibleContext bibleContext)
        {
            this.bibleContext = bibleContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(BibleViewInfo bibleViewInfo)
        {
            var items = await this.GetBibleVersionsAsync();
            return this.View(
                new Tuple<List<BibleVersion>, BibleViewInfo>(
                    items,
                    bibleViewInfo));
        }

        private Task<List<BibleVersion>> GetBibleVersionsAsync()
        {
            return this.bibleContext.BibleVersions.ToListAsync();
        }
    }
}
