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

        public async Task<IViewComponentResult> InvokeAsync(BibleViewInfo bibleChapterInfo)
        {
            BibleBook book = await this.GetBibleBookAsync(bibleChapterInfo.Book);
            return this.View(
                new Tuple<List<BibleBookAbbreviation>, BibleViewInfo>(
                    book.Abbreviations,
                    bibleChapterInfo));
        }

        private Task<BibleBook> GetBibleBookAsync(int book)
        {
            return this.bibleContext.BibleBooks.Where(e => e.Book == book).FirstOrDefaultAsync();
        }
    }
}
