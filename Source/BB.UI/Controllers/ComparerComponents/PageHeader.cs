namespace BB.UI.Controllers.ComparerComponents
{
    using System.Linq;
    using System.Threading.Tasks;
    using BB.Infrastructure.Context;
    using BB.Infrastructure.Model;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    public class PageHeader : ViewComponent
    {
        private readonly BibleContext bibleContext;

        public PageHeader(BibleContext bibleContext)
        {
            this.bibleContext = bibleContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(BibleViewInfo bibleViewInfo)
        {
            BibleBook book = await this.GetBibleBookAsync(bibleViewInfo.Book);
            return this.View(new BibleChapterInfo()
            {
                Book = book.Id,
                BookGenre = book.Genre.GenreName,
                BookName = book.Name,
                Chapter = bibleViewInfo.Chapter,
                TestamentID = book.Testament
            });
        }

        private Task<BibleBook> GetBibleBookAsync(int book)
        {
            return this.bibleContext.BibleBooks.
                Include(e => e.Genre).
                Where(e => e.Id == book).
                DefaultIfEmpty(null).FirstOrDefaultAsync();
        }
    }
}
