namespace BE.UI.Controllers.ComparerComponents
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using BE.Comparer.Business;
    using BE.Comparer.Model;
    using BE.Infrastructure.Model;
    using BE.Infrastructure.Service;
    using Microsoft.AspNetCore.Mvc;

    public class PageContent : ViewComponent
    {
        private readonly IBibleService bibleService;
        private readonly ITextDiff textDiff;

        public PageContent(IBibleService bibleService, ITextDiff textDiff)
        {
            this.bibleService = bibleService;
            this.textDiff = textDiff;
        }

        public async Task<IViewComponentResult> InvokeAsync(BibleViewInfo bibleViewInfo)
        {
            IEnumerable<ComparedBibleVerse> verses = await this.GetComparedBibleChapterAsync(
                bibleViewInfo);

            return this.View(verses);
        }

        private async Task<IEnumerable<ComparedBibleVerse>> GetComparedBibleChapterAsync(
            BibleViewInfo bibleViewInfo)
        {
            Comparison<BibleVerse> comparison = new Comparison<BibleVerse>((x, y) => x.Verse.CompareTo(y.Verse));
            List<BibleVerse> mainBook = await this.bibleService.GetBookChapterVersesAsync(
                bibleViewInfo.MainBible,
                bibleViewInfo.Book,
                bibleViewInfo.Chapter);

            mainBook.Sort(comparison);

            List<BibleVerse> otherBook = null;
            if (bibleViewInfo.CompareBible != BibleID.NONE && bibleViewInfo.CompareBible != BibleID.INVALID)
            {
                otherBook = await this.bibleService.GetBookChapterVersesAsync(
                    bibleViewInfo.CompareBible,
                    bibleViewInfo.Book,
                    bibleViewInfo.Chapter);

                otherBook.Sort(comparison);
            }

            List<ComparedBibleVerse> list = new List<ComparedBibleVerse>();

            foreach (var verse in mainBook)
            {
                var index = mainBook.IndexOf(verse);
                var comparedBibleVerse = new ComparedBibleVerse(this.textDiff, verse);

                if (otherBook != null)
                {
                    var otherBibleID = bibleViewInfo.CompareBible;
                    var otherBibleBook = otherBook;
                    var otherVerse = otherBibleBook[index];

                    comparedBibleVerse.AddComparison(otherBibleID, otherVerse);
                }

                list.Add(comparedBibleVerse);
            }

            return list;
        }
    }
}
